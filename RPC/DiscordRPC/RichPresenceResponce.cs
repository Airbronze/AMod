using System;
using Newtonsoft.Json;

namespace DiscordRPC
{
    // Token: 0x0200001A RID: 26
    internal sealed class RichPresenceResponse : BaseRichPresence
    {
        // Token: 0x1700004E RID: 78
        // (get) Token: 0x0600012B RID: 299 RVA: 0x00007057 File Offset: 0x00005257
        // (set) Token: 0x0600012C RID: 300 RVA: 0x0000705F File Offset: 0x0000525F
        [JsonProperty("application_id")]
        public string ClientID { get; private set; }

        // Token: 0x1700004F RID: 79
        // (get) Token: 0x0600012D RID: 301 RVA: 0x00007068 File Offset: 0x00005268
        // (set) Token: 0x0600012E RID: 302 RVA: 0x00007070 File Offset: 0x00005270
        [JsonProperty("name")]
        public string Name { get; private set; }
    }
}
