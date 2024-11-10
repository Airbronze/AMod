using System;

namespace DiscordRPC.Message
{
    public abstract class IMessage
    {
        public abstract MessageType Type { get; }

        public DateTime TimeCreated
        {
            get
            {
                return this._timecreated;
            }
        }

        public IMessage()
        {
            this._timecreated = DateTime.Now;
        }

        private DateTime _timecreated;
    }
}
