using System;
using DiscordRPC.Message;

namespace DiscordRPC.Events
{
    public delegate void OnRpcMessageEvent(object sender, IMessage msg);
}
