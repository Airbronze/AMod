using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using HarmonyLib;
using Il2Cpp;
using Il2CppBasicTypes;
using Il2CppInterop.Runtime;
using Il2CppKernys.Bson;
using Il2CppTMPro;
using MelonLoader;
using Mono.CSharp;
using System.IO;
using Microsoft.Extensions.Primitives;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using System.ServiceModel.Configuration;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using System.Runtime.InteropServices;
using System.Data;
using Il2CppSystem.IO;
using Il2CppAmazon.Runtime;
using MelonLoader.TinyJSON;
using UnityEngine.TextCore;
using UnityEngine;
using AMod.PuzzleNS;
using System.Timers;

namespace AMod
{
    public class Patches
    {
        [HarmonyPatch(typeof(ConfigData), nameof(ConfigData.CanPlayerPickCollectableFromBlock))]
        private static class AntiCollect
        {
            private static bool Prefix()
            {
                return !Globals.AntiCollect;
            }
        }
        [HarmonyPatch(typeof(ConfigData), nameof(ConfigData.IsBlockHot))]
        private static class AntiBounce
        {
            private static bool Prefix(World.BlockType blockType)
            {
                return !Globals.AntiBounce;
            }
        }
        [HarmonyPatch(typeof(Player), nameof(Player.ShouldBelowBlockDoBounce))]
        private static class AntiBounce2
        {
            private static bool Prefix()
            {
                return !Globals.AntiBounce;
            }
        }
        [HarmonyPatch(typeof(ConfigData), nameof(ConfigData.IsBlockPinball))]
        private static class AntiBounce3
        {
            private static bool Prefix(World.BlockType blockType)
            {
                return !Globals.AntiBounce;
            }
        }

        [HarmonyPatch(typeof(ConfigData), nameof(ConfigData.IsBlockSpring))]
        private static class AntiBounce4
        {
            private static bool Prefix(World.BlockType blockType)
            {
                return !Globals.AntiBounce;
            }
        }
        [HarmonyPatch(typeof(ConfigData), nameof(ConfigData.IsBlockTrampolin))]
        private static class AntiBounce5
        {
            private static bool Prefix(World.BlockType blockType)
            {
                return !Globals.AntiBounce;
            }
        }
        [HarmonyPatch(typeof(ConfigData), nameof(ConfigData.IsBlockElastic))]
        private static class AntiBounce6
        {
            private static bool Prefix(World.BlockType blockType)
            {
                return !Globals.AntiBounce;
            }
        }
        [HarmonyPatch(typeof(ConfigData), nameof(ConfigData.IsBlockWind))]
        private static class AntiBounce7
        {
            private static bool Prefix(World.BlockType blockType)
            {
                return !Globals.AntiBounce;
            }
        }
        [HarmonyPatch(typeof(WorldController), nameof(WorldController.BlockColliderAndLayerHelper))]
        private static class WC_BCALH
        {
            private static void Prefix(ref World.BlockType blockType, UnityEngine.GameObject blockGameObject, Il2CppBasicTypes.Vector2i mapPoint)
            {
                if (ConfigData.IsBlockInstakill(blockType) && Globals.AntiBounce)
                {
                    blockType = World.BlockType.SoilBlock;
                }
            }
        }
        [HarmonyPatch(typeof(Player), nameof(Player.IsPlayerInMapPoint))]
        private static class BlockKill
        {
            private static bool Prefix()
            {
                bool BlockKill = Globals.BlockKill;
                if (BlockKill)
                {
                    return false;
                }
                return !Globals.BlockKill;
            }
        }
        [HarmonyPatch(typeof(ConfigData), nameof(ConfigData.IsBlockPortal))]
        private static class AntiVortex
        {
            private static bool Prefix()
            {
                bool AntiVortex = Globals.AntiVortex;
                if (AntiVortex)
                {
                    ConfigData.vortexPortalActivateDistance = 0f;
                }
                else
                {
                    ConfigData.vortexPortalActivateDistance = 0.14f;
                }
                return !Globals.AntiVortex;
            }
        }
        [HarmonyPatch(typeof(ConfigData), "ShowLoungeWorldInMainMenu")]
        internal static class LoungeByNekto
        {
            private static bool Prefix(ref bool __result, TextManager.LanguageSelection languageSelection)
            {
                if (Globals.Custompack)
                {
                    __result = true;
                }
                else
                {
                    __result = false;
                }
                return false;
            }
        }
        [HarmonyPatch(typeof(Player), nameof(Player.ActivatePortalInAnimation))]
        private static class JoinWorldFirstTime
        {
            private static void Prefix(World.BlockType blockType)
            {
                Globals.AnimationCount++;
                if (Globals.AnimationCount == 1)
                {
                    ChatUI.SendMinigameMessage("-----<BR AMod v2.2 <BR Creator: Airbronze<BR -----");
                    ChatUI.SendLogMessage("-----<BR Huge Thanks to @ne.kto, @notkrak for pathfinder, @shiuki for pathfinder, @0jepe, @JED5729, @sexyzeppelin, <BR and the AMod Community <3<BR <BR Press Tab to show/hide GUI, type /help or /ahelp to view list of commands.<BR Join our Discord Server: https://discord.gg/amod<BR -----"); // OLD LINK https://discord.gg/aKTa85hrwG has carried for generations.
                }
            }
        }

        [HarmonyPatch(typeof(Player), "DeactivatePortalAnimation")]
        private static class BypassDisconnect
        {
            private static void Postfix(Player __instance)
            {
                if (__instance == Globals.Player)
                {
                    InfoPopupUI.SetupInfoPopup("Bypassing AntiCheat..", "Credits to @0jepe & @JED5729");
                    InfoPopupUI.ForceShowMenu();
                    OutgoingMessages.SendPlayerActivateOutPortal(Globals.world.playerStartPosition);
                }
            }
        }
        
        // Jepe Patches
        
        [HarmonyPatch(typeof(WiringConfigData), "CanBlockBeBehindWiring")]
        private static class WiringConfigData_CanBlockBeBehindWiring
        {
            private static bool Prefix(ref bool __result)
            {
                __result = true;
                return false;
            }
        }

        [HarmonyPatch(typeof(DevtodevAnalytics), "TrackTrade")]
        private static class DevtodevAnalytics_TrackTrade
        {
            private static bool Prefix(string action, Il2CppStructArray<PlayerData.InventoryKey> newItems, Il2CppStructArray<PlayerData.InventoryKey> tradedItems, int addedBytecoins, int givenBytecoins)
            {
                return false;
            }
        }
        

        [HarmonyPatch(typeof(ChatUI), "NewMessage")]
        private static class Test1221121
        {
            private static bool Prefix(ref ChatMessage msg)
            {
                bool result;
                if (msg == null || msg.message == null)
                {
                result = false;
                }
                else
                {
                    if (msg.channelType == 1 || msg.channelType == 2 && !msg.message.Contains("(" + msg.channel + ")") && msg.nick != StaticPlayer.theRealPlayername)
                    {
                        msg.message = msg.message + " [" + msg.channel + "] ";
                        string ignoreGMMW = Globals.IgnoreGMW.ToString();
                        bool ignoreeGG = msg.channel.Contains(ignoreGMMW.ToLower());

                        string paragraphSeparator = "\u2029";
                        string message = msg.message.ToLower();
                        string symb1 = "<br";
                        if (message.Contains(symb1.ToLower()))
                        {
                            msg.message = msg.message.Replace("<br", "");
                        }

                        if (message.Contains(paragraphSeparator))
                        {
                            msg.message = msg.message.Replace(paragraphSeparator, "");
                        }

                        if (message.Contains("\r\n"))
                        {
                            msg.message = msg.message.Replace("\r\n", " ");
                        }
                    }

                    if (msg.channelType == 1 && !msg.message.Contains("(" + msg.channel + ")") && msg.nick != StaticPlayer.theRealPlayername)
                    {
                        string ignoreGMMW = Globals.IgnoreGMW.ToString();
                        bool ignoreeGG = msg.channel.Contains(ignoreGMMW.ToLower());

                        if (Globals.MuteGMs)
                        {
                            return false;
                        }

                        if (Globals.GWarper && msg.message.Contains(Globals.LocateGM.ToLower()) && ignoreeGG == false)
                        {
                            SceneLoader.CheckIfWeCanGoFromWorldToWorld(msg.channel, "", null, false, null);
                            Globals.GWarper = false;
                            Globals.LocateGM = "";
                            InfoPopupUI.SetupInfoPopup("GM Found!", "Warping..");
                            InfoPopupUI.ForceShowMenu();
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.EasterEggAppear);
                        }
                        string paragraphSeparator = "\u2029";
                        string message = msg.message.ToLower();
                        string symb1 = "<br";
                        if (message.Contains(symb1.ToLower()))
                        {
                            msg.message = msg.message.Replace("<br", "");
                        }

                        if (message.Contains(paragraphSeparator))
                        {
                            msg.message = msg.message.Replace(paragraphSeparator, "");
                        }

                        if (message.Contains("\r\n"))
                        {
                            msg.message = msg.message.Replace("\r\n", " ");
                        }

                    }
                    result = true;
                }
                return result;
            }
        }

        // Normal Patches

        [HarmonyPatch(typeof(NetworkClient), "HandleMessages")]
        public static class AutoMath
        {
            public static void Prefix(ref BSONObject messages)
            {
                BSONObject bsonobject = messages;
                for (int i = 0; i < bsonobject["mc"]; i++)
                {
                    BSONObject bsonobject2 = bsonobject["m" + i.ToString()].TryCast<BSONObject>();
                    string[] array = new string[]
                    {
                    "mP",
                    "ST",
                    "BcsU",
                    "GPd",
                    "p"
                    };
                    foreach (string b in array)
                    {
                        bool flag = bsonobject2["ID"].stringValue == b;
                        if (flag)
                        {
                            return;
                        }
                    }

                    try
                    {
                        BSONObject bsonobject3 = bsonobject2["CmB"].TryCast<BSONObject>();
                        if (bsonobject3["channelIndex"] == 1)
                        {
                            break;
                        }

                        if (bsonobject3["channelIndex"] == 2)
                        {
                            try
                            {
                                string stringValue = bsonobject3["channel"].stringValue;
                                MelonLogger.Msg("Sent From World: " + stringValue);
                            }
                            catch
                            {
                            }
                        }
                        Utils.ReadBSON(bsonobject3, "");
                        string stringValue2 = bsonobject3["message"].stringValue;

                        if (bsonobject3["channelIndex"] == 2)
                        {
                            try
                            {
                                string stringValue3 = bsonobject3["channel"].stringValue;
                                ChatUI.SendMinigameMessage("World: " + stringValue3 + " Message: " + stringValue2);
                            }
                            catch
                            {
                            }
                        }

                        string[] array3 = stringValue2.ToLower().Split(new char[] { ' ' }, StringSplitOptions.None);

                        string text = array3[0];

                        string[] AutoMathNum = new string[]
                        {
                        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"
                        };

                        if (Globals.AutoMath)
                        {
                            System.Random random = new System.Random();

                            foreach (string value in AutoMathNum)
                            {
                                bool flag7 = stringValue2.StartsWith(value);
                                if (flag7)
                                {
                                    bool flag8 = stringValue2.Length > 2;
                                    if (flag8)
                                    {
                                        string stringValue4 = bsonobject3["message"].stringValue;

                                        try
                                        {
                                            string text3 = stringValue4.Replace("x", "*")
                                                                       .Replace("÷", "/")
                                                                       .Replace("=", "")
                                                                       .Replace(" ", "")
                                                                       .Replace("✖", "*")
                                                                       .Replace("×", "*");

                                            double value2 = Convert.ToDouble(new DataTable().Compute(text3, null));
                                            bool flag9 = value2.ToString() == text3;
                                            if (!flag9)
                                            {
                                                BSONObject bsonobject4 = new BSONObject();
                                                bsonobject4["ID"] = "WCM";
                                                bsonobject4["msg"] = "​" + value2.ToString();

                                                BSONObject toAdd = bsonobject4;
                                                ChatUI.SendMinigameMessage($"Question: {text3} Answer: {value2}");
                                                OutgoingMessages.AddOneMessageToList(toAdd);
                                                Globals.chatUI.Submit(value2.ToString());
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        [HarmonyPatch(typeof(NetworkClient), nameof(NetworkClient.HandleMessages))]
        private static class HandleMessagesIncoming
        {
            private static bool Prefix(BSONObject messages)
            {
                bool ShouldPacketBeReceived = true;

                for (int i = 0; i < messages["mc"].int32Value; i++)
                {


                    BSONValue IncomingPacket = messages["m" + i.ToString()];
                    string IncomingPacketID = IncomingPacket["ID"].stringValue;
                    if (Globals.solvingFossils && IncomingPacketID == "MGSt")
                    {
                        Il2CppSystem.Collections.Generic.List<int> int32ListValue = IncomingPacket["MGD"].int32ListValue;
                        if (int32ListValue != null)
                        {
                            List<int> list = new List<int>();
                            for (int j = 0; j < int32ListValue.Count - 1; j++)
                            {
                                list.Add(int32ListValue[j]);
                            }
                            int[] array = list.ToArray();
                            Puzzle startPuzzle = new Puzzle(array);
                            PuzzleSolver puzzleSolver = new PuzzleSolver();
                            List<Puzzle> boards = puzzleSolver.SolvePuzzle(startPuzzle);
                            Globals.solvingSteps = puzzleSolver.FindMoves(boards);
                        }
                    }
                    bool nigg = Globals.IncomingBlock.Contains(IncomingPacketID);

                    if (Globals.IncomingBlock.Contains(IncomingPacketID))
                    {
                        ShouldPacketBeReceived = false;
                    }

                    if (Globals.ignoreFwk && IncomingPacketID == "Fwk")
                    {
                        ShouldPacketBeReceived = false;
                    }
                    if (IncomingPacketID == "OoIP")
                    {
                        Globals.worldIP = IncomingPacket["IP"].stringValue;
                    }

                    if (IncomingPacketID == "TTjW" && Globals.JoinRandomWorlds)
                    {
                        SceneLoader.CheckIfWeCanGoFromWorldToWorld(IncomingPacket["WN"].stringValue, "", null, false, null);
                    }

                    if (Globals.cmIncoming && IncomingPacketID != "mP" && IncomingPacketID != "PSicU" && IncomingPacketID != "ST" && IncomingPacketID != "p")
                    {
                        Globals.CurrentCaptureINCOMING = Utils.DumpBSON(IncomingPacket.Cast<BSONObject>());
                        Console.WriteLine("\r\n \r\n" + Utils.DumpBSON(IncomingPacket.Cast<BSONObject>()) + "\r\n \r\n");
                    }

                    if (IncomingPacketID == "BcsU")
                    {
                        Globals.AudioManager.PlayMusic(23);
                    }

                    if (IncomingPacket.ContainsKey("U"))
                    {
                        if (IncomingPacketID != "mP" && Globals.PWStaff.ContainsKey(IncomingPacket["U"].stringValue))
                        {
                            Globals.DoCustomNotification("Mod / Admin Found with ID: [ " + IncomingPacket["U"].stringValue + " ]  Username: " + Globals.PWStaff[IncomingPacket["U"]], Globals.CurrentMapPoint);
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.NetherBossWraithShieldAppear);
                            ChatUI.SendMinigameMessage("Mod / Admin found with ID  " + IncomingPacket["U"].stringValue + "  Username:  " + Globals.PWStaff[IncomingPacket["U"]]);
                            ChatUI.SendMinigameMessage("Mod / Admin detected with Packet: " + IncomingPacket["ID"].stringValue);
                            MelonLogger.Msg("Mod / Admin " + Globals.PWStaff[IncomingPacket["U"]] + "(ID: " + IncomingPacket["U"] + ") " + "Detected with Packet: " + IncomingPacket["ID"].stringValue);

                            if (Globals.LeaveOnDetect)
                            {
                                OutgoingMessages.LeaveWorld();
                                MelonLogger.Msg("Mod / Admin Detected! \r\n ID: [ " + IncomingPacket["U"].stringValue + " ] \r\n Username: " + Globals.PWStaff[IncomingPacket["U"]] + "\r\n Left world because you enabled LeaveOnDetect!");
                            }
                        }
                    }
                    /*if (IncomingPacketID == "Recall")
                    {
                        if (IncomingPacket["RecallBT"].int32Value == 4367)
                        {
                            OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.MountFlyingBathtub);
                            Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.MountFlyingBathtub);
                        }
                    }*/

// removed ts so developers arent lazy to make anticheat lol

                    if (Globals.NiceTry && IncomingPacketID == "Fwk")
                    {
                        string playerID = IncomingPacket["U"].stringValue;
                        OutgoingMessages.AddOneMessageToList(new BSONObject()
                        {
                            ["ID"] = "BPl",
                            ["U"] = playerID,
                        });
                    }

                    if (Globals.KFwk && IncomingPacketID == "Fwk")
                    {
                        string playerID = IncomingPacket["U"].stringValue;
                        OutgoingMessages.AddOneMessageToList(new BSONObject()
                        {
                            ["ID"] = "KPl",
                            ["U"] = playerID,
                        });
                    }

                    if (IncomingPacket.ContainsKey("ID"))
                    {
                        if (Globals.CaptureIncomingID && IncomingPacketID != "mP" && IncomingPacketID != "PSicU")
                        {
                            Globals.CurrentCaptureINCOMING = "{ \r\n" + IncomingPacket["ID"].stringValue + "\r\n }";
                        }
                    }

                    if (Globals.AutoBuy1 && IncomingPacketID == "BIPack")
                    {
                        Globals.IPID1 = IncomingPacket["IPId"].stringValue;

                        if (IncomingPacket.ContainsKey("ER"))
                        {
                            string ERValue1 = IncomingPacket["ER"].stringValue;
                            MelonLogger.Msg("Error on AutoBuy:" + ERValue1);
                            Globals.AutoBuy1 = false;
                            Globals.IPID1 = "";
                        }
                    }

                    if (Globals.LogAllPlayers && IncomingPacketID != "mP" && IncomingPacket.ContainsKey("U"))
                    {
                        Globals.DoCustomNotification("Player action logged! \r\n Incoming Packet ID: " + IncomingPacket["ID"], Globals.CurrentMapPoint);
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.NetherBossWraithShieldAppear);
                        ChatUI.SendMinigameMessage(NetworkPlayers.GetNameWithId(IncomingPacket["U"]) + "'s action logged with Incoming Packet ID: " + IncomingPacket["ID"]);
                        MelonLogger.Msg(NetworkPlayers.GetNameWithId(IncomingPacket["U"]) + "'s action logged with Incoming Packet ID: " + IncomingPacket["ID"]);
                    }

                    if (Globals.HardDetect && IncomingPacket.ContainsKey("U"))
                    {
                        if (Globals.PWStaff.ContainsKey(IncomingPacket["U"].stringValue))
                        {
                            Globals.DoCustomNotification("Mod / Admin Found with ID: [ " + IncomingPacket["U"].stringValue + " ]", Globals.CurrentMapPoint);
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.NetherBossWraithShieldAppear);
                            ChatUI.SendMinigameMessage("Mod / Admin found with ID  " + IncomingPacket["U"].stringValue + "  Username:  " + Globals.PWStaff[IncomingPacket["ID"]]);
                            MelonLogger.Msg("Mod Detected with Packet: " + IncomingPacket["ID"].stringValue + "  Name: " + Globals.PWStaff[IncomingPacket["ID"]]);
                        }
                    }

                    if (Globals.AntiRetard && IncomingPacketID == "AnP" && IncomingPacket.ContainsKey("U"))
                    {
                        if (Globals.Retards.ContainsKey(IncomingPacket["U"].stringValue))
                        {
                            Globals.DoCustomNotification("Retard found! Username: " + NetworkPlayers.GetNameWithId(IncomingPacket["U"]), Globals.CurrentMapPoint);
                            OutgoingMessages.BanAndKickPlayer(IncomingPacket["U"].stringValue);
                            OutgoingMessages.KickPlayer(IncomingPacket["U"].stringValue);
                            ChatUI.SendMinigameMessage("Retard found! Banned " + NetworkPlayers.GetNameWithId(IncomingPacket["U"]) + " from the world!");
                        }
                    }
                }
                return ShouldPacketBeReceived;
            }
        }

        [HarmonyPatch(typeof(OutgoingMessages), nameof(OutgoingMessages.AddOneMessageToList))]
        private static class OutgoingPackets
        {
            private static bool Prefix(BSONObject toAdd)
            {
                bool ShouldPacketBeSent = true;
                string OutgoingPacketID = toAdd["ID"].stringValue;
                if (Globals.OutgoingBlock.Contains(OutgoingPacketID))
                {
                    ShouldPacketBeSent = false;
                }

                if (Globals.refOnCollect && OutgoingPacketID == "C")
                {  
                    OutgoingMessages.AddOneMessageToList(new BSONObject()
                    {
                        ["ID"] = "AGI",
                        ["PT"] = 0
                    });
                }

                if (OutgoingPacketID == "SetMinR")
                {
                    Vector2i mapKey = new Vector2i();
                    mapKey.x = toAdd["x"].int32Value;
                    mapKey.y = toAdd["y"].int32Value;
                    WorldItemBase worldIB = Globals.world.GetWorldItemData(mapKey);
                    BSONObject getWIB = worldIB.GetAsBSON();

                    toAdd["WiB"] = getWIB;
                    if (getWIB["blockType"] != 3604)
                    {
                        ShouldPacketBeSent = false;
                        Globals.DoCustomNotification("AutoBan prevented!", Globals.Player.currentPlayerMapPoint);
                    }

                    if (getWIB["blockType"] != 3639)
                    {
                        ShouldPacketBeSent = false;
                        Globals.DoCustomNotification("AutoBan prevented!", Globals.Player.currentPlayerMapPoint);
                        ChatUI.SendMinigameMessage("Packet " + toAdd["ID"].stringValue + " was not sent. Reason; AUTOBAN.");
                    }
                }

                if (OutgoingPacketID == "WP")
                {
                    if (toAdd["U"] == Globals.PlayerData.playerId)
                    {
                        ShouldPacketBeSent = false;
                        Globals.DoCustomNotification("AutoBan prevented!", Globals.Player.currentPlayerMapPoint);
                        ChatUI.SendMinigameMessage("Packet " + toAdd["ID"].stringValue + " was not sent. Reason; AUTOBAN.");
                    }
                }


                if (Globals.AutoBanPackets.Contains(toAdd["ID"].stringValue))
                {
                    ShouldPacketBeSent = false;
                }
                
                if (OutgoingPacketID == "PAiP")
                {
                    ShouldPacketBeSent = false;
                }

                if (Globals.AutoBanPackets.Contains(OutgoingPacketID))
                {
                    ShouldPacketBeSent = false;
                }

                if (Globals.AutoVendor && toAdd["ID"].stringValue == "PVi")
                {
                    Globals.VendCID = toAdd["vC"].int32Value;
                    Globals.VendID = toAdd["vI"].int32Value;
                    Globals.VendIK = toAdd["IK"].int32Value;
                }

                if (Globals.GetOpen && toAdd["ID"].stringValue == "OpenPresent")
                {
                    Globals.AOINVK = toAdd["IK"].int32Value;
                    Globals.GetOpen = false;
                }

                if (Globals.cmOutgoing && OutgoingPacketID != "PSicU" && OutgoingPacketID != "" && OutgoingPacketID != "")
                {
                    Globals.CurrentCaptureOUTGOING = Utils.DumpBSON(toAdd);
                    Console.WriteLine("\r\n \r\n" + Utils.DumpBSON(toAdd) + "\r\n \r\n");
                }
                if (Globals.captureCard && toAdd["ID"].stringValue == "DCard")
                {
                    InfoPopupUI.SetupInfoPopup("Disenchanter started!", "");
                    InfoPopupUI.ForceShowMenu();
                    Globals.cardID = toAdd["ccD"].int32Value;
                    Globals.cardAmt = toAdd["Amt"].int32Value;
                    Globals.captureCard = false;
                }
                if (Globals.autoGM && toAdd["ID"].stringValue == "GM")
                {
                    Globals.Base64Msg = toAdd["msg"].stringValue;
                    ChatUI.SendMinigameMessage("=====<br [AUTOGM]: Base64 Msg Acquired!<br =====");
                    Globals.autoGM = false;
                    Console.WriteLine(Globals.Base64Msg);
                }

                return ShouldPacketBeSent;
            }
        }
    }
}
