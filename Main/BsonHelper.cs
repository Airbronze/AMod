using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Il2CppSystem.Collections.Generic;
using HarmonyLib;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Il2CppMono.Unity;
using AMod;
using Il2Cpp;
using Il2CppKernys.Bson;
using Il2CppKernys;

namespace AMod
{
    internal static class BsonHelper
    {
        public static BSONObject FormatBson(BSONObject bson)
        {
            try
            {
                Il2CppSystem.Collections.Generic.Dictionary<string, Il2CppKernys.Bson.BSONValue>.KeyCollection keys = bson.mMap.Keys;
                foreach (string text in keys)
                {
                    string[] array = bson.mMap.Values.dictionary[text].stringValue.ToLower().Replace(':', ',').Split(new char[] { ',' });
                    bool flag = BsonHelper.sValueKeys.Contains(array[0]);
                    if (flag)
                    {
                        string text2 = array[0];
                        string text3 = text2;
                        string text4 = text3;

                        // Compute hash value using MD5
                        string hashValue = ComputeHash(text4);

                        // Convert hashValue to uint
                        uint num = ConvertToUInt(hashValue);

                        // Add your conditionals here
                        if (num <= 936371955U)
                        {
                            if (num <= 312129319U)
                            {
                                if (num != 223174175U)
                                {
                                    if (num == 312129319U)
                                    {
                                        if (text4 == "$wib")
                                        {
                                            bool flag2 = array.Length < 3;
                                            if (flag2)
                                            {
                                                throw new Exception("Invalid args, please use: \"WiB:X,Y\"");
                                            }
                                            bson.mMap.Values.dictionary[text] = Globals.world.GetWorldItemData(int.Parse(array[1]), int.Parse(array[2])).GetAsBSON();
                                        }
                                    }
                                }
                                else if (text4 == "$ik")
                                {
                                    bool flag3 = array.Length < 3;
                                    if (flag3)
                                    {
                                        throw new Exception("Invalid args, please use: \"IK:blockId,itemType\"");
                                    }
                                    bson.mMap.Values.dictionary[text] = PlayerData.InventoryKey.InventoryKeyToInt(new PlayerData.InventoryKey((World.BlockType)int.Parse(array[1]), (PlayerData.InventoryItemType)(byte)int.Parse(array[2])));
                                }
                            }
                            else if (num != 433279108U)
                            {
                                if (num == 936371955U)
                                {
                                    if (text4 == "$iid")
                                    {
                                        bool flag4 = array.Length < 2;
                                        if (flag4)
                                        {
                                            throw new Exception("Invalid args, please use: \"IiD:inventoryKey\"");
                                        }
                                        bson.mMap.Values.dictionary[text] = Globals.PlayerData.GetInventoryData(PlayerData.InventoryKey.IntToInventoryKey(int.Parse(array[1]))).GetAsBSON();
                                    }
                                }
                            }
                            else if (text4 == "$inventorykey")
                            {
                                bool flag5 = array.Length < 3;
                                if (flag5)
                                {
                                    throw new Exception("Invalid args, please use: \"InventoryKey:blockId,itemType\"");
                                }
                                bson.mMap.Values.dictionary[text] = PlayerData.InventoryKey.InventoryKeyToInt(new PlayerData.InventoryKey((World.BlockType)int.Parse(array[1]), (PlayerData.InventoryItemType)(byte)int.Parse(array[2])));
                            }
                        }
                        else if (num <= 1275578154U)
                        {
                            if (num != 1206597225U)
                            {
                                if (num == 1275578154U)
                                {
                                    if (text4 == "$time")
                                    {
                                        bson.mMap.Values.dictionary[text] = DateTime.Now.Ticks;
                                    }
                                }
                            }
                            else if (text4 == "$worlddata")
                            {
                                bool flag6 = array.Length < 3;
                                if (flag6)
                                {
                                    throw new Exception("Invalid args, please use: \"$WorldData:X,Y\"");
                                }
                                bson.mMap.Values.dictionary[text] = Globals.world.GetWorldItemData(int.Parse(array[1]), int.Parse(array[2])).GetAsBSON();
                            }
                        }
                        else if (num != 2115231304U)
                        {
                            if (num == 3552972909U)
                            {
                                if (text4 == "$inventorydata")
                                {
                                    bool flag7 = array.Length < 2;
                                    if (flag7)
                                    {
                                        throw new Exception("Invalid args, please use: \"InventoryData:inventoryKey\"");
                                    }
                                    bson.mMap.Values.dictionary[text] = Globals.PlayerData.GetInventoryData(PlayerData.InventoryKey.IntToInventoryKey(int.Parse(array[1]))).GetAsBSON();
                                }
                            }
                        }
                        else if (text4 == "$timeutc")
                        {
                            bson.mMap.Values.dictionary[text] = DateTime.UtcNow.Ticks;
                        }
                    }
                }
                return bson;
            }
            catch (Exception ex)
            {
                bool flag8 = ex.GetType() != typeof(InvalidOperationException);
                if (flag8)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return null;
        }

        public static string GetBsonAsString(BSONObject SinglePacket, string Parent = "")
        {
            StringBuilder sb = new StringBuilder();

            foreach (string Key in Keys)
            {
                try
                {
                    BSONValue Packet = SinglePacket[Key];
                    switch (Packet.valueType)
                    {
                        case BSONValue.ValueType.String:
                            sb.AppendLine($"{Parent} = {Key} | {Packet.valueType} = {Packet.stringValue}");
                            break;
                        case BSONValue.ValueType.Boolean:
                            sb.AppendLine($"{Parent} = {Key} | {Packet.valueType} = {Packet.boolValue}");
                            break;
                        case BSONValue.ValueType.Int32:
                            sb.AppendLine($"{Parent} = {Key} | {Packet.valueType} = {Packet.int32Value}");
                            break;
                        case BSONValue.ValueType.Int64:
                            sb.AppendLine($"{Parent} = {Key} | {Packet.valueType} = {Packet.int64Value}");
                            break;
                        case BSONValue.ValueType.Binary: // BSONObject
                            sb.AppendLine($"{Parent} = {Key} | {Packet.valueType}");
                            sb.Append(GetBsonAsString(SimpleBSON.Load(Packet.binaryValue), Key));
                            break;
                        case BSONValue.ValueType.Double:
                            sb.AppendLine($"{Parent} = {Key} | {Packet.valueType} = {Packet.doubleValue}");
                            break;
                        case BSONValue.ValueType.UTCDateTime:
                            sb.AppendLine($"{Parent} = {Key} | {Packet.valueType} = {Packet.dateTimeValue}");
                            break;
                        case BSONValue.ValueType.Array:
                            string value = string.Join("; ", Packet.stringListValue);
                            sb.AppendLine($"{Parent} = {Key} = {value}");
                            break;
                        default:
                            sb.AppendLine($"{Parent} = {Key} = {Packet.valueType}");
                            break;
                    }
                }
                catch
                {
                }
            }
            return sb.ToString();
        }

        private static string ComputeHash(string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private static uint ConvertToUInt(string hex)
        {
            return uint.Parse(hex.Substring(0, 8), System.Globalization.NumberStyles.HexNumber);
        }

        public static System.Collections.Generic.List<string> sValueKeys = new System.Collections.Generic.List<string>
        {
            "$time",
            "$timeutc",
            "$worlddata",
            "$wib",
            "$wiringdata",
            "$inventorydata",
            "$iib",
            "$inventorykey",
            "$ik"
        };

        public static System.Collections.Generic.Dictionary<string, BSONValue> mMap = new System.Collections.Generic.Dictionary<string, BSONValue>();

        public static System.Collections.Generic.ICollection<string> Keys => mMap.Keys;

    }
}
