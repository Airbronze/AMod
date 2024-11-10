using System;
using DiscordRPC.Message;

namespace DiscordRPC.Events
{
    public delegate void OnConnectionFailedEvent(object sender, ConnectionFailedMessage args);
}
