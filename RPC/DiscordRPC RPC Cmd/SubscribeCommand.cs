﻿using System;
using DiscordRPC.RPC.Payload;

namespace DiscordRPC.RPC.Commands
{
    internal class SubscribeCommand : ICommand
    {
        public ServerEvent Event { get; set; }

        public bool IsUnsubscribe { get; set; }

        public IPayload PreparePayload(long nonce)
        {
            return new EventPayload(nonce)
            {
                Command = (this.IsUnsubscribe ? Command.Unsubscribe : Command.Subscribe),
                Event = new ServerEvent?(this.Event)
            };
        }
    }
}