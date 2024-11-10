using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiscordRPC.RPC.Payload
{
    internal class ArgumentPayload : IPayload
    {
        [JsonProperty("args", NullValueHandling = (NullValueHandling)1)]
        public JObject Arguments { get; set; }

        public ArgumentPayload()
        {
            this.Arguments = null;
        }

        public ArgumentPayload(long nonce) : base(nonce)
        {
            this.Arguments = null;
        }

        public ArgumentPayload(object args, long nonce) : base(nonce)
        {
            this.SetObject(args);
        }

        public void SetObject(object obj)
        {
            this.Arguments = JObject.FromObject(obj);
        }

        public T GetObject<T>()
        {
            return this.Arguments.ToObject<T>();
        }

        public override string ToString()
        {
            return "Argument " + base.ToString();
        }
    }
}
