using Il2CppKernys.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2Cpp;
using Il2CppBasicTypes;
using UnityEngine;
using Il2CppBasicTypes;

namespace AMod
{
    internal class Utils
    {
        public static string DumpBSON(Il2CppKernys.Bson.BSONObject obj)
        {
            byte[] bytes = SimpleBSON.Dump(obj);
            BsonDocument obj2 = BsonSerializer.Deserialize<BsonDocument>(bytes, null);
            return obj2.ToJson(new JsonWriterSettings
            {
                Indent = true
            }, null, null, default(BsonSerializationArgs));
        }

        public static Vector2 ConvertMapPointToWorldPoint(Vector2i mapPoint)
        {
            float tileSizeX = ConfigData.tileSizeX;
            float tileSizeY = ConfigData.tileSizeY;

            float worldX = mapPoint.x * tileSizeX;
            float worldY = mapPoint.y * tileSizeY;

            return new Vector2(worldX, worldY);
        }
        public static List<Vector2i> GetMapPointsGridInRange(int range)
        {
            List<Vector2i> result;
            try
            {
                List<Vector2i> list = new List<Vector2i>();
                Vector2i vector2i = new Vector2i(Globals.Player.currentPlayerMapPoint.x - range, Globals.Player.currentPlayerMapPoint.y + range);
                int num = vector2i.x;
                int num2 = vector2i.y;
                for (int i = 0; i < 2 * range + 1; i++)
                {
                    for (int j = 0; j < 2 * range + 1; j++)
                    {
                        bool flag = num >= 0 && num2 >= 0 && num <= Globals.world.worldSizeX && num2 <= Globals.world.worldSizeY;
                        if (flag)
                        {
                            list.Add(new Vector2i(num, num2));
                        }
                        num++;
                    }
                    num = vector2i.x;
                    num2--;
                }
                result = list;
            }
            catch
            {
                result = null;
            }
            return result;
        }
        public static string ReadBSON(BSONObject SinglePacket, string Parent = "")
        {
            StringBuilder sb = new StringBuilder();

            foreach (string text in SinglePacket.mMap.Keys)
            {
                try
                {
                    BSONValue bsonvalue = SinglePacket[text];
                    sb.Append($"{Parent} = {text} | {bsonvalue.valueType} = ");
                    switch (bsonvalue.valueType)
                    {
                        case BSONValue.ValueType.Double:
                            sb.AppendLine(bsonvalue.doubleValue.ToString());
                            break;
                        case BSONValue.ValueType.String:
                            sb.AppendLine(bsonvalue.stringValue);
                            break;
                        case BSONValue.ValueType.Int32:
                            sb.AppendLine(bsonvalue.int32Value.ToString());
                            break;
                        case BSONValue.ValueType.Int64:
                            sb.AppendLine(bsonvalue.int64Value.ToString());
                            break;
                        case BSONValue.ValueType.Boolean:
                            sb.AppendLine(bsonvalue.boolValue.ToString());
                            break;
                        case BSONValue.ValueType.Array:
                        case BSONValue.ValueType.Binary:
                        case BSONValue.ValueType.Object:
                            sb.AppendLine($"Complex type: {bsonvalue.valueType}");
                            break;
                        case BSONValue.ValueType.None:
                        default:
                            sb.AppendLine("Unknown Type");
                            break;
                    }
                }
                catch
                {
                    sb.AppendLine($"Error processing {text}");
                }
            }
            return sb.ToString();
        }
    }
}
