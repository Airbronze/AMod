using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMod
{
    internal class ChatUtils
    {
        public static void Error(string message)
        {
            ChatMessage msg = new ChatMessage(message + "</color>", Il2CppSystem.DateTime.Now, ChatMessage.ChannelTypes.SERVER_MESSAGE, "<B><#ff2919>[<#f25449>ERROR<#ff2919>]</color></color></color></B>", "", "");
            ControllerHelper.chatUI.NewMessage(msg);
        }
        public static void D(string message)
        {
            ChatMessage msg = new ChatMessage(message + "</color>", Il2CppSystem.DateTime.Now, ChatMessage.ChannelTypes.SERVER_MESSAGE, "<B><#707070>[<#808080>DEBUG<#707070>]</color></color></color></B>", "", "");
            ControllerHelper.chatUI.NewMessage(msg);
        }
        public static void ParseMsg(string message)
        {
            ChatMessage msg = new ChatMessage(message + "</color>", Il2CppSystem.DateTime.Now, ChatMessage.ChannelTypes.SERVER_MESSAGE, "<B><#707070>[<#808080>TOOLS<#707070>]</color></color></color></B>", "", "");
            ControllerHelper.chatUI.NewMessage(msg);
        }

        public static void Msg(string message)
        {
            ChatMessage msg = new ChatMessage(message + "</color>", Il2CppSystem.DateTime.Now, ChatMessage.ChannelTypes.SERVER_MESSAGE, "<B><#707070>[<#808080>AMOD<#707070>]</color></color></color></B>", "", "");
            ControllerHelper.chatUI.NewMessage(msg);
        }
        public static void GM(string message)
        {
            ChatMessage msg = new ChatMessage(message + "</color>", Il2CppSystem.DateTime.Now, ChatMessage.ChannelTypes.SERVER_MESSAGE, "<B><#707070>[<#f5e642>AMOD<#707070>]</color></color></color></B>", "", "");
            ControllerHelper.chatUI.NewMessage(msg);
        }
    }
}
