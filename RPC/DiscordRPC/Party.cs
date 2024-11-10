using System;
using System.Text.Json.Serialization;
using DiscordRPC.Helper;
using Newtonsoft.Json;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace DiscordRPC
{
    [Serializable]
    public class Party
    {
        [JsonProperty("id", NullValueHandling = (NullValueHandling)1)]
        public string ID
        {
            get
            {
                return this._partyid;
            }
            set
            {
                this._partyid = value.GetNullOrString();
            }
        }

        [JsonIgnore]
        public int Size { get; set; }

        [JsonIgnore]
        public int Max { get; set; }

        [JsonProperty("privacy", NullValueHandling = 0, DefaultValueHandling = 0)]
        public Party.PrivacySetting Privacy { get; set; }

        [JsonProperty("size", NullValueHandling = (NullValueHandling)1)]
        private int[] _size
        {
            get
            {
                int num = Math.Max(1, this.Size);
                return new int[]
                {
                    num,
                    Math.Max(num, this.Max)
                };
            }
            set
            {
                bool flag = value.Length != 2;
                if (flag)
                {
                    this.Size = 0;
                    this.Max = 0;
                }
                else
                {
                    this.Size = value[0];
                    this.Max = value[1];
                }
            }
        }

        private string _partyid;

        public enum PrivacySetting
        {
            Private,
            Public
        }
    }
}
