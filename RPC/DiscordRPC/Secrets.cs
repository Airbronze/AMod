using System;
using System.Text;
using DiscordRPC.Exceptions;
using Newtonsoft.Json;

namespace DiscordRPC
{
    [Serializable]
    public class Secrets
    {
        [Obsolete("This feature has been deprecated my Mason in issue #152 on the offical library. Was originally used as a Notify Me feature, it has been replaced with Join / Spectate.")]
        [JsonProperty("match", NullValueHandling = (NullValueHandling)1)]
        public string MatchSecret
        {
            get
            {
                return this._matchSecret;
            }
            set
            {
                bool flag = !BaseRichPresence.ValidateString(value, out this._matchSecret, 128, Encoding.UTF8);
                if (flag)
                {
                    throw new StringOutOfRangeException(128);
                }
            }
        }

        [JsonProperty("join", NullValueHandling = (NullValueHandling)1)]
        public string JoinSecret
        {
            get
            {
                return this._joinSecret;
            }
            set
            {
                bool flag = !BaseRichPresence.ValidateString(value, out this._joinSecret, 128, Encoding.UTF8);
                if (flag)
                {
                    throw new StringOutOfRangeException(128);
                }
            }
        }

        [JsonProperty("spectate", NullValueHandling = (NullValueHandling)1)]
        public string SpectateSecret
        {
            get
            {
                return this._spectateSecret;
            }
            set
            {
                bool flag = !BaseRichPresence.ValidateString(value, out this._spectateSecret, 128, Encoding.UTF8);
                if (flag)
                {
                    throw new StringOutOfRangeException(128);
                }
            }
        }

        public static Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        public static int SecretLength
        {
            get
            {
                return 128;
            }
        }

        public static string CreateSecret(Random random)
        {
            byte[] array = new byte[Secrets.SecretLength];
            random.NextBytes(array);
            return Secrets.Encoding.GetString(array);
        }

        public static string CreateFriendlySecret(Random random)
        {
            string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < Secrets.SecretLength; i++)
            {
                stringBuilder.Append(text[random.Next(text.Length)]);
            }
            return stringBuilder.ToString();
        }

        private string _matchSecret;

        private string _joinSecret;

        private string _spectateSecret;
    }
}
