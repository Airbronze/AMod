using System;
using System.Linq;
using System.Text;

namespace DiscordRPC.Helper
{
    public static class StringTools
    {
        public static string GetNullOrString(this string str)
        {
            return (str.Length == 0 || string.IsNullOrEmpty(str.Trim())) ? null : str;
        }

        public static bool WithinLength(this string str, int bytes)
        {
            return str.WithinLength(bytes, Encoding.UTF8);
        }

        public static bool WithinLength(this string str, int bytes, Encoding encoding)
        {
            return encoding.GetByteCount(str) <= bytes;
        }

        public static string ToCamelCase(this string str)
        {
            bool flag = str == null;
            string result;
            if (flag)
            {
                result = null;
            }
            else
            {
                result = (from s in str.ToLowerInvariant().Split(new string[]
                {
                    "_",
                    " "
                }, StringSplitOptions.RemoveEmptyEntries)
                          select char.ToUpper(s[0]).ToString() + s.Substring(1, s.Length - 1)).Aggregate(string.Empty, (string s1, string s2) => s1 + s2);
            }
            return result;
        }

        public static string ToSnakeCase(this string str)
        {
            bool flag = str == null;
            string result;
            if (flag)
            {
                result = null;
            }
            else
            {
                string text = string.Concat(str.Select((char x, int i) => (i > 0 && char.IsUpper(x)) ? ("_" + x.ToString()) : x.ToString()).ToArray<string>());
                result = text.ToUpperInvariant();
            }
            return result;
        }
    }
}
