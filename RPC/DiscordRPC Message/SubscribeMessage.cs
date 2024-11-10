using System;
using DiscordRPC.RPC.Payload;

namespace DiscordRPC.Message
{
    public class SubscribeMessage : IMessage
    {
        public override MessageType Type
        {
            get
            {
                return MessageType.Subscribe;
            }
        }

        public EventType Event { get; internal set; }

        internal SubscribeMessage(ServerEvent evt)
        {
            switch (evt)
            {
                default:
                    this.Event = EventType.Join;
                    break;
                case ServerEvent.ActivitySpectate:
                    this.Event = EventType.Spectate;
                    break;
                case ServerEvent.ActivityJoinRequest:
                    this.Event = EventType.JoinRequest;
                    break;
            }
        }
    }
}
