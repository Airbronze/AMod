using System;
using DiscordRPC.Message;

namespace DiscordRPC.Events
{
    public delegate void OnReadyEvent(object sender, ReadyMessage args);
}
