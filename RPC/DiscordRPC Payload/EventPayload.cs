using System;
using DiscordRPC.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordRPC.RPC.Payload
{
    internal class EventPayload : IPayload
    {
        [JsonProperty("data", NullValueHandling = (NullValueHandling)1)]
        public JObject Data { get; set; }

        [JsonProperty("evt")]
        [JsonConverter(typeof(EnumSnakeCaseConverter))]
        public ServerEvent? Event { get; set; }

        public EventPayload()
        {
            this.Data = null;
        }

        public EventPayload(long nonce) : base(nonce)
        {
            this.Data = null;
        }

        public T GetObject<T>()
        {
            bool flag = this.Data == null;
            T result;
            if (flag)
            {
                result = default(T);
            }
            else
            {
                result = this.Data.ToObject<T>();
            }
            return result;
        }

        public override string ToString()
        {
            return "Event " + base.ToString() + ", Event: " + ((this.Event != null) ? this.Event.ToString() : "N/A");
        }
    }
}
