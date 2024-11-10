using System;

namespace DiscordRPC.Logging
{
    public class ConsoleLogger : ILogger
    {
        public LogLevel Level { get; set; }

        public bool Coloured { get; set; }

        [Obsolete("Use Coloured")]
        public bool Colored
        {
            get
            {
                return this.Coloured;
            }
            set
            {
                this.Coloured = value;
            }
        }

        public ConsoleLogger()
        {
            this.Level = LogLevel.Info;
            this.Coloured = false;
        }

        public ConsoleLogger(LogLevel level) : this()
        {
            this.Level = level;
        }

        public ConsoleLogger(LogLevel level, bool coloured)
        {
            this.Level = level;
            this.Coloured = coloured;
        }

        public void Trace(string message, params object[] args)
        {
            bool flag = this.Level > LogLevel.Trace;
            if (!flag)
            {
                bool coloured = this.Coloured;
                if (coloured)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                string text = "TRACE: " + message;
                bool flag2 = args.Length != 0;
                if (flag2)
                {
                    Console.WriteLine(text, args);
                }
                else
                {
                    Console.WriteLine(text);
                }
            }
        }

        public void Info(string message, params object[] args)
        {
            bool flag = this.Level > LogLevel.Info;
            if (!flag)
            {
                bool coloured = this.Coloured;
                if (coloured)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                string text = "INFO: " + message;
                bool flag2 = args.Length != 0;
                if (flag2)
                {
                    Console.WriteLine(text, args);
                }
                else
                {
                    Console.WriteLine(text);
                }
            }
        }

        public void Warning(string message, params object[] args)
        {
            bool flag = this.Level > LogLevel.Warning;
            if (!flag)
            {
                bool coloured = this.Coloured;
                if (coloured)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                string text = "WARN: " + message;
                bool flag2 = args.Length != 0;
                if (flag2)
                {
                    Console.WriteLine(text, args);
                }
                else
                {
                    Console.WriteLine(text);
                }
            }
        }

        public void Error(string message, params object[] args)
        {
            bool flag = this.Level > LogLevel.Error;
            if (!flag)
            {
                bool coloured = this.Coloured;
                if (coloured)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                string text = "ERR : " + message;
                bool flag2 = args.Length != 0;
                if (flag2)
                {
                    Console.WriteLine(text, args);
                }
                else
                {
                    Console.WriteLine(text);
                }
            }
        }
    }
}
