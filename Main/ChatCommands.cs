using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Razor.Parser.SyntaxTree;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Boo.Lang.Compiler.TypeSystem.Internal;
using HarmonyLib;
using Il2Cpp;
using Il2CppBasicTypes;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppKernys.Bson;
using Il2CppSystem;
using Il2CppSystem.Threading.Tasks;
using MelonLoader;
using Mono.CSharp;
using UnityEngine;
using static Il2Cpp.PlayerData;
using static Il2Cpp.World;

namespace AMod
{
    internal class ChatCommands
    {
        public static World world;
        private static int totalgems;

        [HarmonyPatch(typeof(ChatUI), "Submit")]
        
        private static class CUI_SUBMIT
        {
            private static bool Prefix(ref string text)
            {
                string[] Arguments = text.Split(' ');
                if (Arguments[0] == "/credits") // DLL Ver Mentioned 5
                {
                    ChatUI.SendMinigameMessage("-----<BR AMod v2.2 <BR Creator: Airbronze<BR -----");
                    ChatUI.SendLogMessage("-----<BR Huge Thanks to Jepe, Nekto, JED5729, Kelasponssaa , Zeppelin, Krak, Shiuki<BR <3<BR <BR Press Tab to show/hide GUI, type /help or /ahelp to view list of commands.<BR Join our Discord Server: https://discord.gg/aKTa85hrwG<BR -----");
                }
                if (Arguments[0] == "/help") // CommandHELPList // DLL Ver mentioned 3
                {
                    ChatUI.SendMinigameMessage("AMod v2.2 <BR Credits to Jepe, Nekto, JED5729, Kelasponssaa , Zeppelin, Krak, Shiuki<BR ==========<BR How To Use:<BR /command Arguements..<BR To view command info type /command ?<BR ==========<BR AMod Commands List<BR ==========<BR /credits, /help, /ahelp, /keys, /love, /pet1, /pet2, /sleep, /wake. /quit, /pwe, /support, /gbt, /rsc, /ait, /rit, /uait, /urit, /sbt, /cdata, /ref, /aref, /drop, /dall, /d1, /dupe, /cdupe, /poblock, /piblock, /iclear, /oclear, /eject, /set, /craft<BR ==========");
                }
                if (Arguments[0] == "/ahelp") // CommandHELPList 2 // DLL Ver mentioned 4
                {
                    ChatUI.SendMinigameMessage("AMod v2.2 <BR Credits to Jepe, Nekto, JED5729, Kelasponssaa , Zeppelin, Krak, Shiuki<BR ==========<BR How To Use:<BR /command Arguements..<BR To view command info type /command ?<BR ==========<BR AMod Commands List<BR ==========<BR /credits, /help, /ahelp, /keys, /love, /pet1, /pet2, /sleep, /wake. /quit, /pwe, /support, /gbt, /rsc, /ait, /rit, /uait, /urit, /sbt, /cdata, /ref, /aref, /drop, /dall, /d1, /dupe, /cdupe, /poblock, /piblock, /iclear, /oclear, /eject, /set, /craft<BR ==========");
                }
                if (Arguments[0] == "/keys")
                {
                    ChatUI.SendMinigameMessage("=====<BR AMOD v2.2 HOTKEYS<BR =====<BR GUI On/Off [Tab]<BR Invis Hack [ ALT + 1 ]<BR JetSpammer [ALT + 2]<br REINIT [ALT + F]<BR =====");
                }
                if (Arguments[0] == "/love")
                {
                    OutgoingMessages.SendPetPetMessage(3);
                }
                if (Arguments[0] == "/pet1")
                {
                    OutgoingMessages.SendPetCleanMessage(3);
                }
                if (Arguments[0] == "/pet2")
                {
                    OutgoingMessages.SendPetTrainMessage(3, 0);
                }
                if (Arguments[0] == "/sleep")
                {
                    ConfigData.playerChangeToSleepSeconds = 0;
                }
                if (Arguments[0] == "/wake")
                {
                    ConfigData.playerChangeToSleepSeconds = 120;
                }
                if (Arguments[0] == "/quit")
                {
                    UnityEngine.Application.Quit();
                }
                if (Arguments[0] == "/pwe")
                {
                    Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;
                    Globals.WorldController.SetBlock(World.BlockType.PWETerminal, currentPlayerMapPoint.x, currentPlayerMapPoint.y);
                    Globals.world.SetBlock(World.BlockType.PWETerminal, currentPlayerMapPoint.x, currentPlayerMapPoint.y);
                }
                if (Arguments[0] == "/support")
                {
                    SceneLoader.CheckIfWeCanGoFromWorldToWorld("GIFTBRONZE", "", null, false, null);
                }
                if (Arguments[0] == "/gbt")
                {
                    PlayerData.InventoryKey Nigga = Globals.gameplayUI.inventoryControl.GetCurrentSelection();
                    InfoPopupUI.SetupInfoPopup("BlockType ID Copied!", "BlockType ID: " + Nigga.blockType.ToString());
                    InfoPopupUI.ForceShowMenu();
                    string toCopy = Nigga.blockType.ToString();
                    Clipboard.SetText(toCopy);
                }
                if (Arguments[0] == "/rsc")
                {
                    Clipboard.Clear();
                }
                if (Arguments[0] == "/ait")
                {
                    int counter = 0; // Declare and initialize the integer
                    counter++;  // Increment the integer by 1
                    PlayerData.InventoryKey currentSelection5 = Globals.gameplayUI.inventoryControl.GetCurrentSelection();
                    World.BlockType blockType = Globals.world.GetBlockType(Globals.Player.currentPlayerMapPoint);
                    WorldItemBase worldItemData2 = Globals.world.GetWorldItemData(Globals.Player.currentPlayerMapPoint);
                    bool flag19 = worldItemData2 != null && ConfigData.IsBlockStorage(blockType);
                    BSONObject asBSON5 = worldItemData2.GetAsBSON();
                    Il2CppSystem.Collections.Generic.List<int> int32ListValue = asBSON5["storageItemsAsInventoryKeys"].int32ListValue;
                    Il2CppSystem.Collections.Generic.List<int> int32ListValue2 = asBSON5["storageItemsAmounts"].int32ListValue;
                    int SIK = (int)PlayerData.InventoryKey.InventoryKeyToInt(currentSelection5);
                    int GC = 1;
                    int32ListValue.Add(SIK);
                    int32ListValue2.Add(GC);


                    asBSON5["storageItemsAsInventoryKeys"] = int32ListValue;
                    asBSON5["storageItemsAmounts"] = int32ListValue2;
                    asBSON5["blockType"] = (int)blockType;
                    asBSON5["class"] = blockType.ToString() + "Data";
                    BSONObject bsonobject = new BSONObject();
                    bsonobject["ID"] = "ASI";
                    bsonobject["WiB"] = asBSON5;
                    bsonobject["x"] = Globals.Player.currentPlayerMapPoint.x;
                    bsonobject["y"] = Globals.Player.currentPlayerMapPoint.y;
                    bsonobject["PT"] = 1;
                    OutgoingMessages.AddOneMessageToList(bsonobject);
                    Globals.PlayerData.RemoveItemFromInventory(currentSelection5);
                    ChatUtils.D("Success");
                }

                if (Arguments[0] == "/rit")
                {
                    int counter = 0; // Declare and initialize the integer
                    counter++;  // Increment the integer by 1

                    PlayerData.InventoryKey currentSelection5 = Globals.gameplayUI.inventoryControl.GetCurrentSelection();
                    World.BlockType blockType = Globals.world.GetBlockType(Globals.Player.currentPlayerMapPoint);
                    WorldItemBase worldItemData2 = Globals.world.GetWorldItemData(Globals.Player.currentPlayerMapPoint);
                    bool flag19 = worldItemData2 != null && ConfigData.IsBlockStorage(blockType);
                    BSONObject asBSON5 = worldItemData2.GetAsBSON();
                    Il2CppSystem.Collections.Generic.List<int> int32ListValue = asBSON5["storageItemsAsInventoryKeys"].int32ListValue;
                    Il2CppSystem.Collections.Generic.List<int> int32ListValue2 = asBSON5["storageItemsAmounts"].int32ListValue;
                    int32ListValue.Clear();
                    int32ListValue2.Clear();

                    asBSON5["storageItemsAsInventoryKeys"] = int32ListValue;
                    asBSON5["storageItemsAmounts"] = int32ListValue2;
                    asBSON5["blockType"] = (int)blockType;
                    asBSON5["class"] = blockType.ToString() + "Data";
                    BSONObject bsonobject = new BSONObject();
                    bsonobject["ID"] = "ASI";
                    bsonobject["WiB"] = asBSON5;
                    bsonobject["x"] = Globals.Player.currentPlayerMapPoint.x;
                    bsonobject["y"] = Globals.Player.currentPlayerMapPoint.y;
                    bsonobject["PT"] = 1;
                    OutgoingMessages.AddOneMessageToList(bsonobject);

                    OutgoingMessages.AddOneMessageToList(new BSONObject()
                    {
                        ["ID"] = "AGI",
                        ["PT"] = 0
                    });
                    ChatUtils.D("Success");
                }


                if (Arguments[0] == "/uait")
                {
                    int counter = 0; // Declare and initialize the integer
                    counter++;  // Increment the integer by 1
                    PlayerData.InventoryKey currentSelection5 = Globals.gameplayUI.inventoryControl.GetCurrentSelection();
                    World.BlockType blockType = Globals.world.GetBlockType(Globals.Player.currentPlayerMapPoint);
                    WorldItemBase worldItemData2 = Globals.world.GetWorldItemData(Globals.Player.currentPlayerMapPoint);
                    bool flag19 = worldItemData2 != null && ConfigData.IsBlockStorage(blockType);
                    BSONObject asBSON5 = worldItemData2.GetAsBSON();
                    Il2CppSystem.Collections.Generic.List<int> int32ListValue = asBSON5["storageItemsAsInventoryKeys"].int32ListValue;
                    Il2CppSystem.Collections.Generic.List<int> int32ListValue2 = asBSON5["storageItemsAmounts"].int32ListValue;
                    int SIK = (int)PlayerData.InventoryKey.InventoryKeyToInt(currentSelection5);
                    int GC = 1;
                    int32ListValue.Add(SIK);
                    int32ListValue2.Add(GC);


                    asBSON5["storageItemsAsInventoryKeys"] = int32ListValue;
                    asBSON5["storageItemsAmounts"] = int32ListValue2;
                    asBSON5["blockType"] = 2270;
                    asBSON5["class"] = blockType.ToString() + "Data";
                    BSONObject bsonobject = new BSONObject();
                    bsonobject["ID"] = "ASI";
                    bsonobject["WiB"] = asBSON5;
                    bsonobject["x"] = Globals.Player.currentPlayerMapPoint.x;
                    bsonobject["y"] = Globals.Player.currentPlayerMapPoint.y;
                    bsonobject["PT"] = 1;
                    OutgoingMessages.AddOneMessageToList(bsonobject);
                    Globals.PlayerData.RemoveItemFromInventory(currentSelection5);
                    ChatUtils.D("Success");
                }

                if (Arguments[0] == "/urit")
                {
                    int counter = 0; // Declare and initialize the integer
                    counter++;  // Increment the integer by 1

                    PlayerData.InventoryKey currentSelection5 = Globals.gameplayUI.inventoryControl.GetCurrentSelection();
                    World.BlockType blockType = Globals.world.GetBlockType(Globals.Player.currentPlayerMapPoint);
                    WorldItemBase worldItemData2 = Globals.world.GetWorldItemData(Globals.Player.currentPlayerMapPoint);
                    bool flag19 = worldItemData2 != null && ConfigData.IsBlockStorage(blockType);
                    BSONObject asBSON5 = worldItemData2.GetAsBSON();
                    Il2CppSystem.Collections.Generic.List<int> int32ListValue = asBSON5["storageItemsAsInventoryKeys"].int32ListValue;
                    Il2CppSystem.Collections.Generic.List<int> int32ListValue2 = asBSON5["storageItemsAmounts"].int32ListValue;
                    int32ListValue.Clear();
                    int32ListValue2.Clear();

                    asBSON5["storageItemsAsInventoryKeys"] = int32ListValue;
                    asBSON5["storageItemsAmounts"] = int32ListValue2;
                    asBSON5["blockType"] = 2270;
                    asBSON5["class"] = blockType.ToString() + "Data";
                    BSONObject bsonobject = new BSONObject();
                    bsonobject["ID"] = "ASI";
                    bsonobject["WiB"] = asBSON5;
                    bsonobject["x"] = Globals.Player.currentPlayerMapPoint.x;
                    bsonobject["y"] = Globals.Player.currentPlayerMapPoint.y;
                    bsonobject["PT"] = 1;
                    OutgoingMessages.AddOneMessageToList(bsonobject);

                    OutgoingMessages.AddOneMessageToList(new BSONObject()
                    {
                        ["ID"] = "AGI",
                        ["PT"] = 0
                    });
                    ChatUtils.D("Success");
                }

                if (Arguments[0] == "/sbt")
                {
                    bool flag18 = Globals.Player != null && Globals.Player != null;
                    if (flag18)
                    {
                        World.BlockType blockType = Globals.world.GetBlockType(Globals.Player.currentPlayerMapPoint);
                        WorldItemBase worldItemData2 = Globals.world.GetWorldItemData(Globals.Player.currentPlayerMapPoint);
                        bool flag19 = worldItemData2 != null && ConfigData.IsBlockStorage(blockType);
                        if (flag19)
                        {
                            BSONObject asBSON5 = worldItemData2.GetAsBSON();
                            bool flag20 = asBSON5["inventoryDatas"]["DatasCount"].int32Value > 0;
                            if (flag20)
                            {
                                Il2CppSystem.Collections.Generic.List<int> int32ListValue = asBSON5["storageItemsAsInventoryKeys"].int32ListValue;
                                Il2CppSystem.Collections.Generic.List<int> int32ListValue2 = asBSON5["storageItemsAmounts"].int32ListValue;
                                PlayerData.InventoryKey inventoryKey = PlayerData.InventoryKey.IntToInventoryKey(int32ListValue[0]);
                                short num5 = (short)int32ListValue2[0];
                                bool flag21 = num5 < 2;
                                if (flag21)
                                {
                                    bool flag22 = Globals.PlayerData.GetCount(inventoryKey) > 0;
                                    if (!flag22)
                                    {
                                        ChatUtils.Error("You dont have " + TextManager.GetBlockTypeOrSeedName(inventoryKey) + "!");
                                    }
                                    int32ListValue2[0] = 2;
                                    Globals.PlayerData.RemoveItemFromInventory(inventoryKey);
                                }
                                else
                                {
                                    int32ListValue2[0] = 1;
                                    Globals.PlayerData.AddItemToInventory(inventoryKey, null);
                                }
                                asBSON5["storageItemsAmounts"] = int32ListValue2;
                                asBSON5["blockType"] = (int)((asBSON5["blockType"].int32Value == 2270) ? blockType : World.BlockType.StorageForUntradeables);
                                asBSON5["class"] = blockType.ToString() + "Data";
                                BSONObject bsonobject = new BSONObject();
                                bsonobject["ID"] = "ASI";
                                bsonobject["WiB"] = asBSON5;
                                bsonobject["x"] = Globals.Player.currentPlayerMapPoint.x;
                                bsonobject["y"] = Globals.Player.currentPlayerMapPoint.y;
                                bsonobject["PT"] = 1;
                                OutgoingMessages.AddOneMessageToList(bsonobject);
                                OutgoingMessages.AddOneMessageToList(new BSONObject()
                                {
                                    ["ID"] = "AGI",
                                    ["PT"] = 0
                                });

                                ChatUtils.D("Success");
                            }
                            else
                            {
                                ChatUtils.Error("Put an item inside the chest.");
                            }
                        }
                        else
                        {
                            ChatUtils.Error("Not a chest!");
                        }
                    }
                }
                // DUPE DROP
                //
                //
                //
                //

                if (Arguments[0] == "/cdata")
                {
                    BSONObject asBSON10 = Globals.PlayerData.GetInventoryData(Globals.gameplayUI.inventoryControl.GetCurrentSelection()).GetAsBSON();

                    if (asBSON10 != null)
                    {
                        ChatUtils.D("Copied item data to clipboard!");
                        Clipboard.SetText(Utils.DumpBSON(asBSON10));
                    }
                    else
                    {
                        ChatUtils.Error("Item does not contain data!");
                    }
                }

                if (Arguments[0] == "/v")
                {
                    if (Arguments[1].ToLower() == "?")
                    {
                        ChatUtils.Msg("Command to place down holograms! \r\n Arguments: \r\n W (Owned Worlds) \r\n F (Favorite Worlds) \r\n WT (WOTW)");
                    }

                    if (Arguments[1].ToLower() == "w")
                    {
                        Globals.world.SetBlock(World.BlockType.WorldHologram, Globals.CurrentMapPoint, "", "", false);
                        Globals.WorldController.SetBlock(World.BlockType.WorldHologram, Globals.CurrentMapPoint.x, Globals.CurrentMapPoint.y);
                    }
                    if (Arguments[1].ToLower() == "f")
                    {
                        Globals.world.SetBlock(World.BlockType.FavouriteWorldsProp, Globals.CurrentMapPoint, "", "", false);
                        Globals.WorldController.SetBlock(World.BlockType.FavouriteWorldsProp, Globals.CurrentMapPoint.x, Globals.CurrentMapPoint.y);
                    }
                    if (Arguments[1].ToLower() == "wt")
                    {
                        Globals.world.SetBlock(World.BlockType.WOTWWorldsProp, Globals.CurrentMapPoint, "", "", false);
                        Globals.WorldController.SetBlock(World.BlockType.WOTWWorldsProp, Globals.CurrentMapPoint.x, Globals.CurrentMapPoint.y);
                    }
                }

                if (Arguments[0] == "/ref")
                {
                    OutgoingMessages.AddOneMessageToList(new BSONObject()
                    {
                        ["ID"] = "AGI",
                        ["PT"] = 0
                    });
                }

                short num2 = 0;
                /*
                if (Arguments[0] == "/find2")
                {

                    if (Arguments[1] == "?")
                    {
                        ChatUI.SendMinigameMessage("Find item by name");
                    }

                    List<string> itemIDs = new List<string>();
                    for (int num = 0; num < System.Enum.GetNames(typeof(World.BlockType)).Length; num = Globals.num2 + 1)
                    {
                        itemIDs.Add(TextManager.GetBlockTypeName((World.BlockType)num).ToLower());
                        if (itemIDs[num].Contains(Arguments[1].ToLower()))
                        {
                            ChatUI.SendMinigameMessage(string.Format("Item found: [{0}] {1}", num, TextManager.GetBlockTypeName((World.BlockType)num)));
                            short num3 = num2;
                            num2 = (short)(num3 + 1);
                        }
                        else
                        {
                            ChatUI.SendMinigameMessage("No item found.");
                        }
                        num2 = (short)num;
                    }
                }
                if (Arguments[0] == "/get")
                {
                    if (Arguments[1] == "?")
                    {
                        ChatUI.SendMinigameMessage("Visually get the item in your inventory with custom amount.<br Example; /give 666 69 (gives 69x midnight blades)");
                    }
                    World.BlockType itemToGive = (World.BlockType)int.Parse(Arguments[1]);
                    PlayerData.InventoryKey IK;
                    IK.blockType = itemToGive;
                    IK.itemType = ConfigData.GetBlockTypeInventoryItemType(itemToGive);
                    Globals.PlayerData.AddItemToInventory(IK, 1, Globals.PlayerData.GetInventoryData(IK));
                    ChatUI.SendMinigameMessage("Given " + TextManager.GetBlockTypeName(itemToGive) + " 1x");
                    if (Arguments[3] != null)
                    {
                        IK.blockType = itemToGive;
                        IK.itemType = ConfigData.GetBlockTypeInventoryItemType(itemToGive);
                        Globals.PlayerData.AddItemToInventory(IK, (short)int.Parse(Arguments[3]), Globals.PlayerData.GetInventoryData(IK));
                        ChatUI.SendMinigameMessage("Given " + TextManager.GetBlockTypeName(itemToGive) + " " + Arguments[3] + "x");
                    }
                }
                */
                if (Arguments[0] == "/craft")
                {
                    Globals.CraftitemID = int.Parse(Arguments[1]);
                    if (string.IsNullOrEmpty(Arguments[1]))
                    {
                        ChatUtils.Error("No item ID given.");
                    }
                    else
                    {
                        ChatUtils.D("Set craftID to " + Arguments[1] + ", now enable the MiningGear or FishingGear toggle in Main Tab to start.");
                    }
                }

                if (Arguments[0] == "/aref")
                {
                    Globals.IRef = !Globals.IRef;
                    if (Globals.IRef)
                    {
                        ChatUI.SendMinigameMessage("Auto Inventory-Refresh enabled!");
                    }
                    else
                    {
                        ChatUI.SendMinigameMessage("Auto Inventory-Refreshed disabled!");
                    }
                }

                if (Arguments[0] == "/drop")
                {
                    PlayerData.InventoryKey currentSelection5 = Globals.gameplayUI.inventoryControl.GetCurrentSelection();


                    Vector2i nextPlayerPositionBasedOnLookDirection = Globals.Player.GetNextPlayerPositionBasedOnLookDirection(0);
                    BSONObject bsonobject = new BSONObject();
                    bsonobject["ID"] = "Di";
                    bsonobject["x"] = nextPlayerPositionBasedOnLookDirection.x;
                    bsonobject["y"] = nextPlayerPositionBasedOnLookDirection.y;
                    BSONValue bsonvalue2 = bsonobject;
                    string key2 = "dI";
                    BSONObject bsonobject2 = new BSONObject();
                    bsonobject2["CollectableID"] = 0;
                    bsonobject2["BlockType"] = (int)currentSelection5.blockType;
                    bsonobject2["Amount"] = int.Parse(Arguments[1]);
                    bsonobject2["InventoryType"] = (int)currentSelection5.itemType;
                    bsonobject2["PosX"] = 0;
                    bsonobject2["PosY"] = 0;
                    bsonobject2["IsGem"] = false;
                    bsonobject2["GemType"] = 0;
                    bsonvalue2[key2] = bsonobject2;
                    BSONObject bsonobject3 = bsonobject;
                    InventoryItemBase inventoryData2 = Globals.PlayerData.GetInventoryData(currentSelection5);
                    bool flag194 = inventoryData2 != null;
                    if (flag194)
                    {
                        bsonobject3["dI"]["InventoryDataKey"] = inventoryData2.GetAsBSON();
                    }
                    OutgoingMessages.AddOneMessageToList(bsonobject3);
                }
                if (Arguments[0] == "/debugparse")
                {
                    int num15 = 0;
                    try
                    {
                        string path = Directory.GetCurrentDirectory() + "\\AMod Official\\Parse.txt";
                        string text19 = "#LEGEND: ID|InventoryItemType|Health|Tier|Name|IsTradeable|CanBeBehindWater|RecallAllowed|HotSpots|Complexity|HasCollider|DropGemsMin|DropGemsMax|XpAfterDestroy\n";
                        System.Array values = System.Enum.GetValues(typeof(World.BlockType));
                        for (int num16 = 0; num16 < values.Length - 1; num16 = num2 + 1)
                        {
                            World.BlockType blockType11 = new World.BlockType();
                            blockType11 = (World.BlockType)num16;

                            text19 += string.Format("{0}|", num16);
                            text19 += string.Format("{0}|", ConfigData.GetBlockTypeInventoryItemType((World.BlockType)num16));
                            text19 += string.Format("{0}|", ConfigData.GetHitsRequired(blockType11));
                            bool flag175 = ConfigData.blockTiers[blockType11] == "n/a";
                            if (flag175)
                            {
                                text19 += "0|";
                            }
                            else
                            {
                                text19 = text19 + ConfigData.blockTiers[(World.BlockType)num16] + "|";
                            }
                            text19 = text19 + TextManager.GetBlockTypeName(blockType11) + "|";
                            text19 = text19 + (ConfigData.IsBlockUntradeable(blockType11) ? "0" : "1") + "|";
                            text19 = text19 + (ConfigData.CanBlockBeBehindWater(blockType11) ? "1" : "0") + "|";
                            text19 = text19 + (ConfigData.IsBlockRecallItem(blockType11) ? "1" : "0") + "|";
                            Il2CppStructArray<AnimationHotSpots> animationHotSpots = ControllerHelper.genericStorage.GetAnimationHotSpots(blockType11);
                            bool flag176 = animationHotSpots.Count < 1;
                            if (flag176)
                            {
                                text19 += "0|";
                            }
                            else
                            {
                                foreach (AnimationHotSpots animationHotSpots2 in animationHotSpots)
                                {
                                    string str5 = text19;
                                    num2 = (short)(int)animationHotSpots2;
                                    text19 = str5 + num2.ToString() + ",";
                                }
                                text19 = text19.Remove(text19.Length - 1) + "|";
                            }
                            text19 = text19 + ConfigData.GetBlockComplexity(blockType11).ToString() + "|";
                            text19 = text19 + (ConfigData.DoesBlockHaveCollider(blockType11) ? "1" : "0") + "|";
                            bool flag177 = (short)ConfigData.GetBlockGemDropAverage(blockType11) >= 10 && (short)ConfigData.GetBlockGemDropAverage(blockType11) == ConfigData.GetBlockDropGemRangeMin(blockType11) && ConfigData.GetBlockDropGemRangeMin(blockType11) <= 20;
                            if (flag177)
                            {
                                text19 += "0|";
                                text19 = text19 + ((int)(ConfigData.GetBlockDropGemRangeMax(blockType11) / 10)).ToString() + "|";
                            }
                            else
                            {
                                text19 = text19 + ConfigData.GetBlockDropGemRangeMin(blockType11).ToString() + "|";
                                text19 = text19 + ConfigData.GetBlockDropGemRangeMax(blockType11).ToString() + "|";
                            }
                            text19 = text19 + ConfigData.GetDestroyBlockExperience(blockType11).ToString() + "|";
                            bool flag178 = ConfigData.ShouldSkipDoesPlayerHaveRightToModifyItemData(blockType11);
                            if (flag178)
                            {
                                System.Console.WriteLine(string.Format("case BlockType.{0}:", blockType11));
                            }
                            text19 += "\n";
                            num15 = num16;
                            num2 = (short)num16;
                        }
                        File.WriteAllText(path, text19);

                        ChatUtils.D("Success.");
                    }
                    catch (System.Exception ex5)
                    {
                        MelonLogger.Error(ex5.ToString());
                        MelonLogger.Msg("Last Index was " + num15.ToString());
                    }
                }

                if (Arguments[0] == "/dall")
                {
                    PlayerData.InventoryKey currentSelection5 = Globals.gameplayUI.inventoryControl.GetCurrentSelection();
                    bool flag192 = currentSelection5.blockType > World.BlockType.None;
                    if (flag192)
                    {
                        Vector2i nextPlayerPositionBasedOnLookDirection = Globals.Player.GetNextPlayerPositionBasedOnLookDirection(0);
                        BSONObject bsonobject = new BSONObject();
                        bsonobject["ID"] = "Di";
                        bsonobject["x"] = nextPlayerPositionBasedOnLookDirection.x;
                        bsonobject["y"] = nextPlayerPositionBasedOnLookDirection.y;
                        BSONValue bsonvalue2 = bsonobject;
                        string key2 = "dI";
                        BSONObject bsonobject2 = new BSONObject();
                        bsonobject2["CollectableID"] = 0;
                        bsonobject2["BlockType"] = (int)currentSelection5.blockType;
                        bsonobject2["Amount"] = Globals.PlayerData.GetCount(currentSelection5);
                        bsonobject2["InventoryType"] = (int)currentSelection5.itemType;
                        bsonobject2["PosX"] = 0;
                        bsonobject2["PosY"] = 0;
                        bsonobject2["IsGem"] = false;
                        bsonobject2["GemType"] = 0;
                        bsonvalue2[key2] = bsonobject2;
                        BSONObject bsonobject3 = bsonobject;
                        InventoryItemBase inventoryData2 = Globals.PlayerData.GetInventoryData(currentSelection5);
                        bool flag194 = inventoryData2 != null;
                        if (flag194)
                        {
                            bsonobject3["dI"]["InventoryDataKey"] = inventoryData2.GetAsBSON();
                        }
                        OutgoingMessages.AddOneMessageToList(bsonobject3);
                    }
                }

                if (Arguments[0] == "/d1")
                {
                    PlayerData.InventoryKey currentSelection5 = Globals.gameplayUI.inventoryControl.GetCurrentSelection();
                    bool flag192 = currentSelection5.blockType > World.BlockType.None;
                    if (flag192)
                    {
                        Vector2i nextPlayerPositionBasedOnLookDirection = Globals.Player.GetNextPlayerPositionBasedOnLookDirection(0);
                        BSONObject bsonobject = new BSONObject();
                        bsonobject["ID"] = "Di";
                        bsonobject["x"] = nextPlayerPositionBasedOnLookDirection.x;
                        bsonobject["y"] = nextPlayerPositionBasedOnLookDirection.y;
                        BSONValue bsonvalue2 = bsonobject;
                        string key2 = "dI";
                        BSONObject bsonobject2 = new BSONObject();
                        bsonobject2["CollectableID"] = 0;
                        bsonobject2["BlockType"] = (int)currentSelection5.blockType;
                        bsonobject2["Amount"] = 1;
                        bsonobject2["InventoryType"] = (int)currentSelection5.itemType;
                        bsonobject2["PosX"] = 0;
                        bsonobject2["PosY"] = 0;
                        bsonobject2["IsGem"] = false;
                        bsonobject2["GemType"] = 0;
                        bsonvalue2[key2] = bsonobject2;
                        BSONObject bsonobject3 = bsonobject;
                        InventoryItemBase inventoryData2 = Globals.PlayerData.GetInventoryData(currentSelection5);
                        bool flag194 = inventoryData2 != null;
                        if (flag194)
                        {
                            bsonobject3["dI"]["InventoryDataKey"] = inventoryData2.GetAsBSON();
                        }
                        OutgoingMessages.AddOneMessageToList(bsonobject3);
                    }
                }

                if (Arguments[0] == "/dupe")
                {
                        PlayerData.InventoryKey currentSelection5 = Globals.gameplayUI.inventoryControl.GetCurrentSelection();
                        ChatUI.SendMinigameMessage("Duping Selected Item: " + currentSelection5.blockType.ToString());
                        bool flag193 = currentSelection5.blockType > World.BlockType.None;
                        if (flag193)
                        {
                            Vector2i nextPlayerPositionBasedOnLookDirection = Globals.Player.GetNextPlayerPositionBasedOnLookDirection(0);
                            BSONObject bsonobject = new BSONObject();
                            bsonobject["ID"] = "Di";
                            bsonobject["x"] = nextPlayerPositionBasedOnLookDirection.x;
                            bsonobject["y"] = nextPlayerPositionBasedOnLookDirection.y;
                            BSONValue bsonvalue2 = bsonobject;
                            string key2 = "dI";
                            BSONObject bsonobject2 = new BSONObject();
                            bsonobject2["CollectableID"] = 0;
                            bsonobject2["BlockType"] = PlayerData.InventoryKey.InventoryKeyToInt(currentSelection5);
                            bsonobject2["Amount"] = Globals.PlayerData.GetCount(currentSelection5);
                            bsonobject2["InventoryType"] = 2;
                            bsonobject2["PosX"] = 0;
                            bsonobject2["PosY"] = 0;
                            bsonobject2["IsGem"] = false;
                            bsonobject2["GemType"] = 0;
                            bsonvalue2[key2] = bsonobject2;
                            BSONObject bsonobject3 = bsonobject;
                            InventoryItemBase inventoryData2 = Globals.PlayerData.GetInventoryData(currentSelection5);
                            bool flag194 = inventoryData2 != null;
                            if (flag194)
                            {
                                bsonobject3["dI"]["InventoryDataKey"] = inventoryData2.GetAsBSON();
                            }

                            InfoPopupUI.SetupInfoPopup("Dupe has been initialized!", "Pick up the dropped seed and type command /ref \r\n Dupe was Discovered by Jepe \r\n  Made by discord.gg/amod");
                            ChatUtils.Msg("Pick up the dropped seed and type command /ref \r\n Dupe was Discovered by Jepe \r\n  Made by discord.gg/amod");
                            InfoPopupUI.ForceShowMenu();

                            OutgoingMessages.AddOneMessageToList(bsonobject3);

                            OutgoingMessages.AddOneMessageToList(new BSONObject()
                            {
                                ["ID"] = "AGI",
                                ["PT"] = 0
                            });
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.SpiritAppear);
                        }
                }
                if (Arguments[0] == "/cdupe")
                {
                        PlayerData.InventoryKey currentSelection5 = Globals.gameplayUI.inventoryControl.GetCurrentSelection();
                        ChatUI.SendMinigameMessage("Duping Selected Item: " + currentSelection5.blockType.ToString());
                        bool flag193 = currentSelection5.blockType > World.BlockType.None;
                        if (flag193)
                        {
                            Vector2i nextPlayerPositionBasedOnLookDirection = Globals.Player.GetNextPlayerPositionBasedOnLookDirection(0);
                            BSONObject bsonobject = new BSONObject();
                            bsonobject["ID"] = "Di";
                            bsonobject["x"] = nextPlayerPositionBasedOnLookDirection.x;
                            bsonobject["y"] = nextPlayerPositionBasedOnLookDirection.y;
                            BSONValue bsonvalue2 = bsonobject;
                            string key2 = "dI";
                            BSONObject bsonobject2 = new BSONObject();
                            bsonobject2["CollectableID"] = 0;
                            bsonobject2["BlockType"] = PlayerData.InventoryKey.InventoryKeyToInt(currentSelection5);
                            bsonobject2["Amount"] = int.Parse(Arguments[1]);
                            bsonobject2["InventoryType"] = 2;
                            bsonobject2["PosX"] = 0;
                            bsonobject2["PosY"] = 0;
                            bsonobject2["IsGem"] = false;
                            bsonobject2["GemType"] = 0;
                            bsonvalue2[key2] = bsonobject2;
                            BSONObject bsonobject3 = bsonobject;
                            InventoryItemBase inventoryData2 = Globals.PlayerData.GetInventoryData(currentSelection5);
                            bool flag194 = inventoryData2 != null;
                            if (flag194)
                            {
                                bsonobject3["dI"]["InventoryDataKey"] = inventoryData2.GetAsBSON();
                            }

                            InfoPopupUI.SetupInfoPopup("Dupe has been initialized!", "Pick up the dropped seed and type command /ref \r\n Dupe was Discovered by Jepe \r\n  Made by discord.gg/amod");
                            ChatUtils.Msg("Pick up the dropped seed and type command /ref \r\n Dupe was Discovered by Jepe \r\n  Made by discord.gg/amod");
                            InfoPopupUI.ForceShowMenu();

                            OutgoingMessages.AddOneMessageToList(bsonobject3);

                            OutgoingMessages.AddOneMessageToList(new BSONObject()
                            {
                                ["ID"] = "AGI",
                                ["PT"] = 0
                            });
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.SpiritAppear);
                    }
                }
                //
                //
                //
                //
                // DUPEDROP

                if (Arguments[0] == "/poblock")
                {
                    Globals.OutgoingBlock.Add(Arguments[1]);
                    string bb33 = "";
                    foreach (string nig1 in Globals.OutgoingBlock)
                    {
                        bb33 = bb33 + nig1 + ", ";
                    }
                }

                if (Arguments[0] == "/piblock")
                {
                    Globals.IncomingBlock.Add(Arguments[1]);
                    string bb33 = "";
                    foreach (string nig1 in Globals.IncomingBlock)
                    {
                        bb33 = bb33 + nig1 + ", ";
                    }
                    ChatUI.SendMinigameMessage("Ignoring these packets: " + bb33);
                }

                if (Arguments[0] == "/iclear")
                {
                    Globals.IncomingBlock.Clear();
                    string bb33 = "";
                    foreach (string nig1 in Globals.IncomingBlock)
                    {
                        bb33 = bb33 + nig1 + ", ";
                    }
                    ChatUI.SendMinigameMessage("Incoming Ignores cleared! " + bb33);
                }

                if (Arguments[0] == "/set")
                {
                    if (Arguments[1] == "")
                    {

                        ChatUtils.ParseMsg("Set " + Arguments[1] + " to " + Arguments[2]);
                    }

                    if (Arguments[1] == "")
                    {

                        ChatUtils.ParseMsg("Set " + Arguments[1] + " to " + Arguments[2]);
                    }

                    if (Arguments[1] == "")
                    {

                        ChatUtils.ParseMsg("Set " + Arguments[1] + " to " + Arguments[2]);
                    }

                    if (Arguments[1] == "")
                    {

                        ChatUtils.ParseMsg("Set " + Arguments[1] + " to " + Arguments[2]);
                    }

                    if (Arguments[1] == "")
                    {

                        ChatUtils.ParseMsg("Set " + Arguments[1] + " to " + Arguments[2]);
                    }

                    if (Arguments[1] == "?")
                    {
                        ChatUtils.ParseMsg(" COMPONENT EDITOR \r\n Command that lets you edit values of components. Example: /set worldname HI \r\n \r\n List of editable components: \r\n \r\n World & WorldController: \r\n worldName, weather, worldBackground,     \r\n \r\n ConfigData: \r\n       \r\n \r\n Player & PlayerData: \r\n        \r\n \r\n ChatUI: \r\n SendMinigameMessage, SendLogMessage,      \r\n \r\n GameplayUI: \r\n   ");
                    }
                    /*
                    FieldInfo targetField = typeof(Globals).GetField(Arguments[1]);
                    if (targetField == null)
                    {
                        ChatUtils.ParseMsg("No field named " + Arguments[1] + " found.");
                    }
                    else
                    {
                        object targetObject = targetField.GetValue(null);
                        if (targetObject == null)
                        {
                            ChatUtils.ParseMsg("Field " + Arguments[1] + " is null.");
                        }
                        else
                        {
                            string methodName = Arguments[2];
                            MethodInfo method = targetObject.GetType().GetMethod(methodName);
                            if (method == null)
                            {
                                ChatUtils.ParseMsg("Method " + methodName + " not found on " + Arguments[1]);
                            }
                            else
                            {
                                ParameterInfo[] parameters = method.GetParameters();
                                if (parameters.Length != 1)
                                {
                                    ChatUtils.ParseMsg("Method {methodName} does not take a single parameter.");
                                }
                                else
                                {
                                    Type parameterType = parameters[0].ParameterType;
                                    object parameterValue;
                                    if (parameterType.IsEnum)
                                    {
                                        parameterValue = System.Enum.Parse(parameterType, Arguments[3]);
                                    }
                                    else
                                    {
                                        parameterValue = Convert.ChangeType(Arguments[3], parameterType);
                                    }

                                    // Step 5: Invoke the method with the dynamically determined parameter
                                    method.Invoke(targetObject, new object[] { parameterValue });
                                    ChatUtils.ParseMsg($"Set {Arguments[1]}.{methodName} to {parameterValue}");
                                }
                            }
                        }
                    }
                    */
                }

                if (Arguments[0] == "/oclear")
                {
                    Globals.OutgoingBlock.Clear();
                    string bb33 = "";
                    foreach (string nig1 in Globals.OutgoingBlock)
                    {
                        bb33 = bb33 + nig1 + ", ";
                    }
                    ChatUI.SendMinigameMessage("Outgoing Ignores cleared! " + bb33);
                }

                if (Arguments[0] == "/eject")
                {
                    MelonBase.FindMelon("AMod", "Airbronze").Unregister("Ejected by user (you).", false);
                    ChatUI.SendMinigameMessage("Successfully ejected AMod from the game session. If you'd like to re-add it, restart your game.");
                }
                if (Arguments[0] == "/info")
                {
                    if (Globals.Commands.ContainsKey(Arguments[1].ToLower()))
                    {
                        string Key = Arguments[1].ToLower();
                        ChatUtils.Msg(Globals.Commands[Key]);
                    }
                    else
                    {
                    ChatUtils.Error("Please specify a correct command!");
                    }
                }
                return !text.StartsWith('/'.ToString());
            }
        }
    }
}