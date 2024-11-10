using System;

namespace DiscordRPC.Exceptions
{
    public class BadPresenceException : Exception
    {
        internal BadPresenceException(string message) : base(message)
        {
        }
    }
}
