using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using DiscordRPC.Logging;

namespace DiscordRPC.IO
{
    public sealed class ManagedNamedPipeClient : INamedPipeClient, IDisposable
    {
        public ILogger Logger { get; set; }

        public bool IsConnected
        {
            get
            {
                bool isClosed = this._isClosed;
                bool result;
                if (isClosed)
                {
                    result = false;
                }
                else
                {
                    object obj = this.l_stream;
                    lock (obj)
                    {
                        result = (this._stream != null && this._stream.IsConnected);
                    }
                }
                return result;
            }
        }

        public int ConnectedPipe
        {
            get
            {
                return this._connectedPipe;
            }
        }

        public ManagedNamedPipeClient()
        {
            this._buffer = new byte[PipeFrame.MAX_SIZE];
            this.Logger = new NullLogger();
            this._stream = null;
        }

        public bool Connect(int pipe)
        {
            this.Logger.Trace("ManagedNamedPipeClient.Connection({0})", new object[]
            {
                pipe
            });
            bool isDisposed = this._isDisposed;
            if (isDisposed)
            {
                throw new ObjectDisposedException("NamedPipe");
            }
            bool flag = pipe > 9;
            if (flag)
            {
                throw new ArgumentOutOfRangeException("pipe", "Argument cannot be greater than 9");
            }
            bool flag2 = pipe < 0;
            if (flag2)
            {
                for (int i = 0; i < 10; i++)
                {
                    bool flag3 = this.AttemptConnection(i, false) || this.AttemptConnection(i, true);
                    if (flag3)
                    {
                        this.BeginReadStream();
                        return true;
                    }
                }
            }
            else
            {
                bool flag4 = this.AttemptConnection(pipe, false) || this.AttemptConnection(pipe, true);
                if (flag4)
                {
                    this.BeginReadStream();
                    return true;
                }
            }
            return false;
        }

        private bool AttemptConnection(int pipe, bool isSandbox = false)
        {
            bool isDisposed = this._isDisposed;
            if (isDisposed)
            {
                throw new ObjectDisposedException("_stream");
            }
            string text = isSandbox ? ManagedNamedPipeClient.GetPipeSandbox() : "";
            bool flag = isSandbox && text == null;
            bool result;
            if (flag)
            {
                this.Logger.Trace("Skipping sandbox connection.", Array.Empty<object>());
                result = false;
            }
            else
            {
                this.Logger.Trace("Connection Attempt {0} ({1})", new object[]
                {
                    pipe,
                    text
                });
                string pipeName = ManagedNamedPipeClient.GetPipeName(pipe, text);
                try
                {
                    object obj = this.l_stream;
                    lock (obj)
                    {
                        this.Logger.Info("Attempting to connect to '{0}'", new object[]
                        {
                            pipeName
                        });
                        this._stream = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);
                        this._stream.Connect(0);
                        this.Logger.Trace("Waiting for connection...", Array.Empty<object>());
                        do
                        {
                            Thread.Sleep(10);
                        }
                        while (!this._stream.IsConnected);
                    }
                    this.Logger.Info("Connected to '{0}'", new object[]
                    {
                        pipeName
                    });
                    this._connectedPipe = pipe;
                    this._isClosed = false;
                }
                catch (Exception ex)
                {
                    this.Logger.Error("Failed connection to {0}. {1}", new object[]
                    {
                        pipeName,
                        ex.Message
                    });
                    this.Close();
                }
                this.Logger.Trace("Done. Result: {0}", new object[]
                {
                    this._isClosed
                });
                result = !this._isClosed;
            }
            return result;
        }

        private void BeginReadStream()
        {
            bool isClosed = this._isClosed;
            if (!isClosed)
            {
                try
                {
                    object obj = this.l_stream;
                    lock (obj)
                    {
                        bool flag2 = this._stream == null || !this._stream.IsConnected;
                        if (!flag2)
                        {
                            this.Logger.Trace("Begining Read of {0} bytes", new object[]
                            {
                                this._buffer.Length
                            });
                            this._stream.BeginRead(this._buffer, 0, this._buffer.Length, new AsyncCallback(this.EndReadStream), this._stream.IsConnected);
                        }
                    }
                }
                catch (ObjectDisposedException)
                {
                    this.Logger.Warning("Attempted to start reading from a disposed pipe", Array.Empty<object>());
                }
                catch (InvalidOperationException)
                {
                    this.Logger.Warning("Attempted to start reading from a closed pipe", Array.Empty<object>());
                }
                catch (Exception ex)
                {
                    this.Logger.Error("An exception occured while starting to read a stream: {0}", new object[]
                    {
                        ex.Message
                    });
                    this.Logger.Error(ex.StackTrace, Array.Empty<object>());
                }
            }
        }

        private void EndReadStream(IAsyncResult callback)
        {
            this.Logger.Trace("Ending Read", Array.Empty<object>());
            int num = 0;
            try
            {
                object obj = this.l_stream;
                lock (obj)
                {
                    bool flag2 = this._stream == null || !this._stream.IsConnected;
                    if (flag2)
                    {
                        return;
                    }
                    num = this._stream.EndRead(callback);
                }
            }
            catch (IOException)
            {
                this.Logger.Warning("Attempted to end reading from a closed pipe", Array.Empty<object>());
                return;
            }
            catch (NullReferenceException)
            {
                this.Logger.Warning("Attempted to read from a null pipe", Array.Empty<object>());
                return;
            }
            catch (ObjectDisposedException)
            {
                this.Logger.Warning("Attemped to end reading from a disposed pipe", Array.Empty<object>());
                return;
            }
            catch (Exception ex)
            {
                this.Logger.Error("An exception occured while ending a read of a stream: {0}", new object[]
                {
                    ex.Message
                });
                this.Logger.Error(ex.StackTrace, Array.Empty<object>());
                return;
            }
            this.Logger.Trace("Read {0} bytes", new object[]
            {
                num
            });
            bool flag3 = num > 0;
            if (flag3)
            {
                using (MemoryStream memoryStream = new MemoryStream(this._buffer, 0, num))
                {
                    try
                    {
                        PipeFrame item = default(PipeFrame);
                        bool flag4 = item.ReadStream(memoryStream);
                        if (flag4)
                        {
                            this.Logger.Trace("Read a frame: {0}", new object[]
                            {
                                item.Opcode
                            });
                            object framequeuelock = this._framequeuelock;
                            lock (framequeuelock)
                            {
                                this._framequeue.Enqueue(item);
                            }
                        }
                        else
                        {
                            this.Logger.Error("Pipe failed to read from the data received by the stream.", Array.Empty<object>());
                            this.Close();
                        }
                    }
                    catch (Exception ex2)
                    {
                        this.Logger.Error("A exception has occured while trying to parse the pipe data: {0}", new object[]
                        {
                            ex2.Message
                        });
                        this.Close();
                    }
                }
            }
            else
            {
                bool flag6 = ManagedNamedPipeClient.IsUnix();
                if (flag6)
                {
                    this.Logger.Error("Empty frame was read on {0}, aborting.", new object[]
                    {
                        Environment.OSVersion
                    });
                    this.Close();
                }
                else
                {
                    this.Logger.Warning("Empty frame was read. Please send report to Lachee.", Array.Empty<object>());
                }
            }
            bool flag7 = !this._isClosed && this.IsConnected;
            if (flag7)
            {
                this.Logger.Trace("Starting another read", Array.Empty<object>());
                this.BeginReadStream();
            }
        }

        public bool ReadFrame(out PipeFrame frame)
        {
            bool isDisposed = this._isDisposed;
            if (isDisposed)
            {
                throw new ObjectDisposedException("_stream");
            }
            object framequeuelock = this._framequeuelock;
            bool result;
            lock (framequeuelock)
            {
                bool flag2 = this._framequeue.Count == 0;
                if (flag2)
                {
                    frame = default(PipeFrame);
                    result = false;
                }
                else
                {
                    frame = this._framequeue.Dequeue();
                    result = true;
                }
            }
            return result;
        }

        public bool WriteFrame(PipeFrame frame)
        {
            bool isDisposed = this._isDisposed;
            if (isDisposed)
            {
                throw new ObjectDisposedException("_stream");
            }
            bool flag = this._isClosed || !this.IsConnected;
            bool result;
            if (flag)
            {
                this.Logger.Error("Failed to write frame because the stream is closed", Array.Empty<object>());
                result = false;
            }
            else
            {
                try
                {
                    frame.WriteStream(this._stream);
                    return true;
                }
                catch (IOException ex)
                {
                    this.Logger.Error("Failed to write frame because of a IO Exception: {0}", new object[]
                    {
                        ex.Message
                    });
                }
                catch (ObjectDisposedException)
                {
                    this.Logger.Warning("Failed to write frame as the stream was already disposed", Array.Empty<object>());
                }
                catch (InvalidOperationException)
                {
                    this.Logger.Warning("Failed to write frame because of a invalid operation", Array.Empty<object>());
                }
                result = false;
            }
            return result;
        }

        public void Close()
        {
            bool isClosed = this._isClosed;
            if (isClosed)
            {
                this.Logger.Warning("Tried to close a already closed pipe.", Array.Empty<object>());
            }
            else
            {
                try
                {
                    object obj = this.l_stream;
                    lock (obj)
                    {
                        bool flag2 = this._stream != null;
                        if (flag2)
                        {
                            try
                            {
                                this._stream.Flush();
                                this._stream.Dispose();
                            }
                            catch (Exception)
                            {
                            }
                            this._stream = null;
                            this._isClosed = true;
                        }
                        else
                        {
                            this.Logger.Warning("Stream was closed, but no stream was available to begin with!", Array.Empty<object>());
                        }
                    }
                }
                catch (ObjectDisposedException)
                {
                    this.Logger.Warning("Tried to dispose already disposed stream", Array.Empty<object>());
                }
                finally
                {
                    this._isClosed = true;
                    this._connectedPipe = -1;
                }
            }
        }

        public void Dispose()
        {
            bool isDisposed = this._isDisposed;
            if (!isDisposed)
            {
                bool flag = !this._isClosed;
                if (flag)
                {
                    this.Close();
                }
                object obj = this.l_stream;
                lock (obj)
                {
                    bool flag3 = this._stream != null;
                    if (flag3)
                    {
                        this._stream.Dispose();
                        this._stream = null;
                    }
                }
                this._isDisposed = true;
            }
        }

        public static string GetPipeName(int pipe, string sandbox)
        {
            bool flag = !ManagedNamedPipeClient.IsUnix();
            string result;
            if (flag)
            {
                result = sandbox + string.Format("discord-ipc-{0}", pipe);
            }
            else
            {
                result = Path.Combine(ManagedNamedPipeClient.GetTemporaryDirectory(), sandbox + string.Format("discord-ipc-{0}", pipe));
            }
            return result;
        }

        public static string GetPipeName(int pipe)
        {
            return ManagedNamedPipeClient.GetPipeName(pipe, "");
        }

        public static string GetPipeSandbox()
        {
            PlatformID platform = Environment.OSVersion.Platform;
            PlatformID platformID = platform;
            string result;
            if (platformID != PlatformID.Unix)
            {
                result = null;
            }
            else
            {
                result = "snap.discord/";
            }
            return result;
        }

        private static string GetTemporaryDirectory()
        {
            string text = null;
            text = (text ?? Environment.GetEnvironmentVariable("XDG_RUNTIME_DIR"));
            text = (text ?? Environment.GetEnvironmentVariable("TMPDIR"));
            text = (text ?? Environment.GetEnvironmentVariable("TMP"));
            text = (text ?? Environment.GetEnvironmentVariable("TEMP"));
            return text ?? "/tmp";
        }

        public static bool IsUnix()
        {
            PlatformID platform = Environment.OSVersion.Platform;
            PlatformID platformID = platform;
            return platformID == PlatformID.Unix || platformID == PlatformID.MacOSX;
        }

        private const string PIPE_NAME = "discord-ipc-{0}";

        private int _connectedPipe;

        private NamedPipeClientStream _stream;

        private byte[] _buffer = new byte[PipeFrame.MAX_SIZE];

        private Queue<PipeFrame> _framequeue = new Queue<PipeFrame>();

        private object _framequeuelock = new object();

        private volatile bool _isDisposed = false;

        private volatile bool _isClosed = true;

        private object l_stream = new object();
    }
}
