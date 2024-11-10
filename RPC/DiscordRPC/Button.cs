using System;
using System.Text;
using DiscordRPC.Exceptions;
using Newtonsoft.Json;

namespace DiscordRPC
{
    public class Button
    {
        [JsonProperty("label")]
        public string Label
        {
            get
            {
                return this._label;
            }
            set
            {
                bool flag = !BaseRichPresence.ValidateString(value, out this._label, 32, Encoding.UTF8);
                if (flag)
                {
                    throw new StringOutOfRangeException(32);
                }
            }
        }

        [JsonProperty("url")]
        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                bool flag = !BaseRichPresence.ValidateString(value, out this._url, 512, Encoding.UTF8);
                if (flag)
                {
                    throw new StringOutOfRangeException(512);
                }
                Uri uri;
                bool flag2 = !Uri.TryCreate(this._url, UriKind.Absolute, out uri);
                if (flag2)
                {
                    throw new ArgumentException("Url must be a valid URI");
                }
            }
        }

        private string _label;

        private string _url;
    }
}
