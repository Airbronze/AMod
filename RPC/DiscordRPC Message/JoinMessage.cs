using System;
using Newtonsoft.Json;

namespace DiscordRPC.Message
{
    public class JoinMessage : IMessage
    {
        public override MessageType Type
        {
            get
            {
                return MessageType.Join;
            }
        }

        [JsonProperty("secret")]
        public string Secret { get; internal set; }
    }
}
