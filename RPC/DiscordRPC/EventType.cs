using System;

namespace DiscordRPC
{
    // Token: 0x02000012 RID: 18
    [Flags]
    public enum EventType
    {
        // Token: 0x0400004E RID: 78
        None = 0,
        // Token: 0x0400004F RID: 79
        Spectate = 1,
        // Token: 0x04000050 RID: 80
        Join = 2,
        // Token: 0x04000051 RID: 81
        JoinRequest = 4
    }
}
