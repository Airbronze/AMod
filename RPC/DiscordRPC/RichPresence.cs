using System;
using Newtonsoft.Json;

namespace DiscordRPC
{
    public sealed class RichPresence : BaseRichPresence
    {
        [JsonProperty("buttons", NullValueHandling = (NullValueHandling)1)]
        public Button[] Buttons { get; set; }

        public bool HasButtons()
        {
            return this.Buttons != null && this.Buttons.Length != 0;
        }

        public RichPresence WithState(string state)
        {
            base.State = state;
            return this;
        }

        public RichPresence WithDetails(string details)
        {
            base.Details = details;
            return this;
        }

        public RichPresence WithTimestamps(Timestamps timestamps)
        {
            base.Timestamps = timestamps;
            return this;
        }

        public RichPresence WithAssets(Assets assets)
        {
            base.Assets = assets;
            return this;
        }

        public RichPresence WithParty(Party party)
        {
            base.Party = party;
            return this;
        }

        public RichPresence WithSecrets(Secrets secrets)
        {
            base.Secrets = secrets;
            return this;
        }

        public RichPresence Clone()
        {
            RichPresence richPresence = new RichPresence();
            richPresence.State = ((this._state != null) ? (this._state.Clone() as string) : null);
            richPresence.Details = ((this._details != null) ? (this._details.Clone() as string) : null);
            richPresence.Buttons = ((!this.HasButtons()) ? null : (this.Buttons.Clone() as Button[]));
            richPresence.Secrets = ((!base.HasSecrets()) ? null : new Secrets
            {
                JoinSecret = ((base.Secrets.JoinSecret != null) ? (base.Secrets.JoinSecret.Clone() as string) : null),
                SpectateSecret = ((base.Secrets.SpectateSecret != null) ? (base.Secrets.SpectateSecret.Clone() as string) : null)
            });
            BaseRichPresence baseRichPresence = richPresence;
            Timestamps timestamps2;
            if (base.HasTimestamps())
            {
                Timestamps timestamps = new Timestamps();
                timestamps.Start = base.Timestamps.Start;
                timestamps2 = timestamps;
                timestamps.End = base.Timestamps.End;
            }
            else
            {
                timestamps2 = null;
            }
            baseRichPresence.Timestamps = timestamps2;
            richPresence.Assets = ((!base.HasAssets()) ? null : new Assets
            {
                LargeImageKey = ((base.Assets.LargeImageKey != null) ? (base.Assets.LargeImageKey.Clone() as string) : null),
                LargeImageText = ((base.Assets.LargeImageText != null) ? (base.Assets.LargeImageText.Clone() as string) : null),
                SmallImageKey = ((base.Assets.SmallImageKey != null) ? (base.Assets.SmallImageKey.Clone() as string) : null),
                SmallImageText = ((base.Assets.SmallImageText != null) ? (base.Assets.SmallImageText.Clone() as string) : null)
            });
            BaseRichPresence baseRichPresence2 = richPresence;
            Party party2;
            if (base.HasParty())
            {
                Party party = new Party();
                party.ID = base.Party.ID;
                party.Size = base.Party.Size;
                party.Max = base.Party.Max;
                party2 = party;
                party.Privacy = base.Party.Privacy;
            }
            else
            {
                party2 = null;
            }
            baseRichPresence2.Party = party2;
            return richPresence;
        }

        internal RichPresence Merge(BaseRichPresence presence)
        {
            this._state = presence.State;
            this._details = presence.Details;
            base.Party = presence.Party;
            base.Timestamps = presence.Timestamps;
            base.Secrets = presence.Secrets;
            bool flag = presence.HasAssets();
            if (flag)
            {
                bool flag2 = !base.HasAssets();
                if (flag2)
                {
                    base.Assets = presence.Assets;
                }
                else
                {
                    base.Assets.Merge(presence.Assets);
                }
            }
            else
            {
                base.Assets = null;
            }
            return this;
        }

        internal override bool Matches(RichPresence other)
        {
            bool flag = !base.Matches(other);
            bool result;
            if (flag)
            {
                result = false;
            }
            else
            {
                bool flag2 = this.Buttons == null ^ other.Buttons == null;
                if (flag2)
                {
                    result = false;
                }
                else
                {
                    bool flag3 = this.Buttons != null;
                    if (flag3)
                    {
                        bool flag4 = this.Buttons.Length != other.Buttons.Length;
                        if (flag4)
                        {
                            return false;
                        }
                        for (int i = 0; i < this.Buttons.Length; i++)
                        {
                            Button button = this.Buttons[i];
                            Button button2 = other.Buttons[i];
                            bool flag5 = button.Label != button2.Label || button.Url != button2.Url;
                            if (flag5)
                            {
                                return false;
                            }
                        }
                    }
                    result = true;
                }
            }
            return result;
        }

        public static implicit operator bool(RichPresence presesnce)
        {
            return presesnce != null;
        }
    }
}
