using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using DiscordRPC.Converters;
using DiscordRPC.Events;
using DiscordRPC.Helper;
using DiscordRPC.IO;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using DiscordRPC.RPC.Commands;
using DiscordRPC.RPC.Payload;
using Newtonsoft.Json;

namespace DiscordRPC.RPC
{
    internal class RpcConnection : IDisposable
    {
        public ILogger Logger
        {
            get
            {
                return this._logger;
            }
            set
            {
                this._logger = value;
                bool flag = this.namedPipe != null;
                if (flag)
                {
                    this.namedPipe.Logger = value;
                }
            }
        }

        public event OnRpcMessageEvent OnRpcMessage;

        public RpcState State
        {
            get
            {
                object obj = this.l_states;
                RpcState state;
                lock (obj)
                {
                    state = this._state;
                }
                return state;
            }
        }

        public Configuration Configuration
        {
            get
            {
                Configuration result = null;
                object obj = this.l_config;
                lock (obj)
                {
                    result = this._configuration;
                }
                return result;
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.thread != null;
            }
        }

        public bool ShutdownOnly { get; set; }

        public RpcConnection(string applicationID, int processID, int targetPipe, INamedPipeClient client, uint maxRxQueueSize = 128U, uint maxRtQueueSize = 512U)
        {
            this.applicationID = applicationID;
            this.processID = processID;
            this.targetPipe = targetPipe;
            this.namedPipe = client;
            this.ShutdownOnly = true;
            this.Logger = new ConsoleLogger();
            this.delay = new BackoffDelay(500, 60000);
            this._maxRtQueueSize = maxRtQueueSize;
            this._rtqueue = new Queue<ICommand>((int)(this._maxRtQueueSize + 1U));
            this._maxRxQueueSize = maxRxQueueSize;
            this._rxqueue = new Queue<IMessage>((int)(this._maxRxQueueSize + 1U));
            this.nonce = 0L;
        }

        private long GetNextNonce()
        {
            this.nonce += 1L;
            return this.nonce;
        }

        internal void EnqueueCommand(ICommand command)
        {
            this.Logger.Trace("Enqueue Command: {0}", new object[]
            {
                command.GetType().FullName
            });
            bool flag = this.aborting || this.shutdown;
            if (!flag)
            {
                object obj = this.l_rtqueue;
                lock (obj)
                {
                    bool flag3 = (long)this._rtqueue.Count == (long)((ulong)this._maxRtQueueSize);
                    if (flag3)
                    {
                        this.Logger.Error("Too many enqueued commands, dropping oldest one. Maybe you are pushing new presences to fast?", Array.Empty<object>());
                        this._rtqueue.Dequeue();
                    }
                    this._rtqueue.Enqueue(command);
                }
            }
        }

        private void EnqueueMessage(IMessage message)
        {
            try
            {
                bool flag = this.OnRpcMessage != null;
                if (flag)
                {
                    this.OnRpcMessage(this, message);
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error("Unhandled Exception while processing event: {0}", new object[]
                {
                    ex.GetType().FullName
                });
                this.Logger.Error(ex.Message, Array.Empty<object>());
                this.Logger.Error(ex.StackTrace, Array.Empty<object>());
            }
            bool flag2 = this._maxRxQueueSize <= 0U;
            if (flag2)
            {
                this.Logger.Trace("Enqueued Message, but queue size is 0.", Array.Empty<object>());
            }
            else
            {
                this.Logger.Trace("Enqueue Message: {0}", new object[]
                {
                    message.Type
                });
                object obj = this.l_rxqueue;
                lock (obj)
                {
                    bool flag4 = (long)this._rxqueue.Count == (long)((ulong)this._maxRxQueueSize);
                    if (flag4)
                    {
                        this.Logger.Warning("Too many enqueued messages, dropping oldest one.", Array.Empty<object>());
                        this._rxqueue.Dequeue();
                    }
                    this._rxqueue.Enqueue(message);
                }
            }
        }

        internal IMessage DequeueMessage()
        {
            object obj = this.l_rxqueue;
            IMessage result;
            lock (obj)
            {
                bool flag2 = this._rxqueue.Count == 0;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    result = this._rxqueue.Dequeue();
                }
            }
            return result;
        }

        internal IMessage[] DequeueMessages()
        {
            object obj = this.l_rxqueue;
            IMessage[] result;
            lock (obj)
            {
                IMessage[] array = this._rxqueue.ToArray();
                this._rxqueue.Clear();
                result = array;
            }
            return result;
        }

        private void MainLoop()
        {
            this.Logger.Info("RPC Connection Started", Array.Empty<object>());
            bool flag = this.Logger.Level <= LogLevel.Trace;
            if (flag)
            {
                this.Logger.Trace("============================", Array.Empty<object>());
                this.Logger.Trace("Assembly:             " + Assembly.GetAssembly(typeof(RichPresence)).FullName, Array.Empty<object>());
                this.Logger.Trace("Pipe:                 " + this.namedPipe.GetType().FullName, Array.Empty<object>());
                this.Logger.Trace("Platform:             " + Environment.OSVersion.ToString(), Array.Empty<object>());
                this.Logger.Trace("applicationID:        " + this.applicationID, Array.Empty<object>());
                this.Logger.Trace("targetPipe:           " + this.targetPipe.ToString(), Array.Empty<object>());
                this.Logger.Trace("POLL_RATE:            " + RpcConnection.POLL_RATE.ToString(), Array.Empty<object>());
                this.Logger.Trace("_maxRtQueueSize:      " + this._maxRtQueueSize.ToString(), Array.Empty<object>());
                this.Logger.Trace("_maxRxQueueSize:      " + this._maxRxQueueSize.ToString(), Array.Empty<object>());
                this.Logger.Trace("============================", Array.Empty<object>());
            }
            while (!this.aborting && !this.shutdown)
            {
                try
                {
                    bool flag2 = this.namedPipe == null;
                    if (flag2)
                    {
                        this.Logger.Error("Something bad has happened with our pipe client!", Array.Empty<object>());
                        this.aborting = true;
                        return;
                    }
                    this.Logger.Trace("Connecting to the pipe through the {0}", new object[]
                    {
                        this.namedPipe.GetType().FullName
                    });
                    bool flag3 = this.namedPipe.Connect(this.targetPipe);
                    if (flag3)
                    {
                        this.Logger.Trace("Connected to the pipe. Attempting to establish handshake...", Array.Empty<object>());
                        this.EnqueueMessage(new ConnectionEstablishedMessage
                        {
                            ConnectedPipe = this.namedPipe.ConnectedPipe
                        });
                        this.EstablishHandshake();
                        this.Logger.Trace("Connection Established. Starting reading loop...", Array.Empty<object>());
                        bool flag4 = true;
                        while (flag4 && !this.aborting && !this.shutdown && this.namedPipe.IsConnected)
                        {
                            PipeFrame frame;
                            bool flag5 = this.namedPipe.ReadFrame(out frame);
                            if (flag5)
                            {
                                this.Logger.Trace("Read Payload: {0}", new object[]
                                {
                                    frame.Opcode
                                });
                                switch (frame.Opcode)
                                {
                                    case Opcode.Handshake:
                                        goto IL_4A6;
                                    case Opcode.Frame:
                                        {
                                            bool flag6 = this.shutdown;
                                            if (flag6)
                                            {
                                                this.Logger.Warning("Skipping frame because we are shutting down.", Array.Empty<object>());
                                            }
                                            else
                                            {
                                                bool flag7 = frame.Data == null;
                                                if (flag7)
                                                {
                                                    this.Logger.Error("We received no data from the frame so we cannot get the event payload!", Array.Empty<object>());
                                                }
                                                else
                                                {
                                                    EventPayload eventPayload = null;
                                                    try
                                                    {
                                                        eventPayload = frame.GetObject<EventPayload>();
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        this.Logger.Error("Failed to parse event! {0}", new object[]
                                                        {
                                                    ex.Message
                                                        });
                                                        this.Logger.Error("Data: {0}", new object[]
                                                        {
                                                    frame.Message
                                                        });
                                                    }
                                                    try
                                                    {
                                                        bool flag8 = eventPayload != null;
                                                        if (flag8)
                                                        {
                                                            this.ProcessFrame(eventPayload);
                                                        }
                                                    }
                                                    catch (Exception ex2)
                                                    {
                                                        this.Logger.Error("Failed to process event! {0}", new object[]
                                                        {
                                                    ex2.Message
                                                        });
                                                        this.Logger.Error("Data: {0}", new object[]
                                                        {
                                                    frame.Message
                                                        });
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case Opcode.Close:
                                        {
                                            ClosePayload @object = frame.GetObject<ClosePayload>();
                                            this.Logger.Warning("We have been told to terminate by discord: ({0}) {1}", new object[]
                                            {
                                        @object.Code,
                                        @object.Reason
                                            });
                                            this.EnqueueMessage(new CloseMessage
                                            {
                                                Code = @object.Code,
                                                Reason = @object.Reason
                                            });
                                            flag4 = false;
                                            break;
                                        }
                                    case Opcode.Ping:
                                        this.Logger.Trace("PING", Array.Empty<object>());
                                        frame.Opcode = Opcode.Pong;
                                        this.namedPipe.WriteFrame(frame);
                                        break;
                                    case Opcode.Pong:
                                        this.Logger.Trace("PONG", Array.Empty<object>());
                                        break;
                                    default:
                                        goto IL_4A6;
                                }
                                goto IL_4D2;
                            IL_4A6:
                                this.Logger.Error("Invalid opcode: {0}", new object[]
                                {
                                    frame.Opcode
                                });
                                flag4 = false;
                            }
                        IL_4D2:
                            bool flag9 = !this.aborting && this.namedPipe.IsConnected;
                            if (flag9)
                            {
                                this.ProcessCommandQueue();
                                this.queueUpdatedEvent.WaitOne(RpcConnection.POLL_RATE);
                            }
                        }
                        this.Logger.Trace("Left main read loop for some reason. Aborting: {0}, Shutting Down: {1}", new object[]
                        {
                            this.aborting,
                            this.shutdown
                        });
                    }
                    else
                    {
                        this.Logger.Error("Failed to connect for some reason.", Array.Empty<object>());
                        this.EnqueueMessage(new ConnectionFailedMessage
                        {
                            FailedPipe = this.targetPipe
                        });
                    }
                    bool flag10 = !this.aborting && !this.shutdown;
                    if (flag10)
                    {
                        long num = (long)this.delay.NextDelay();
                        this.Logger.Trace("Waiting {0}ms before attempting to connect again", new object[]
                        {
                            num
                        });
                        Thread.Sleep(this.delay.NextDelay());
                    }
                }
                catch (Exception ex3)
                {
                    this.Logger.Error("Unhandled Exception: {0}", new object[]
                    {
                        ex3.GetType().FullName
                    });
                    this.Logger.Error(ex3.Message, Array.Empty<object>());
                    this.Logger.Error(ex3.StackTrace, Array.Empty<object>());
                }
                finally
                {
                    bool isConnected = this.namedPipe.IsConnected;
                    if (isConnected)
                    {
                        this.Logger.Trace("Closing the named pipe.", Array.Empty<object>());
                        this.namedPipe.Close();
                    }
                    this.SetConnectionState(RpcState.Disconnected);
                }
            }
            this.Logger.Trace("Left Main Loop", Array.Empty<object>());
            bool flag11 = this.namedPipe != null;
            if (flag11)
            {
                this.namedPipe.Dispose();
            }
            this.Logger.Info("Thread Terminated, no longer performing RPC connection.", Array.Empty<object>());
        }

        private void ProcessFrame(EventPayload response)
        {
            this.Logger.Info("Handling Response. Cmd: {0}, Event: {1}", new object[]
            {
                response.Command,
                response.Event
            });
            bool flag = response.Event != null && response.Event.Value == ServerEvent.Error;
            if (flag)
            {
                this.Logger.Error("Error received from the RPC", Array.Empty<object>());
                ErrorMessage @object = response.GetObject<ErrorMessage>();
                this.Logger.Error("Server responded with an error message: ({0}) {1}", new object[]
                {
                    @object.Code.ToString(),
                    @object.Message
                });
                this.EnqueueMessage(@object);
            }
            else
            {
                bool flag2 = this.State == RpcState.Connecting;
                if (flag2)
                {
                    bool flag3 = response.Command == Command.Dispatch && response.Event != null && response.Event.Value == ServerEvent.Ready;
                    if (flag3)
                    {
                        this.Logger.Info("Connection established with the RPC", Array.Empty<object>());
                        this.SetConnectionState(RpcState.Connected);
                        this.delay.Reset();
                        ReadyMessage object2 = response.GetObject<ReadyMessage>();
                        object obj = this.l_config;
                        lock (obj)
                        {
                            this._configuration = object2.Configuration;
                            object2.User.SetConfiguration(this._configuration);
                        }
                        this.EnqueueMessage(object2);
                        return;
                    }
                }
                bool flag5 = this.State == RpcState.Connected;
                if (flag5)
                {
                    switch (response.Command)
                    {
                        case Command.Dispatch:
                            this.ProcessDispatch(response);
                            break;
                        case Command.SetActivity:
                            {
                                bool flag6 = response.Data == null;
                                if (flag6)
                                {
                                    this.EnqueueMessage(new PresenceMessage());
                                }
                                else
                                {
                                    RichPresenceResponse object3 = response.GetObject<RichPresenceResponse>();
                                    this.EnqueueMessage(new PresenceMessage(object3));
                                }
                                break;
                            }
                        case Command.Subscribe:
                        case Command.Unsubscribe:
                            {
                                JsonSerializer jsonSerializer = new JsonSerializer();
                                jsonSerializer.Converters.Add(new EnumSnakeCaseConverter());
                                ServerEvent value = response.GetObject<EventPayload>().Event.Value;
                                bool flag7 = response.Command == Command.Subscribe;
                                if (flag7)
                                {
                                    this.EnqueueMessage(new SubscribeMessage(value));
                                }
                                else
                                {
                                    this.EnqueueMessage(new UnsubscribeMessage(value));
                                }
                                break;
                            }
                        case Command.SendActivityJoinInvite:
                            this.Logger.Trace("Got invite response ack.", Array.Empty<object>());
                            break;
                        case Command.CloseActivityJoinRequest:
                            this.Logger.Trace("Got invite response reject ack.", Array.Empty<object>());
                            break;
                        default:
                            this.Logger.Error("Unkown frame was received! {0}", new object[]
                            {
                            response.Command
                            });
                            break;
                    }
                }
                else
                {
                    this.Logger.Trace("Received a frame while we are disconnected. Ignoring. Cmd: {0}, Event: {1}", new object[]
                    {
                        response.Command,
                        response.Event
                    });
                }
            }
        }

        private void ProcessDispatch(EventPayload response)
        {
            bool flag = response.Command > Command.Dispatch;
            if (!flag)
            {
                bool flag2 = response.Event == null;
                if (!flag2)
                {
                    switch (response.Event.Value)
                    {
                        case ServerEvent.ActivityJoin:
                            {
                                JoinMessage @object = response.GetObject<JoinMessage>();
                                this.EnqueueMessage(@object);
                                break;
                            }
                        case ServerEvent.ActivitySpectate:
                            {
                                SpectateMessage object2 = response.GetObject<SpectateMessage>();
                                this.EnqueueMessage(object2);
                                break;
                            }
                        case ServerEvent.ActivityJoinRequest:
                            {
                                JoinRequestMessage object3 = response.GetObject<JoinRequestMessage>();
                                this.EnqueueMessage(object3);
                                break;
                            }
                        default:
                            this.Logger.Warning("Ignoring {0}", new object[]
                            {
                            response.Event.Value
                            });
                            break;
                    }
                }
            }
        }

        private void ProcessCommandQueue()
        {
            bool flag = this.State != RpcState.Connected;
            if (!flag)
            {
                bool flag2 = this.aborting;
                if (flag2)
                {
                    this.Logger.Warning("We have been told to write a queue but we have also been aborted.", Array.Empty<object>());
                }
                bool flag3 = true;
                ICommand command = null;
                while (flag3 && this.namedPipe.IsConnected)
                {
                    object obj = this.l_rtqueue;
                    lock (obj)
                    {
                        flag3 = (this._rtqueue.Count > 0);
                        bool flag5 = !flag3;
                        if (flag5)
                        {
                            break;
                        }
                        command = this._rtqueue.Peek();
                    }
                    bool flag6 = this.shutdown || (!this.aborting && RpcConnection.LOCK_STEP);
                    if (flag6)
                    {
                        flag3 = false;
                    }
                    IPayload payload = command.PreparePayload(this.GetNextNonce());
                    this.Logger.Trace("Attempting to send payload: {0}", new object[]
                    {
                        payload.Command
                    });
                    PipeFrame frame = default(PipeFrame);
                    bool flag7 = command is CloseCommand;
                    if (flag7)
                    {
                        this.SendHandwave();
                        this.Logger.Trace("Handwave sent, ending queue processing.", Array.Empty<object>());
                        object obj2 = this.l_rtqueue;
                        lock (obj2)
                        {
                            this._rtqueue.Dequeue();
                        }
                        break;
                    }
                    bool flag9 = this.aborting;
                    if (flag9)
                    {
                        this.Logger.Warning("- skipping frame because of abort.", Array.Empty<object>());
                        object obj3 = this.l_rtqueue;
                        lock (obj3)
                        {
                            this._rtqueue.Dequeue();
                        }
                    }
                    else
                    {
                        frame.SetObject(Opcode.Frame, payload);
                        this.Logger.Trace("Sending payload: {0}", new object[]
                        {
                            payload.Command
                        });
                        bool flag11 = this.namedPipe.WriteFrame(frame);
                        if (!flag11)
                        {
                            this.Logger.Warning("Something went wrong during writing!", Array.Empty<object>());
                            break;
                        }
                        this.Logger.Trace("Sent Successfully.", Array.Empty<object>());
                        object obj4 = this.l_rtqueue;
                        lock (obj4)
                        {
                            this._rtqueue.Dequeue();
                        }
                    }
                }
            }
        }

        private void EstablishHandshake()
        {
            this.Logger.Trace("Attempting to establish a handshake...", Array.Empty<object>());
            bool flag = this.State > RpcState.Disconnected;
            if (flag)
            {
                this.Logger.Error("State must be disconnected in order to start a handshake!", Array.Empty<object>());
            }
            else
            {
                this.Logger.Trace("Sending Handshake...", Array.Empty<object>());
                bool flag2 = !this.namedPipe.WriteFrame(new PipeFrame(Opcode.Handshake, new Handshake
                {
                    Version = RpcConnection.VERSION,
                    ClientID = this.applicationID
                }));
                if (flag2)
                {
                    this.Logger.Error("Failed to write a handshake.", Array.Empty<object>());
                }
                else
                {
                    this.SetConnectionState(RpcState.Connecting);
                }
            }
        }

        private void SendHandwave()
        {
            this.Logger.Info("Attempting to wave goodbye...", Array.Empty<object>());
            bool flag = this.State == RpcState.Disconnected;
            if (flag)
            {
                this.Logger.Error("State must NOT be disconnected in order to send a handwave!", Array.Empty<object>());
            }
            else
            {
                bool flag2 = !this.namedPipe.WriteFrame(new PipeFrame(Opcode.Close, new Handshake
                {
                    Version = RpcConnection.VERSION,
                    ClientID = this.applicationID
                }));
                if (flag2)
                {
                    this.Logger.Error("failed to write a handwave.", Array.Empty<object>());
                }
            }
        }

        public bool AttemptConnection()
        {
            this.Logger.Info("Attempting a new connection", Array.Empty<object>());
            bool flag = this.thread != null;
            bool result;
            if (flag)
            {
                this.Logger.Error("Cannot attempt a new connection as the previous connection thread is not null!", Array.Empty<object>());
                result = false;
            }
            else
            {
                bool flag2 = this.State > RpcState.Disconnected;
                if (flag2)
                {
                    this.Logger.Warning("Cannot attempt a new connection as the previous connection hasn't changed state yet.", Array.Empty<object>());
                    result = false;
                }
                else
                {
                    bool flag3 = this.aborting;
                    if (flag3)
                    {
                        this.Logger.Error("Cannot attempt a new connection while aborting!", Array.Empty<object>());
                        result = false;
                    }
                    else
                    {
                        this.thread = new Thread(new ThreadStart(this.MainLoop));
                        this.thread.Name = "Discord IPC Thread";
                        this.thread.IsBackground = true;
                        this.thread.Start();
                        result = true;
                    }
                }
            }
            return result;
        }

        private void SetConnectionState(RpcState state)
        {
            this.Logger.Trace("Setting the connection state to {0}", new object[]
            {
                state.ToString().ToSnakeCase().ToUpperInvariant()
            });
            object obj = this.l_states;
            lock (obj)
            {
                this._state = state;
            }
        }

        public void Shutdown()
        {
            this.Logger.Trace("Initiated shutdown procedure", Array.Empty<object>());
            this.shutdown = true;
            object obj = this.l_rtqueue;
            lock (obj)
            {
                this._rtqueue.Clear();
                bool clear_ON_SHUTDOWN = RpcConnection.CLEAR_ON_SHUTDOWN;
                if (clear_ON_SHUTDOWN)
                {
                    this._rtqueue.Enqueue(new PresenceCommand
                    {
                        PID = this.processID,
                        Presence = null
                    });
                }
                this._rtqueue.Enqueue(new CloseCommand());
            }
            this.queueUpdatedEvent.Set();
        }

        public void Close()
        {
            bool flag = this.thread == null;
            if (flag)
            {
                this.Logger.Error("Cannot close as it is not available!", Array.Empty<object>());
            }
            else
            {
                bool flag2 = this.aborting;
                if (flag2)
                {
                    this.Logger.Error("Cannot abort as it has already been aborted", Array.Empty<object>());
                }
                else
                {
                    bool shutdownOnly = this.ShutdownOnly;
                    if (shutdownOnly)
                    {
                        this.Shutdown();
                    }
                    else
                    {
                        this.Logger.Trace("Updating Abort State...", Array.Empty<object>());
                        this.aborting = true;
                        this.queueUpdatedEvent.Set();
                    }
                }
            }
        }

        public void Dispose()
        {
            this.ShutdownOnly = false;
            this.Close();
        }

        public static readonly int VERSION = 1;

        public static readonly int POLL_RATE = 1000;

        private static readonly bool CLEAR_ON_SHUTDOWN = true;

        private static readonly bool LOCK_STEP = false;

        private ILogger _logger;

        private RpcState _state;

        private readonly object l_states = new object();

        private Configuration _configuration = null;

        private readonly object l_config = new object();

        private volatile bool aborting = false;

        private volatile bool shutdown = false;

        private string applicationID;

        private int processID;

        private long nonce;

        private Thread thread;

        private INamedPipeClient namedPipe;

        private int targetPipe;

        private readonly object l_rtqueue = new object();

        private readonly uint _maxRtQueueSize;

        private Queue<ICommand> _rtqueue;

        private readonly object l_rxqueue = new object();

        private readonly uint _maxRxQueueSize;

        private Queue<IMessage> _rxqueue;

        private AutoResetEvent queueUpdatedEvent = new AutoResetEvent(false);

        private BackoffDelay delay;
    }
}
