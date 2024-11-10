﻿using System;

namespace DiscordRPC.Message
{
    public class SpectateMessage : JoinMessage
    {
        public override MessageType Type
        {
            get
            {
                return MessageType.Spectate;
            }
        }
    }
}
