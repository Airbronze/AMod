using System;
using Newtonsoft.Json;

namespace DiscordRPC
{
    public class User
    {
        [JsonProperty("id")]
        public ulong ID { get; private set; }

        [JsonProperty("username")]
        public string Username { get; private set; }

        [JsonProperty("discriminator")]
        [Obsolete("Discord no longer uses discriminators.")]
        public int Discriminator { get; private set; }

        [JsonProperty("global_name")]
        public string DisplayName { get; private set; }

        [JsonProperty("avatar")]
        public string Avatar { get; private set; }

        [JsonProperty("flags", NullValueHandling = (NullValueHandling)1)]
        public User.Flag Flags { get; private set; }

        [JsonProperty("premium_type", NullValueHandling = (NullValueHandling)1)]
        public User.PremiumType Premium { get; private set; }

        public string CdnEndpoint { get; private set; }

        internal User()
        {
            this.CdnEndpoint = "cdn.discordapp.com";
        }

        internal void SetConfiguration(Configuration configuration)
        {
            this.CdnEndpoint = configuration.CdnHost;
        }

        public string GetAvatarURL(User.AvatarFormat format)
        {
            return this.GetAvatarURL(format, User.AvatarSize.x128);
        }

        public string GetAvatarURL(User.AvatarFormat format, User.AvatarSize size)
        {
            string text = string.Format("/avatars/{0}/{1}", this.ID, this.Avatar);
            bool flag = string.IsNullOrEmpty(this.Avatar);
            if (flag)
            {
                bool flag2 = format > User.AvatarFormat.PNG;
                if (flag2)
                {
                    throw new BadImageFormatException("The user has no avatar and the requested format " + format.ToString() + " is not supported. (Only supports PNG).");
                }
                int num = (int)((this.ID >> 22) % 6UL);
                bool flag3 = this.Discriminator > 0;
                if (flag3)
                {
                    num = this.Discriminator % 5;
                }
                text = string.Format("/embed/avatars/{0}", num);
            }
            return string.Format("https://{0}{1}{2}?size={3}", new object[]
            {
                this.CdnEndpoint,
                text,
                this.GetAvatarExtension(format),
                (int)size
            });
        }

        public string GetAvatarExtension(User.AvatarFormat format)
        {
            return "." + format.ToString().ToLowerInvariant();
        }

        public override string ToString()
        {
            bool flag = !string.IsNullOrEmpty(this.DisplayName);
            string result;
            if (flag)
            {
                result = this.DisplayName;
            }
            else
            {
                bool flag2 = this.Discriminator != 0;
                if (flag2)
                {
                    result = this.Username + "#" + this.Discriminator.ToString("D4");
                }
                else
                {
                    result = this.Username;
                }
            }
            return result;
        }

        public enum AvatarFormat
        {
            PNG,
            JPEG,
            WebP,
            GIF
        }

        public enum AvatarSize
        {
            x16 = 16,
            x32 = 32,
            x64 = 64,
            x128 = 128,
            x256 = 256,
            x512 = 512,
            x1024 = 1024,
            x2048 = 2048
        }

        [Flags]
        public enum Flag
        {
            None = 0,
            Employee = 1,
            Partner = 2,
            HypeSquad = 4,
            BugHunter = 8,
            HouseBravery = 64,
            HouseBrilliance = 128,
            HouseBalance = 256,
            EarlySupporter = 512,
            TeamUser = 1024
        }

        public enum PremiumType
        {
            None,
            NitroClassic,
            Nitro
        }
    }
}
