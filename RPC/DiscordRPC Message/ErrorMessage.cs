using System;
using Newtonsoft.Json;

namespace DiscordRPC.Message
{
    public class ErrorMessage : IMessage
    {
        public override MessageType Type
        {
            get
            {
                return MessageType.Error;
            }
        }

        [JsonProperty("code")]
        public ErrorCode Code { get; internal set; }

        [JsonProperty("message")]
        public string Message { get; internal set; }
    }
}
