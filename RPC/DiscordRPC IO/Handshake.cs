using System;
using Newtonsoft.Json;

namespace DiscordRPC.IO
{
    internal class Handshake
    {
        [JsonProperty("v")]
        public int Version { get; set; }

        [JsonProperty("client_id")]
        public string ClientID { get; set; }
    }
}
