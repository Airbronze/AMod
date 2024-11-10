using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace DiscordRPC.Converters
{
    internal class EnumSnakeCaseConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            bool flag = reader.Value == null;
            object result;
            if (flag)
            {
                result = null;
            }
            else
            {
                object obj = null;
                bool flag2 = this.TryParseEnum(objectType, (string)reader.Value, out obj);
                if (flag2)
                {
                    result = obj;
                }
                else
                {
                    result = existingValue;
                }
            }
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Type type = value.GetType();
            string text = Enum.GetName(type, value);
            MemberInfo[] members = type.GetMembers(BindingFlags.Static | BindingFlags.Public);
            foreach (MemberInfo memberInfo in members)
            {
                bool flag = memberInfo.Name.Equals(text);
                if (flag)
                {
                    object[] customAttributes = memberInfo.GetCustomAttributes(typeof(EnumValueAttribute), true);
                    bool flag2 = customAttributes.Length != 0;
                    if (flag2)
                    {
                        text = ((EnumValueAttribute)customAttributes[0]).Value;
                    }
                }
            }
            writer.WriteValue(text);
        }

        public bool TryParseEnum(Type enumType, string str, out object obj)
        {
            bool flag = str == null;
            bool result;
            if (flag)
            {
                obj = null;
                result = false;
            }
            else
            {
                Type type = enumType;
                bool flag2 = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
                if (flag2)
                {
                    type = type.GetGenericArguments().First<Type>();
                }
                bool flag3 = !type.IsEnum;
                if (flag3)
                {
                    obj = null;
                    result = false;
                }
                else
                {
                    MemberInfo[] members = type.GetMembers(BindingFlags.Static | BindingFlags.Public);
                    foreach (MemberInfo memberInfo in members)
                    {
                        object[] customAttributes = memberInfo.GetCustomAttributes(typeof(EnumValueAttribute), true);
                        foreach (object obj2 in customAttributes)
                        {
                            EnumValueAttribute enumValueAttribute = (EnumValueAttribute)obj2;
                            bool flag4 = str.Equals(enumValueAttribute.Value);
                            if (flag4)
                            {
                                obj = Enum.Parse(type, memberInfo.Name, true);
                                return true;
                            }
                        }
                    }
                    obj = null;
                    result = false;
                }
            }
            return result;
        }
    }
}
