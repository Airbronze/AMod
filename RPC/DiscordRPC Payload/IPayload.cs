using System;
using DiscordRPC.Converters;
using Newtonsoft.Json;

namespace DiscordRPC.RPC.Payload
{
    internal abstract class IPayload
    {
        [JsonProperty("cmd")]
        [JsonConverter(typeof(EnumSnakeCaseConverter))]
        public Command Command { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        protected IPayload()
        {
        }

        protected IPayload(long nonce)
        {
            this.Nonce = nonce.ToString();
        }

        public override string ToString()
        {
            return string.Format("Payload || Command: {0}, Nonce: {1}", this.Command, this.Nonce);
        }
    }
}
