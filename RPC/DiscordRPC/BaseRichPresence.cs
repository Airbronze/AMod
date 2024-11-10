using System;
using System.Text;
using DiscordRPC.Exceptions;
using DiscordRPC.Helper;
using Newtonsoft.Json;

namespace DiscordRPC
{
    [JsonObject(MemberSerialization = (MemberSerialization)1)]
    [Serializable]
    public class BaseRichPresence
    {
        [JsonProperty("state", NullValueHandling = (NullValueHandling)1)]
        public string State
        {
            get
            {
                return this._state;
            }
            set
            {
                bool flag = !BaseRichPresence.ValidateString(value, out this._state, 128, Encoding.UTF8);
                if (flag)
                {
                    throw new StringOutOfRangeException("State", 0, 128);
                }
            }
        }

        [JsonProperty("details", NullValueHandling = (NullValueHandling)1)]
        public string Details
        {
            get
            {
                return this._details;
            }
            set
            {
                bool flag = !BaseRichPresence.ValidateString(value, out this._details, 128, Encoding.UTF8);
                if (flag)
                {
                    throw new StringOutOfRangeException(128);
                }
            }
        }

        [JsonProperty("timestamps", NullValueHandling = (NullValueHandling)1)]
        public Timestamps Timestamps { get; set; }

        [JsonProperty("assets", NullValueHandling = (NullValueHandling)1)]
        public Assets Assets { get; set; }

        [JsonProperty("party", NullValueHandling = (NullValueHandling)1)]
        public Party Party { get; set; }

        [JsonProperty("secrets", NullValueHandling = (NullValueHandling)1)]
        public Secrets Secrets { get; set; }

        [JsonProperty("instance", NullValueHandling = (NullValueHandling)1)]
        [Obsolete("This was going to be used, but was replaced by JoinSecret instead")]
        private bool Instance { get; set; }

        public bool HasTimestamps()
        {
            return this.Timestamps != null && (this.Timestamps.Start != null || this.Timestamps.End != null);
        }

        public bool HasAssets()
        {
            return this.Assets != null;
        }

        public bool HasParty()
        {
            return this.Party != null && this.Party.ID != null;
        }

        public bool HasSecrets()
        {
            return this.Secrets != null && (this.Secrets.JoinSecret != null || this.Secrets.SpectateSecret != null);
        }

        internal static bool ValidateString(string str, out string result, int bytes, Encoding encoding)
        {
            result = str;
            bool flag = str == null;
            bool result2;
            if (flag)
            {
                result2 = true;
            }
            else
            {
                string str2 = str.Trim();
                bool flag2 = !str2.WithinLength(bytes, encoding);
                if (flag2)
                {
                    result2 = false;
                }
                else
                {
                    result = str2.GetNullOrString();
                    result2 = true;
                }
            }
            return result2;
        }

        public static implicit operator bool(BaseRichPresence presesnce)
        {
            return presesnce != null;
        }

        internal virtual bool Matches(RichPresence other)
        {
            bool flag = other == null;
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.State != other.State || this.Details != other.Details;
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    bool flag3 = this.Timestamps != null;
                    if (flag3)
                    {
                        bool flag4;
                        if (other.Timestamps != null)
                        {
                            ulong? num = other.Timestamps.StartUnixMilliseconds;
                            ulong? num2 = this.Timestamps.StartUnixMilliseconds;
                            if (num.GetValueOrDefault() == num2.GetValueOrDefault() & num != null == (num2 != null))
                            {
                                num2 = other.Timestamps.EndUnixMilliseconds;
                                num = this.Timestamps.EndUnixMilliseconds;
                                flag4 = !(num2.GetValueOrDefault() == num.GetValueOrDefault() & num2 != null == (num != null));
                                goto IL_D9;
                            }
                        }
                        flag4 = true;
                    IL_D9:
                        bool flag5 = flag4;
                        if (flag5)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        bool flag6 = other.Timestamps != null;
                        if (flag6)
                        {
                            return false;
                        }
                    }
                    bool flag7 = this.Secrets != null;
                    if (flag7)
                    {
                        bool flag8 = other.Secrets == null || other.Secrets.JoinSecret != this.Secrets.JoinSecret || other.Secrets.MatchSecret != this.Secrets.MatchSecret || other.Secrets.SpectateSecret != this.Secrets.SpectateSecret;
                        if (flag8)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        bool flag9 = other.Secrets != null;
                        if (flag9)
                        {
                            return false;
                        }
                    }
                    bool flag10 = this.Party != null;
                    if (flag10)
                    {
                        bool flag11 = other.Party == null || other.Party.ID != this.Party.ID || other.Party.Max != this.Party.Max || other.Party.Size != this.Party.Size || other.Party.Privacy != this.Party.Privacy;
                        if (flag11)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        bool flag12 = other.Party != null;
                        if (flag12)
                        {
                            return false;
                        }
                    }
                    bool flag13 = this.Assets != null;
                    if (flag13)
                    {
                        bool flag14 = other.Assets == null || other.Assets.LargeImageKey != this.Assets.LargeImageKey || other.Assets.LargeImageText != this.Assets.LargeImageText || other.Assets.SmallImageKey != this.Assets.SmallImageKey || other.Assets.SmallImageText != this.Assets.SmallImageText;
                        if (flag14)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        bool flag15 = other.Assets != null;
                        if (flag15)
                        {
                            return false;
                        }
                    }
                    result = (this.Instance == other.Instance);
                }
            }
            return result;
        }

        public RichPresence ToRichPresence()
        {
            RichPresence richPresence = new RichPresence();
            richPresence.State = this.State;
            richPresence.Details = this.Details;
            richPresence.Party = ((!this.HasParty()) ? this.Party : null);
            richPresence.Secrets = ((!this.HasSecrets()) ? this.Secrets : null);
            bool flag = this.HasAssets();
            if (flag)
            {
                richPresence.Assets = new Assets
                {
                    SmallImageKey = this.Assets.SmallImageKey,
                    SmallImageText = this.Assets.SmallImageText,
                    LargeImageKey = this.Assets.LargeImageKey,
                    LargeImageText = this.Assets.LargeImageText
                };
            }
            bool flag2 = this.HasTimestamps();
            if (flag2)
            {
                richPresence.Timestamps = new Timestamps();
                bool flag3 = this.Timestamps.Start != null;
                if (flag3)
                {
                    richPresence.Timestamps.Start = this.Timestamps.Start;
                }
                bool flag4 = this.Timestamps.End != null;
                if (flag4)
                {
                    richPresence.Timestamps.End = this.Timestamps.End;
                }
            }
            return richPresence;
        }

        protected internal string _state;

        protected internal string _details;
    }
}
