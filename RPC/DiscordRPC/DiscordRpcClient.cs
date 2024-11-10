using System;
using System.Diagnostics;
using DiscordRPC.Events;
using DiscordRPC.Exceptions;
using DiscordRPC.IO;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using DiscordRPC.RPC;
using DiscordRPC.RPC.Commands;
using DiscordRPC.RPC.Payload;

namespace DiscordRPC
{
    public sealed class DiscordRpcClient : IDisposable
    {
        public bool HasRegisteredUriScheme { get; private set; }

        public string ApplicationID { get; private set; }

        public string SteamID { get; private set; }

        public int ProcessID { get; private set; }

        public int MaxQueueSize { get; private set; }

        public bool IsDisposed { get; private set; }

        public ILogger Logger
        {
            get
            {
                return this._logger;
            }
            set
            {
                this._logger = value;
                bool flag = this.connection != null;
                if (flag)
                {
                    this.connection.Logger = value;
                }
            }
        }

        public bool AutoEvents { get; private set; }

        public bool SkipIdenticalPresence { get; set; }

        public int TargetPipe { get; private set; }

        public RichPresence CurrentPresence { get; private set; }

        public EventType Subscription { get; private set; }

        public User CurrentUser { get; private set; }

        public Configuration Configuration { get; private set; }

        public bool IsInitialized { get; private set; }

        public bool ShutdownOnly
        {
            get
            {
                return this._shutdownOnly;
            }
            set
            {
                this._shutdownOnly = value;
                bool flag = this.connection != null;
                if (flag)
                {
                    this.connection.ShutdownOnly = value;
                }
            }
        }

        public event OnReadyEvent OnReady;

        public event OnCloseEvent OnClose;

        public event OnErrorEvent OnError;

        public event OnPresenceUpdateEvent OnPresenceUpdate;

        public event OnSubscribeEvent OnSubscribe;

        public event OnUnsubscribeEvent OnUnsubscribe;

        public event OnJoinEvent OnJoin;

        public event OnSpectateEvent OnSpectate;

        public event OnJoinRequestedEvent OnJoinRequested;

        public event OnConnectionEstablishedEvent OnConnectionEstablished;

        public event OnConnectionFailedEvent OnConnectionFailed;

        public event OnRpcMessageEvent OnRpcMessage;

        public DiscordRpcClient(string applicationID) : this(applicationID, -1, null, true, null)
        {
        }

        public DiscordRpcClient(string applicationID, int pipe = -1, ILogger logger = null, bool autoEvents = true, INamedPipeClient client = null)
        {
            bool flag = string.IsNullOrEmpty(applicationID);
            if (flag)
            {
                throw new ArgumentNullException("applicationID");
            }
            this.ApplicationID = applicationID.Trim();
            this.TargetPipe = pipe;
            this.ProcessID = Process.GetCurrentProcess().Id;
            this.HasRegisteredUriScheme = false;
            this.AutoEvents = autoEvents;
            this.SkipIdenticalPresence = true;
            this._logger = (logger ?? new NullLogger());
            this.connection = new RpcConnection(this.ApplicationID, this.ProcessID, this.TargetPipe, client ?? new ManagedNamedPipeClient(), autoEvents ? 0U : 128U, 512U)
            {
                ShutdownOnly = this._shutdownOnly,
                Logger = this._logger
            };
            this.connection.OnRpcMessage += delegate (object sender, IMessage msg)
            {
                bool flag2 = this.OnRpcMessage != null;
                if (flag2)
                {
                    this.OnRpcMessage(this, msg);
                }
                bool autoEvents2 = this.AutoEvents;
                if (autoEvents2)
                {
                    this.ProcessMessage(msg);
                }
            };
        }

        public IMessage[] Invoke()
        {
            if (this.AutoEvents)
            {
                this.Logger.Error("Cannot Invoke client when AutomaticallyInvokeEvents has been set.", Array.Empty<object>());
                return new IMessage[0];
            }

            foreach (IMessage message in this.connection.DequeueMessages())
            {
                this.ProcessMessage(message);
            }

            IMessage[] array = new IMessage[0];
            return array;
        }

        private void ProcessMessage(IMessage message)
        {
            bool flag = message == null;
            if (!flag)
            {
                switch (message.Type)
                {
                    case MessageType.Ready:
                        {
                            ReadyMessage readyMessage = message as ReadyMessage;
                            bool flag2 = readyMessage != null;
                            if (flag2)
                            {
                                object sync = this._sync;
                                lock (sync)
                                {
                                    this.Configuration = readyMessage.Configuration;
                                    this.CurrentUser = readyMessage.User;
                                }
                                this.SynchronizeState();
                            }
                            bool flag4 = this.OnReady != null;
                            if (flag4)
                            {
                                this.OnReady(this, message as ReadyMessage);
                            }
                            break;
                        }
                    case MessageType.Close:
                        {
                            bool flag5 = this.OnClose != null;
                            if (flag5)
                            {
                                this.OnClose(this, message as CloseMessage);
                            }
                            break;
                        }
                    case MessageType.Error:
                        {
                            bool flag6 = this.OnError != null;
                            if (flag6)
                            {
                                this.OnError(this, message as ErrorMessage);
                            }
                            break;
                        }
                    case MessageType.PresenceUpdate:
                        {
                            object sync2 = this._sync;
                            lock (sync2)
                            {
                                PresenceMessage presenceMessage = message as PresenceMessage;
                                bool flag8 = presenceMessage != null;
                                if (flag8)
                                {
                                    bool flag9 = presenceMessage.Presence == null;
                                    if (flag9)
                                    {
                                        this.CurrentPresence = null;
                                    }
                                    else
                                    {
                                        bool flag10 = this.CurrentPresence == null;
                                        if (flag10)
                                        {
                                            this.CurrentPresence = new RichPresence().Merge(presenceMessage.Presence);
                                        }
                                        else
                                        {
                                            this.CurrentPresence.Merge(presenceMessage.Presence);
                                        }
                                    }
                                    presenceMessage.Presence = this.CurrentPresence;
                                }
                            }
                            bool flag11 = this.OnPresenceUpdate != null;
                            if (flag11)
                            {
                                this.OnPresenceUpdate(this, message as PresenceMessage);
                            }
                            break;
                        }
                    case MessageType.Subscribe:
                        {
                            object sync3 = this._sync;
                            lock (sync3)
                            {
                                SubscribeMessage subscribeMessage = message as SubscribeMessage;
                                this.Subscription |= subscribeMessage.Event;
                            }
                            bool flag13 = this.OnSubscribe != null;
                            if (flag13)
                            {
                                this.OnSubscribe(this, message as SubscribeMessage);
                            }
                            break;
                        }
                    case MessageType.Unsubscribe:
                        {
                            object sync4 = this._sync;
                            lock (sync4)
                            {
                                UnsubscribeMessage unsubscribeMessage = message as UnsubscribeMessage;
                                this.Subscription &= ~unsubscribeMessage.Event;
                            }
                            bool flag15 = this.OnUnsubscribe != null;
                            if (flag15)
                            {
                                this.OnUnsubscribe(this, message as UnsubscribeMessage);
                            }
                            break;
                        }
                    case MessageType.Join:
                        {
                            bool flag16 = this.OnJoin != null;
                            if (flag16)
                            {
                                this.OnJoin(this, message as JoinMessage);
                            }
                            break;
                        }
                    case MessageType.Spectate:
                        {
                            bool flag17 = this.OnSpectate != null;
                            if (flag17)
                            {
                                this.OnSpectate(this, message as SpectateMessage);
                            }
                            break;
                        }
                    case MessageType.JoinRequest:
                        {
                            bool flag18 = this.Configuration != null;
                            if (flag18)
                            {
                                JoinRequestMessage joinRequestMessage = message as JoinRequestMessage;
                                bool flag19 = joinRequestMessage != null;
                                if (flag19)
                                {
                                    joinRequestMessage.User.SetConfiguration(this.Configuration);
                                }
                            }
                            bool flag20 = this.OnJoinRequested != null;
                            if (flag20)
                            {
                                this.OnJoinRequested(this, message as JoinRequestMessage);
                            }
                            break;
                        }
                    case MessageType.ConnectionEstablished:
                        {
                            bool flag21 = this.OnConnectionEstablished != null;
                            if (flag21)
                            {
                                this.OnConnectionEstablished(this, message as ConnectionEstablishedMessage);
                            }
                            break;
                        }
                    case MessageType.ConnectionFailed:
                        {
                            bool flag22 = this.OnConnectionFailed != null;
                            if (flag22)
                            {
                                this.OnConnectionFailed(this, message as ConnectionFailedMessage);
                            }
                            break;
                        }
                    default:
                        this.Logger.Error("Message was queued with no appropriate handle! {0}", new object[]
                        {
                        message.Type
                        });
                        break;
                }
            }
        }

        public void Respond(JoinRequestMessage request, bool acceptRequest)
        {
            bool isDisposed = this.IsDisposed;
            if (isDisposed)
            {
                throw new ObjectDisposedException("Discord IPC Client");
            }
            bool flag = this.connection == null;
            if (flag)
            {
                throw new ObjectDisposedException("Connection", "Cannot initialize as the connection has been deinitialized");
            }
            bool flag2 = !this.IsInitialized;
            if (flag2)
            {
                throw new UninitializedException();
            }
            this.connection.EnqueueCommand(new RespondCommand
            {
                Accept = acceptRequest,
                UserID = request.User.ID.ToString()
            });
        }

        public void SetPresence(RichPresence presence)
        {
            bool isDisposed = this.IsDisposed;
            if (isDisposed)
            {
                throw new ObjectDisposedException("Discord IPC Client");
            }
            bool flag = this.connection == null;
            if (flag)
            {
                throw new ObjectDisposedException("Connection", "Cannot initialize as the connection has been deinitialized");
            }
            bool flag2 = !this.IsInitialized;
            if (flag2)
            {
                this.Logger.Warning("The client is not yet initialized, storing the presence as a state instead.", Array.Empty<object>());
            }
            bool flag3 = presence == null;
            if (flag3)
            {
                bool flag4 = !this.SkipIdenticalPresence || this.CurrentPresence != null;
                if (flag4)
                {
                    this.connection.EnqueueCommand(new PresenceCommand
                    {
                        PID = this.ProcessID,
                        Presence = null
                    });
                }
            }
            else
            {
                bool flag5 = presence.HasSecrets() && !this.HasRegisteredUriScheme;
                if (flag5)
                {
                    throw new BadPresenceException("Cannot send a presence with secrets as this object has not registered a URI scheme. Please enable the uri scheme registration in the DiscordRpcClient constructor.");
                }
                bool flag6 = presence.HasParty() && presence.Party.Max < presence.Party.Size;
                if (flag6)
                {
                    throw new BadPresenceException("Presence maximum party size cannot be smaller than the current size.");
                }
                bool flag7 = presence.HasSecrets() && !presence.HasParty();
                if (flag7)
                {
                    this.Logger.Warning("The presence has set the secrets but no buttons will show as there is no party available.", Array.Empty<object>());
                }
                bool flag8 = !this.SkipIdenticalPresence || !presence.Matches(this.CurrentPresence);
                if (flag8)
                {
                    this.connection.EnqueueCommand(new PresenceCommand
                    {
                        PID = this.ProcessID,
                        Presence = presence.Clone()
                    });
                }
            }
            object sync = this._sync;
            lock (sync)
            {
                this.CurrentPresence = ((presence != null) ? presence.Clone() : null);
            }
        }

        public RichPresence UpdateButtons(Button[] button = null)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            richPresence.Buttons = button;
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence SetButton(Button button, int index = 0)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            richPresence.Buttons[index] = button;
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence UpdateDetails(string details)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            richPresence.Details = details;
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence UpdateState(string state)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            richPresence.State = state;
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence UpdateParty(Party party)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            richPresence.Party = party;
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence UpdatePartySize(int size)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            bool flag4 = richPresence.Party == null;
            if (flag4)
            {
                throw new BadPresenceException("Cannot set the size of the party if the party does not exist");
            }
            richPresence.Party.Size = size;
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence UpdatePartySize(int size, int max)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            bool flag4 = richPresence.Party == null;
            if (flag4)
            {
                throw new BadPresenceException("Cannot set the size of the party if the party does not exist");
            }
            richPresence.Party.Size = size;
            richPresence.Party.Max = max;
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence UpdateLargeAsset(string key = null, string tooltip = null)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            bool flag4 = richPresence.Assets == null;
            if (flag4)
            {
                richPresence.Assets = new Assets();
            }
            richPresence.Assets.LargeImageKey = (key ?? richPresence.Assets.LargeImageKey);
            richPresence.Assets.LargeImageText = (tooltip ?? richPresence.Assets.LargeImageText);
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence UpdateSmallAsset(string key = null, string tooltip = null)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            bool flag4 = richPresence.Assets == null;
            if (flag4)
            {
                richPresence.Assets = new Assets();
            }
            richPresence.Assets.SmallImageKey = (key ?? richPresence.Assets.SmallImageKey);
            richPresence.Assets.SmallImageText = (tooltip ?? richPresence.Assets.SmallImageText);
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence UpdateSecrets(Secrets secrets)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            richPresence.Secrets = secrets;
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence UpdateStartTime()
        {
            return this.UpdateStartTime(DateTime.UtcNow);
        }

        public RichPresence UpdateStartTime(DateTime time)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            bool flag4 = richPresence.Timestamps == null;
            if (flag4)
            {
                richPresence.Timestamps = new Timestamps();
            }
            richPresence.Timestamps.Start = new DateTime?(time);
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence UpdateEndTime()
        {
            return this.UpdateEndTime(DateTime.UtcNow);
        }

        public RichPresence UpdateEndTime(DateTime time)
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            bool flag4 = richPresence.Timestamps == null;
            if (flag4)
            {
                richPresence.Timestamps = new Timestamps();
            }
            richPresence.Timestamps.End = new DateTime?(time);
            this.SetPresence(richPresence);
            return richPresence;
        }

        public RichPresence UpdateClearTime()
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            object sync = this._sync;
            RichPresence richPresence;
            lock (sync)
            {
                bool flag3 = this.CurrentPresence == null;
                if (flag3)
                {
                    richPresence = new RichPresence();
                }
                else
                {
                    richPresence = this.CurrentPresence.Clone();
                }
            }
            richPresence.Timestamps = null;
            this.SetPresence(richPresence);
            return richPresence;
        }

        public void ClearPresence()
        {
            bool isDisposed = this.IsDisposed;
            if (isDisposed)
            {
                throw new ObjectDisposedException("Discord IPC Client");
            }
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            bool flag2 = this.connection == null;
            if (flag2)
            {
                throw new ObjectDisposedException("Connection", "Cannot initialize as the connection has been deinitialized");
            }
            this.SetPresence(null);
        }

        public void Subscribe(EventType type)
        {
            this.SetSubscription(this.Subscription | type);
        }

        [Obsolete("Replaced with Unsubscribe", true)]
        public void Unubscribe(EventType type)
        {
            this.SetSubscription(this.Subscription & ~type);
        }

        public void Unsubscribe(EventType type)
        {
            this.SetSubscription(this.Subscription & ~type);
        }

        public void SetSubscription(EventType type)
        {
            bool isInitialized = this.IsInitialized;
            if (isInitialized)
            {
                this.SubscribeToTypes(this.Subscription & ~type, true);
                this.SubscribeToTypes(~this.Subscription & type, false);
            }
            else
            {
                this.Logger.Warning("Client has not yet initialized, but events are being subscribed too. Storing them as state instead.", Array.Empty<object>());
            }
            object sync = this._sync;
            lock (sync)
            {
                this.Subscription = type;
            }
        }

        private void SubscribeToTypes(EventType type, bool isUnsubscribe)
        {
            bool flag = type == EventType.None;
            if (!flag)
            {
                bool isDisposed = this.IsDisposed;
                if (isDisposed)
                {
                    throw new ObjectDisposedException("Discord IPC Client");
                }
                bool flag2 = !this.IsInitialized;
                if (flag2)
                {
                    throw new UninitializedException();
                }
                bool flag3 = this.connection == null;
                if (flag3)
                {
                    throw new ObjectDisposedException("Connection", "Cannot initialize as the connection has been deinitialized");
                }
                bool flag4 = !this.HasRegisteredUriScheme;
                if (flag4)
                {
                    throw new InvalidConfigurationException("Cannot subscribe/unsubscribe to an event as this application has not registered a URI Scheme. Call RegisterUriScheme().");
                }
                bool flag5 = (type & EventType.Spectate) == EventType.Spectate;
                if (flag5)
                {
                    this.connection.EnqueueCommand(new SubscribeCommand
                    {
                        Event = ServerEvent.ActivitySpectate,
                        IsUnsubscribe = isUnsubscribe
                    });
                }
                bool flag6 = (type & EventType.Join) == EventType.Join;
                if (flag6)
                {
                    this.connection.EnqueueCommand(new SubscribeCommand
                    {
                        Event = ServerEvent.ActivityJoin,
                        IsUnsubscribe = isUnsubscribe
                    });
                }
                bool flag7 = (type & EventType.JoinRequest) == EventType.JoinRequest;
                if (flag7)
                {
                    this.connection.EnqueueCommand(new SubscribeCommand
                    {
                        Event = ServerEvent.ActivityJoinRequest,
                        IsUnsubscribe = isUnsubscribe
                    });
                }
            }
        }

        public void SynchronizeState()
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException();
            }
            this.SetPresence(this.CurrentPresence);
            bool hasRegisteredUriScheme = this.HasRegisteredUriScheme;
            if (hasRegisteredUriScheme)
            {
                this.SubscribeToTypes(this.Subscription, false);
            }
        }

        public bool Initialize()
        {
            bool isDisposed = this.IsDisposed;
            if (isDisposed)
            {
                throw new ObjectDisposedException("Discord IPC Client");
            }
            bool isInitialized = this.IsInitialized;
            if (isInitialized)
            {
                throw new UninitializedException("Cannot initialize a client that is already initialized");
            }
            bool flag = this.connection == null;
            if (flag)
            {
                throw new ObjectDisposedException("Connection", "Cannot initialize as the connection has been deinitialized");
            }
            return this.IsInitialized = this.connection.AttemptConnection();
        }

        public void Deinitialize()
        {
            bool flag = !this.IsInitialized;
            if (flag)
            {
                throw new UninitializedException("Cannot deinitialize a client that has not been initalized.");
            }
            this.connection.Close();
            this.IsInitialized = false;
        }

        public void Dispose()
        {
            bool isDisposed = this.IsDisposed;
            if (!isDisposed)
            {
                bool isInitialized = this.IsInitialized;
                if (isInitialized)
                {
                    this.Deinitialize();
                }
                this.IsDisposed = true;
            }
        }

        private ILogger _logger;

        private RpcConnection connection;

        private bool _shutdownOnly = true;

        private object _sync = new object();
    }
}
