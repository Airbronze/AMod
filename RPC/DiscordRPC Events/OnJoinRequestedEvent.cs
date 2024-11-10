using System;
using DiscordRPC.Message;

namespace DiscordRPC.Events
{
    public delegate void OnJoinRequestedEvent(object sender, JoinRequestMessage args);
}
