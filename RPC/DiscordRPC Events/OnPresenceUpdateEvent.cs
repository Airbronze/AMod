using System;
using DiscordRPC.Message;

namespace DiscordRPC.Events
{
    public delegate void OnPresenceUpdateEvent(object sender, PresenceMessage args);
}
