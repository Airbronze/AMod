using System;
using DiscordRPC.Message;

namespace DiscordRPC.Events
{
    public delegate void OnConnectionEstablishedEvent(object sender, ConnectionEstablishedMessage args);
}
