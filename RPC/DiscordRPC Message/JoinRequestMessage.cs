using System;
using Newtonsoft.Json;

namespace DiscordRPC.Message
{
    public class JoinRequestMessage : IMessage
    {
        public override MessageType Type
        {
            get
            {
                return MessageType.JoinRequest;
            }
        }

        [JsonProperty("user")]
        public User User { get; internal set; }
    }
}
