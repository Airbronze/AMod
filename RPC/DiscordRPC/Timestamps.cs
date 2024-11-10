using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace DiscordRPC
{
    [Serializable]
    public class Timestamps
    {
        public static Timestamps Now
        {
            get
            {
                return new Timestamps(DateTime.UtcNow);
            }
        }

        public static Timestamps FromTimeSpan(double seconds)
        {
            return Timestamps.FromTimeSpan(TimeSpan.FromSeconds(seconds));
        }

        public static Timestamps FromTimeSpan(TimeSpan timespan)
        {
            return new Timestamps
            {
                Start = new DateTime?(DateTime.UtcNow),
                End = new DateTime?(DateTime.UtcNow + timespan)
            };
        }

        [JsonIgnore]
        public DateTime? Start { get; set; }

        [JsonIgnore]
        public DateTime? End { get; set; }

        public Timestamps()
        {
            this.Start = null;
            this.End = null;
        }

        public Timestamps(DateTime start)
        {
            this.Start = new DateTime?(start);
            this.End = null;
        }

        public Timestamps(DateTime start, DateTime end)
        {
            this.Start = new DateTime?(start);
            this.End = new DateTime?(end);
        }

        [JsonProperty("start", NullValueHandling = (NullValueHandling)1)]
        public ulong? StartUnixMilliseconds
        {
            get
            {
                return (this.Start != null) ? new ulong?(Timestamps.ToUnixMilliseconds(this.Start.Value)) : null;
            }
            set
            {
                this.Start = ((value != null) ? new DateTime?(Timestamps.FromUnixMilliseconds(value.Value)) : null);
            }
        }

        [JsonProperty("end", NullValueHandling = (NullValueHandling)1)]
        public ulong? EndUnixMilliseconds
        {
            get
            {
                return (this.End != null) ? new ulong?(Timestamps.ToUnixMilliseconds(this.End.Value)) : null;
            }
            set
            {
                this.End = ((value != null) ? new DateTime?(Timestamps.FromUnixMilliseconds(value.Value)) : null);
            }
        }

        public static DateTime FromUnixMilliseconds(ulong unixTime)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddMilliseconds(Convert.ToDouble(unixTime));
        }

        public static ulong ToUnixMilliseconds(DateTime date)
        {
            DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToUInt64((date - d).TotalMilliseconds);
        }
    }
}
