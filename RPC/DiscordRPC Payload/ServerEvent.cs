using System;
using DiscordRPC.Converters;

namespace DiscordRPC.RPC.Payload
{
    internal enum ServerEvent
    {
        [EnumValue("READY")]
        Ready,
        [EnumValue("ERROR")]
        Error,
        [EnumValue("ACTIVITY_JOIN")]
        ActivityJoin,
        [EnumValue("ACTIVITY_SPECTATE")]
        ActivitySpectate,
        [EnumValue("ACTIVITY_JOIN_REQUEST")]
        ActivityJoinRequest
    }
}
