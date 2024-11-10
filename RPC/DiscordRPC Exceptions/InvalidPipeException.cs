using System;

namespace DiscordRPC.Exceptions
{
    [Obsolete("Not Used.")]
    public class InvalidPipeException : Exception
    {
        internal InvalidPipeException(string message) : base(message)
        {
        }
    }
}
