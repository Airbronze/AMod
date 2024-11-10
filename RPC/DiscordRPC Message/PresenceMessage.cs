using System;

namespace DiscordRPC.Message
{
    public class PresenceMessage : IMessage
    {
        public override MessageType Type
        {
            get
            {
                return MessageType.PresenceUpdate;
            }
        }

        internal PresenceMessage() : this(null)
        {
        }

        internal PresenceMessage(RichPresenceResponse rpr)
        {
            bool flag = rpr == null;
            if (flag)
            {
                this.Presence = null;
                this.Name = "No Rich Presence";
                this.ApplicationID = "";
            }
            else
            {
                this.Presence = rpr;
                this.Name = rpr.Name;
                this.ApplicationID = rpr.ClientID;
            }
        }

        public BaseRichPresence Presence { get; internal set; }

        public string Name { get; internal set; }

        public string ApplicationID { get; internal set; }
    }
}
