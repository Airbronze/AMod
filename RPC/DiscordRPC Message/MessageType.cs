using System;

namespace DiscordRPC.Message
{
    public enum MessageType
    {
        Ready,
        Close,
        Error,
        PresenceUpdate,
        Subscribe,
        Unsubscribe,
        Join,
        Spectate,
        JoinRequest,
        ConnectionEstablished,
        ConnectionFailed
    }
}
