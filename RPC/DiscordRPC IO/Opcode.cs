using System;

namespace DiscordRPC.IO
{
    public enum Opcode : uint
    {
        Handshake,
        Frame,
        Close,
        Ping,
        Pong
    }
}
