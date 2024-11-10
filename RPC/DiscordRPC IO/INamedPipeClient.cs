using System;
using DiscordRPC.Logging;

namespace DiscordRPC.IO
{
    public interface INamedPipeClient : IDisposable
    {
        ILogger Logger { get; set; }

        bool IsConnected { get; }

        int ConnectedPipe { get; }

        bool Connect(int pipe);

        bool ReadFrame(out PipeFrame frame);

        bool WriteFrame(PipeFrame frame);

        void Close();
    }
}
