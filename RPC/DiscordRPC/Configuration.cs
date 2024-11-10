using System;
using Newtonsoft.Json;

namespace DiscordRPC
{
    public class Configuration
    {
        [JsonProperty("api_endpoint")]
        public string ApiEndpoint { get; set; }

        [JsonProperty("cdn_host")]
        public string CdnHost { get; set; }

        [JsonProperty("environment")]
        public string Environment { get; set; }
    }
}
