using System;
using DiscordRPC.Message;

namespace DiscordRPC.Events
{
    public delegate void OnUnsubscribeEvent(object sender, UnsubscribeMessage args);
}
