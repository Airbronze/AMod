using System;
using Newtonsoft.Json;

namespace DiscordRPC.Message
{
    public class ReadyMessage : IMessage
    {
        public override MessageType Type
        {
            get
            {
                return MessageType.Ready;
            }
        }

        [JsonProperty("config")]
        public Configuration Configuration { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("v")]
        public int Version { get; set; }
    }
}
