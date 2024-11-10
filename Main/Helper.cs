using System;
using System.Text;
using Il2CppKernys.Bson;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;


namespace AMod
{
    public class Helper
    {
        public static string GenerateRandomString(int Lenght)
        {
            string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_^{[]}";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < Lenght; i++)
            {
                stringBuilder.Append(text[random.Next(text.Length)]);
            }
            return stringBuilder.ToString();
        }

        public static string DumpBSON(BSONObject _BSONObject)
        {
            byte[] array = SimpleBSON.Dump(_BSONObject);
            BsonDocument bsonDocument = BsonSerializer.Deserialize<BsonDocument>(array, null);
            return "\n" + BsonExtensionMethods.ToJson<BsonDocument>(bsonDocument, new JsonWriterSettings
            {
                Indent = true
            }, null, null, default(BsonSerializationArgs));
        }

        public static long FixTicks(long Tick)
        {
            string text = Convert.ToString(Tick);
            text = text.Remove(11) + "2468010";
            return Convert.ToInt64(text);
        }
    }
}