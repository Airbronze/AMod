using System;
using System.Text;
using DiscordRPC.Exceptions;
using Newtonsoft.Json;

namespace DiscordRPC
{
    [Serializable]  
    public class Assets
    {
        [JsonProperty("large_image", NullValueHandling = (NullValueHandling)1)]
        public string LargeImageKey
        {
            get
            {
                return this._largeimagekey;
            }
            set
            {
                bool flag = !BaseRichPresence.ValidateString(value, out this._largeimagekey, 256, Encoding.UTF8);
                if (flag)
                {
                    throw new StringOutOfRangeException(256);
                }
                string largeimagekey = this._largeimagekey;
                this._islargeimagekeyexternal = (largeimagekey != null && largeimagekey.StartsWith("mp:external/"));
                this._largeimageID = null;
            }
        }

        [JsonIgnore]
        public bool IsLargeImageKeyExternal
        {
            get
            {
                return this._islargeimagekeyexternal;
            }
        }

        [JsonProperty("large_text", NullValueHandling = (NullValueHandling)1)]
        public string LargeImageText
        {
            get
            {
                return this._largeimagetext;
            }
            set
            {
                bool flag = !BaseRichPresence.ValidateString(value, out this._largeimagetext, 128, Encoding.UTF8);
                if (flag)
                {
                    throw new StringOutOfRangeException(128);
                }
            }
        }

        [JsonProperty("small_image", NullValueHandling = (NullValueHandling)1)]
        public string SmallImageKey
        {
            get
            {
                return this._smallimagekey;
            }
            set
            {
                bool flag = !BaseRichPresence.ValidateString(value, out this._smallimagekey, 256, Encoding.UTF8);
                if (flag)
                {
                    throw new StringOutOfRangeException(256);
                }
                string smallimagekey = this._smallimagekey;
                this._issmallimagekeyexternal = (smallimagekey != null && smallimagekey.StartsWith("mp:external/"));
                this._smallimageID = null;
            }
        }

        [JsonIgnore]
        public bool IsSmallImageKeyExternal
        {
            get
            {
                return this._issmallimagekeyexternal;
            }
        }

        [JsonProperty("small_text", NullValueHandling = (NullValueHandling)1)]
        public string SmallImageText
        {
            get
            {
                return this._smallimagetext;
            }
            set
            {
                bool flag = !BaseRichPresence.ValidateString(value, out this._smallimagetext, 128, Encoding.UTF8);
                if (flag)
                {
                    throw new StringOutOfRangeException(128);
                }
            }
        }

        [JsonIgnore]
        public ulong? LargeImageID
        {
            get
            {
                return this._largeimageID;
            }
        }

        [JsonIgnore]
        public ulong? SmallImageID
        {
            get
            {
                return this._smallimageID;
            }
        }

        internal void Merge(Assets other)
        {
            this._smallimagetext = other._smallimagetext;
            this._largeimagetext = other._largeimagetext;
            ulong value;
            bool flag = ulong.TryParse(other._largeimagekey, out value);
            if (flag)
            {
                this._largeimageID = new ulong?(value);
            }
            else
            {
                this._largeimagekey = other._largeimagekey;
                this._largeimageID = null;
            }
            ulong value2;
            bool flag2 = ulong.TryParse(other._smallimagekey, out value2);
            if (flag2)
            {
                this._smallimageID = new ulong?(value2);
            }
            else
            {
                this._smallimagekey = other._smallimagekey;
                this._smallimageID = null;
            }
        }

        private string _largeimagekey;

        private bool _islargeimagekeyexternal;

        private string _largeimagetext;

        private string _smallimagekey;

        private bool _issmallimagekeyexternal;

        private string _smallimagetext;

        private ulong? _largeimageID;

        private ulong? _smallimageID;
    }
}
