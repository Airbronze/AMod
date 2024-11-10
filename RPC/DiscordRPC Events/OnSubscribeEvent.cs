using System;
using DiscordRPC.Message;

namespace DiscordRPC.Events
{
    public delegate void OnSubscribeEvent(object sender, SubscribeMessage args);
}
