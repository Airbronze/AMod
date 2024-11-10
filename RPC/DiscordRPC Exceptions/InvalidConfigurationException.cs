using System;

namespace DiscordRPC.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        internal InvalidConfigurationException(string message) : base(message)
        {
        }
    }
}
