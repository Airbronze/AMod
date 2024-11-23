using System;
using MelonLoader;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Il2Cpp;
using System.Web.UI.WebControls;
using UnityEngine.UI;
using System.Media;
using Il2CppAmazon.DynamoDBv2.Model;
using Boo.Lang.Useful.Collections;
using Mono.CSharp;
using JetBrains.Annotations;
using Il2CppBasicTypes;
using Il2CppAlmostEngine;
using Il2CppTMPro;
using Il2CppInterop.Runtime;
using System.Threading;
using System.Timers;
using System.ServiceModel.Configuration;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppAmazon.SecurityToken.Model;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using UnityEngine.SceneManagement;
using System.ServiceModel.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.IO;
using MongoDB;
using Il2CppKernys.Bson;
using static Il2Cpp.PlayerData;
using static UnityEngine.GraphicsBuffer;
using DiscordRPC;
using UnityEngine.U2D;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using MelonLoader;
using System.Collections;
using UnityEngine.UIElements;
using System.IO.Compression;
using Image = UnityEngine.UI.Image;  // Ensure that System.Collections is used for IEnumerator
namespace AMod
{
    public class AMod : MelonMod
    {
        public Rect WindowSize = new Rect(Screen.width / 2, Screen.height / 2, 400, 400);
        public Rect WindowSize2 = new Rect(Screen.width / 2, Screen.height / 2, 400, 400);
        public Rect WindowSize3 = new Rect(150, 150, 200, 200);

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg(" \r\n   ___    __  _______  ____ \r\n   /   |  /  |/  / __ \\/ __ \\\r\n  / /| | / /|_/ / / / / / / /\r\n / ___ |/ /  / / /_/ / /_/ / \r\n/_/  |_/_/  /_/\\____/____/  \r\n                             ");
        }

        public override void OnLateInitializeMelon()
        {
            bool FileExists = !Directory.Exists(Directory.GetCurrentDirectory() + "\\AMod Official");
            if (FileExists)
            {
                try
                {
                    MelonLogger.Msg("AMod File not found! Creating..");
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\AMod Official");
                }
                catch
                {
                    UnityEngine.Application.Quit();
                }
            }
            bool FileExists2 = !Directory.Exists(Directory.GetCurrentDirectory() + "\\AMod Official\\Accounts");
            if (FileExists2)
            {
                try
                {
                    MelonLogger.Msg("Saved Accounts Folder not found! Creating..");
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\AMod Official\\Accounts");
                }
                catch
                {
                    UnityEngine.Application.Quit();
                }
            }

            bool FileExists3 = !Directory.Exists(Directory.GetCurrentDirectory() + "\\AMod Official\\Packets");
            if (FileExists3)
            {
                try
                {
                    MelonLogger.Msg("Packets folder not found! Creating..");
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\AMod Official\\Packets");
                }
                catch
                {
                    UnityEngine.Application.Quit();
                }
            }
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (Globals.Custompack)
            {
                if (!(sceneName == "MainMenu"))
                {
                    if (sceneName == "WelcomeScene")
                    {
                        try
                        {
                            UnityEngine.Object.FindObjectOfType<WelcomeSceneLogic>().canvas.transform.FindChild("Background").GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.2235f, 0.9804f, 1f);
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    try
                    {
                        Image component = UnityEngine.Object.FindObjectOfType<MainMenuBackgroundClouds>().transform.FindChild("Clouds1").GetComponent<Image>();
                        UnityEngine.Object.Destroy(component);
                        Image component2 = UnityEngine.Object.FindObjectOfType<MainMenuBackgroundClouds>().transform.FindChild("Clouds2").GetComponent<Image>();
                        UnityEngine.Object.Destroy(component2);
                        Image component3 = UnityEngine.Object.FindObjectOfType<MainMenuBackgroundClouds>().transform.FindChild("Clouds3").GetComponent<Image>();
                        UnityEngine.Object.Destroy(component3);
                        Image component4 = UnityEngine.Object.FindObjectOfType<MainMenuBackgroundClouds>().transform.FindChild("Clouds4").GetComponent<Image>();
                        UnityEngine.Object.Destroy(component4);
                        MainMenuLogic.GetInstance().canvas.transform.FindChild("MainMenuBG").GetComponent<Image>().color = new Color(0.4f, 0.2235f, 0.9804f, 1f);
                    }
                    catch
                    {
                    }
                }
            }
        }

        public static void SendCustomPacket()
            {
            if (string.IsNullOrEmpty(Globals.CustomPacket))
            {
                MelonLogger.BigError("Packets", "Your current packet is empty.");
                return;
            }

            try
            {
               	BsonDocument CustomPacket = BsonSerializer.Deserialize<BsonDocument>(Globals.CustomPacket, null);
						byte[] array = BsonExtensionMethods.ToBson<BsonDocument>(CustomPacket, null, null, null, default(BsonSerializationArgs), 0);
						BSONObject Packet = SimpleBSON.Load(BsonExtensionMethods.ToBson<BsonDocument>(CustomPacket, null, null, null, default(BsonSerializationArgs), 0));
						BSONObject FormattedPacket = BsonHelper.FormatBson(Packet);
						if (FormattedPacket != null)
						{
							Packet = FormattedPacket;
						}
						OutgoingMessages.AddOneMessageToList(Packet);
						Console.WriteLine("CUSTOM SEND: \r\n" + Utils.DumpBSON(Packet) + "\r\n");
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Could not load or format BSON from Custom Packet Exception: {ex.Message}");
            }
        }

        public static void SendCustomPacket2()
        {
            if (string.IsNullOrEmpty(Globals.CustomPacket))
            {
                return;
            }

            try
            {
                BsonDocument CustomPacket = BsonSerializer.Deserialize<BsonDocument>(Globals.CustomPacket, null);
                byte[] array = BsonExtensionMethods.ToBson<BsonDocument>(CustomPacket, null, null, null, default(BsonSerializationArgs), 0);
                BSONObject Packet = SimpleBSON.Load(BsonExtensionMethods.ToBson<BsonDocument>(CustomPacket, null, null, null, default(BsonSerializationArgs), 0));
                BSONObject FormattedPacket = BsonHelper.FormatBson(Packet);
                if (FormattedPacket != null)
                {
                    Packet = FormattedPacket;
                }
                OutgoingMessages.AddOneMessageToList(Packet);
            }
            catch (Exception ex)
            {

            }
        }


        System.Random random = new System.Random();
        private static bool showTT = false;
        private static bool showBox = true;
        private static bool statusGUI = false;
        private static bool showGUI = true;
        private static bool showNotes = false;
        private static bool showPacketsGUI = false;
        private static bool showCaptureGUI = false;
        private float TeamSwitcherTimer = 0f;
        private float SpikeBomberTimer = 0f;
        private float PotionCrafterTimer = 0f;
        private float InvisibleHackTimer = 0f;
        private float WLPlacerTimer = 0f;
        private float WLPlacerAboveTimer = 0f;
        private float WLPlacerBelowTimer = 0f;
        private float WLPlacerLeftTimer = 0f;
        private float WLPlacerRightTimer = 0f;
        private float JetSpammerTimer = 0f;
        private float LizoTimer = 0f;
        private float GemPouchTimer = 0f;
        private float CardPackTimer = 0f;
        public float ticks = 0f;
        public string VisualPlayerName = "Airbronze";
        private float AnimationTimer = 0f;
        private float GambleTimer = 0f;
        private float RecyclerTimer = 0f;
        private float PWEBuyerTImer = 0f;
        private float DropTimer = 0f;
        private float RandomItemPicker = 0f;
        private float NukeTimer = 0f;
        private float FPTimer = 0f;
        private float DropTrashTimer = 0f;
        private float SpamBotTimer = 0f;
        private float JoinRandomWorlds = 0f;
        private float EmoteTimer = 0f;
        private float ReqTimer = 0f;
        private float FriendReqTimer = 0f;
        private float MSwapTimer = 0f;
        private float CollectTimer = 0f;
        private float SeedTimer = 0f;
        private float WearTimer = 0f;
        private float ForcePlace = 0f;
        private float BedrockBreaker = 0f;
        private float BFarmerTime = 0f;
        private float SelectRandom = 0f;
        private float Temporary = 0f;
        private float Gemmer22 = 0f;
        private float BBT = 0f;
        private int integer = 0;
        private float iTTT = 0f;
        private float TPTrsh = 0f;
        private float GMTimer = 0f;
        private float PTimerP = 0f;
        private float AutoBuyTimer = 0f;
        private float ItemIDChecker = 0f;
        private float removeTime = 0f;
        private float Encime = 0f;
        private float VIPTIME = 0f;
        private float ChT = 0f;
        private float cnvT = 0f;
        private float vendTime = 0f;
        private float CollectTime = 0f;
        private float LastPickupTime = 0f;
        private float PickupInterval = 30;
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.F7))
            {
                showBox = !showBox;
            }
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.E))
            {
                SceneLoader.GoFromWorldToMainMenu();
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.G))
            {
                SceneLoader.ReloadWorld();
            }

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.G))
            {
                SceneLoader.ReloadGame();
            }

            CollectTime += Time.deltaTime;
            if (Globals.collectAll && CollectTime >= Globals.CTimer)
            {
                CollectTime = 0f;
                foreach (CollectableData CD in Globals.world.collectables)
                {
                    if (CD.mapPoint == Globals.CurrentMapPoint)
                    {
                        OutgoingMessages.SendCollectCollectableMessage(CD.id);
                    }
                }
            }

            if (Globals.PCrafter)
            {
                PlayerData.InventoryKey CraftIK = new PlayerData.InventoryKey();
                CraftIK.blockType = (World.BlockType)Globals.CraftitemID;
                CraftIK.itemType = InventoryItemType.Seed;
                for (int i = 0; i < 2; i++)
                {
                    OutgoingMessages.AddOneMessageToList(new BSONObject()
                    {
                        ["ID"] = "CraftMiningGear",
                        ["IK"] = PlayerData.InventoryKey.InventoryKeyToInt(CraftIK)
                    });
                }
                OutgoingMessages.AddOneMessageToList(new BSONObject()
                {
                    ["ID"] = "AGI",
                    ["PT"] = 0
                });
            }

            if (Globals.FCrafter)
            {
                PlayerData.InventoryKey CraftIK = new PlayerData.InventoryKey();
                CraftIK.blockType = (World.BlockType)Globals.CraftitemID;
                CraftIK.itemType = InventoryItemType.Seed;
                for (int i = 0; i < 2; i++)
                {
                    OutgoingMessages.AddOneMessageToList(new BSONObject()
                    {
                        ["ID"] = "CraftFishingGear",
                        ["IK"] = PlayerData.InventoryKey.InventoryKeyToInt(CraftIK)
                    });
                }
                OutgoingMessages.AddOneMessageToList(new BSONObject()
                {
                    ["ID"] = "AGI",
                    ["PT"] = 0
                });
            }

            if (Globals.AutoGear)
            {

            }

            if (Globals.AutoBP)
            {

            }

            GMTimer += Time.deltaTime;
            if (Globals.StartGM && GMTimer >= Globals.GMSpeed)
            {
                GMTimer = 0f;
                OutgoingMessages.AddOneMessageToList(new BSONObject()
                {
                    ["ID"] = "GM",
                    ["msg"] = Globals.Base64Msg
                });
            }

            vendTime += Time.deltaTime;
            if (Globals.AutoVendor && vendTime >= 0.05f)
            {
                PlayerData.InventoryKey VendorIK = InventoryKey.IntToInventoryKey(Globals.VendIK);
                OutgoingMessages.AddOneMessageToList(new BSONObject()
                {
                    ["ID"] = "PVi",
                    ["x"] = Globals.Player.currentPlayerMapPoint.x,
                    ["y"] = Globals.Player.currentPlayerMapPoint.y,
                    ["vC"] = Globals.VendCID,
                    ["vI"] = Globals.VendID,
                    ["IK"] = Globals.VendIK,
                });

                Globals.EndAmtVend = (int)Globals.EndAmtVend2;

                if (Globals.VendIK != 0)
                {
                    Globals.PlayerData.AddItemToInventory(VendorIK, null);
                }
                if (Globals.PlayerData.GetCount(VendorIK) == Globals.EndAmtVend)
                {
                    Globals.AutoVendor = false;
                    Globals.vendIKAMT = 0;
                    Globals.VendIK = 0;
                    Globals.VendID = 0;
                    Globals.VendCID = 0;
                    OutgoingMessages.AddOneMessageToList(new BSONObject()
                    {
                        ["ID"] = "AGI",
                        ["PT"] = 0
                    });
                    InfoPopupUI.SetupInfoPopup("AutoVendor stopped!", "You have reached your selected amount.");
                    InfoPopupUI.ForceShowMenu();
                }
            }

            if (Globals.autoOPENER)
            {
                PlayerData.InventoryKey BIK = InventoryKey.IntToInventoryKey(Globals.AOINVK);
                OutgoingMessages.SendOpenPresentMessage(BIK);
                if (Globals.PlayerData.GetCount(BIK) == 25)
                {
                    Globals.autoOPENER = false;
                    Globals.AOINVK = 0;
                }
            }

            if (Globals.ByteGemmer)
            {
                for (int i = 0; i < 1; i++)
                {
                    OutgoingMessages.BuyItemPack("ByteCoin04");
                    OutgoingMessages.BuyItemPack("ByteCoin04");
                    OutgoingMessages.BuyItemPack("ByteCoin04");
                    Globals.PlayerData.AddByteCoins(18000);
                }
            }

            Gemmer22 += Time.deltaTime;
            if (Globals.ByteGemmer && Gemmer22 >= 0.08f)
            {
                Gemmer22 = 0f;
                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = (World.BlockType)117445102;
                foreach (CollectableData CD in Globals.world.collectables)
                {
                    if (CD.mapPoint == Globals.CurrentMapPoint)
                    {
                        OutgoingMessages.SendCollectCollectableMessage(CD.id);
                    }
                }
                OutgoingMessages.RecycleFish(IK, 999);
                Globals.PlayerData.AddGems(9590400);
            }
            /*
            if (Globals.PLCv)
            {

                string itemName = "PlatinumLock";
                ItemPacks.ItemPack platLock = ItemPacks.GetItemPack(itemName);
                for (int i = 0; i < 2; i++)
                {
                    OutgoingMessages.BuyItemPack(itemName);
                }

                for (int i = 0; i < platLock.sureDrops.Length; i++)
                {
                    Globals.PlayerData.AddItemToInventory(platLock.sureDrops[i], platLock.sureDropAmounts[i], null);

                    if (Globals.PlayerData.GetCount(platLock.sureDrops[i]) > 998)
                    {
                        Globals.PLCv = false;
                    }
                }
                for (int i = 0; i < 1; i++)
                {
                    PlayerData.InventoryKey PL = new PlayerData.InventoryKey();
                    PL.blockType = World.BlockType.LockPlatinum;
                    PL.itemType = InventoryItemType.Block;
                    Globals.PlayerData.RemoveItemFromInventory(PL);
                    Globals.PlayerData.AddByteCoins(21900);

                    OutgoingMessages.AddOneMessageToList(new BSONObject()
                    {
                        ["ID"] = "PVi",
                        ["x"] = Globals.Player.currentPlayerMapPoint.x,
                        ["y"] = Globals.Player.currentPlayerMapPoint.y,
                        ["vC"] = 2,
                        ["vI"] = 2,
                        ["IK"] = 0,
                        ["Amt"] = 21900,
                    });
                }
            }

            Gemmer22 += Time.deltaTime;
            if (Globals.PLCv && Gemmer22 >= 0.08f)
            {
                Gemmer22 = 0f;
                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = (World.BlockType)117445102;
                foreach (CollectableData CD in Globals.world.collectables)
                {
                    if (CD.mapPoint == Globals.CurrentMapPoint)
                    {
                        OutgoingMessages.SendCollectCollectableMessage(CD.id);
                    }
                }
                OutgoingMessages.RecycleFish(IK, 999);
                Globals.PlayerData.AddGems(9590400);
            }
            */
            if (Globals.clearCh)
            {
                var ChatObject = UnityEngine.Object.FindObjectOfType<ChatUI>();
                if (ChatObject != null)
                {
                    UnityEngine.Object.FindObjectOfType<ChatUI>().chatMainWindow.Clear();
                }
                else
                {
                }
            }

            AutoBuyTimer += Time.deltaTime;
            if (Globals.AutoBuy1 && Globals.IPID1 != null && AutoBuyTimer >= Globals.AutoBuyCT)
            {
                ItemPacks.ItemPack itemPack = ItemPacks.GetItemPack(Globals.IPID1);

                AutoBuyTimer = 0f;
                OutgoingMessages.BuyItemPack(Globals.IPID1);

                for (int i = 0; i < itemPack.sureDrops.Length; i++)
                {
                    Globals.PlayerData.AddItemToInventory(itemPack.sureDrops[i], itemPack.sureDropAmounts[i], null);

                    if (Globals.PlayerData.GetCount(itemPack.sureDrops[i]) > 998)
                    {
                        Globals.AutoBuy1 = false;
                        Globals.IPID1 = "";
                    }
                }
            }

            if (!Globals.AutoBuy1)
            {
                Globals.IPID1 = "";
            }

            PTimerP += Time.deltaTime;
            if (Globals.PSpamP && PTimerP >= Globals.PTimerP2)
            {
                PTimerP = 0f;
                if (!Globals.ViewOcaptureOrICapture)
                {
                    Globals.CustomPacket = Globals.CurrentCaptureOUTGOING;
                }
                if (Globals.ViewOcaptureOrICapture)
                {
                    Globals.CustomPacket = Globals.CurrentCaptureINCOMING;
                }
                SendCustomPacket2();
            }


            if (Globals.NetworkClient.playerConnectionStatus == PlayerConnectionStatus.InMenus)
            {
                Globals.InvisibleHack = false;
                Globals.autoGM = false;
                Globals.StartGM = false;
                Globals.Base64Msg = "";
                Globals.PSpamP = false;
                Globals.BytesBuyer = false;
                Globals.BytesBuyer2 = false;
                Globals.GemmerOG = false;
                Globals.GemGemGem = false;
                Globals.PLCv = false;
                Globals.PLCv2 = false;
                Globals.clearCh = false;
                Globals.AutoVendor = false;
            }

            if (Globals.NetworkClient.playerConnectionStatus == PlayerConnectionStatus.Authentication)
            {
                Globals.InvisibleHack = false;
                Globals.autoGM = false;
                Globals.StartGM = false;
                Globals.Base64Msg = "";
                Globals.PSpamP = false;
                Globals.BytesBuyer = false;
                Globals.BytesBuyer2 = false;
                Globals.GemmerOG = false;
                Globals.GemGemGem = false;
                Globals.PLCv = false;
                Globals.PLCv2 = false;
                Globals.clearCh = false;
                Globals.AutoVendor = false;
            }

            if (Globals.NetworkClient.playerConnectionStatus == PlayerConnectionStatus.CheckingGameVersion)
            {
                Globals.InvisibleHack = false;
                Globals.autoGM = false;
                Globals.StartGM = false;
                Globals.Base64Msg = "";
                Globals.PSpamP = false;
                Globals.BytesBuyer = false;
                Globals.BytesBuyer2 = false;
                Globals.GemmerOG = false;
                Globals.GemGemGem = false;
                Globals.PLCv = false;
                Globals.PLCv2 = false;
                Globals.clearCh = false;
                Globals.AutoVendor = false;
            }

            if (Globals.NetworkClient.playerConnectionStatus == PlayerConnectionStatus.ConnectionFailed)
            {
                Globals.InvisibleHack = false;
                Globals.autoGM = false;
                Globals.StartGM = false;
                Globals.Base64Msg = "";
                Globals.PSpamP = false;
                Globals.BytesBuyer = false;
                Globals.BytesBuyer2 = false;
                Globals.GemmerOG = false;
                Globals.GemGemGem = false;
                Globals.PLCv = false;
                Globals.PLCv2 = false;
                Globals.clearCh = false;
                Globals.AutoVendor = false;
            }
            /*
            if (Globals.StartGM && GMTimer >= Globals.GMSpeed)
            {
                GMTimer = 0f;

                OutgoingMessages.AddOneMessageToList(new BSONObject()
                {
                    ["ID"] = "GM",
                    ["msg"] = Globals.Base64Msg
                });
                Console.WriteLine(Globals.Base64Msg);
            }

            /*
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.K))
            {
                Globals.PickyTrasher = !Globals.PickyTrasher;

                if (Globals.PickyTrasher != true)
                {
                    InfoPopupUI.SetupInfoPopup("Picky Trasher", "Currently: Off");
                    InfoPopupUI.ForceShowMenu();
                    Globals.AudioManager.PlaySFX(AudioManager.SoundType.AchievementNotify);
                }
                if (Globals.PickyTrasher != false)
                {
                    InfoPopupUI.SetupInfoPopup("Picky Trasher!", "Currently: On");
                    InfoPopupUI.ForceShowMenu();
                    Globals.AudioManager.PlaySFX(AudioManager.SoundType.AchievementNotify);
                }
            }
            */
            int CardAmt = Globals.cardAmt;
            Encime += Time.deltaTime;
            if (Globals.Disencht && Encime >= 2f)
            {
                CardAmt--;
                OutgoingMessages.AddOneMessageToList(new BSONObject()
                {
                    ["ID"] = "DCard",
                    ["ccD"] = Globals.cardID,
                    ["Amt"] = 1
                });
                if (CardAmt == 2)
                {
                    Globals.Disencht = false;
                    Globals.cardID = 0;
                    Globals.cardAmt = 0;
                }
            }
            VIPTIME += Time.deltaTime;
            if (Globals.autoVIP && Globals.NetworkClient.playerConnectionStatus == PlayerConnectionStatus.InRoom)
            {
                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = World.BlockType.ConsumableMiningToken;
                IK.itemType = InventoryItemType.Consumable;
                foreach (CollectableData CD in Globals.world.collectables)
                {
                    if (CD.mapPoint == Globals.CurrentMapPoint)
                    {
                        OutgoingMessages.SendCollectCollectableMessage(CD.id);
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    OutgoingMessages.SendSpinMiningRouletteMessage();
                }
            }
            VIPTIME += Time.deltaTime;
            if (Globals.autoVIP2 && Globals.NetworkClient.playerConnectionStatus == PlayerConnectionStatus.InRoom)
            {
                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = (World.BlockType)4372;
                IK.itemType = InventoryItemType.Consumable;
                foreach (CollectableData CD in Globals.world.collectables)
                {
                    if (CD.mapPoint == Globals.CurrentMapPoint)
                    {
                        OutgoingMessages.SendCollectCollectableMessage(CD.id);
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    foreach (CollectableData CD in Globals.world.collectables)
                    {
                        if (CD.mapPoint == Globals.CurrentMapPoint)
                        {
                            OutgoingMessages.SendCollectCollectableMessage(CD.id);
                        }
                    }
                    OutgoingMessages.SendSpinJetRaceRouletteMessage();
                }
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha9))
            {
                try
                {
                    WorldItemBase signData = Globals.world.GetWorldItemData(Globals.Player.currentPlayerMapPoint);
                    bool isNullSign = signData != null;
                    if (isNullSign)
                    {
                        Il2CppKernys.Bson.BSONObject BSONsignData = signData.GetAsBSON();
                        Il2CppKernys.Bson.BSONObject signdata2 = new Il2CppKernys.Bson.BSONObject();
                        signdata2["ID"] = "WIU";
                        signdata2["WiB"] = BSONsignData;
                        signdata2["x"] = Globals.Player.currentPlayerMapPoint.x;
                        signdata2["y"] = Globals.Player.currentPlayerMapPoint.y;
                        BSONsignData["text"] = "<br <br <br <br <br <br <br <br <br <br <br <br <br <br <br <br <br <br <br <br <br <br <br <br <br <br HELLOOOOOOOOOOOOOO THIS IS A TEST 123123123123123123123123123132123123123123123123123123123123";
                        signdata2["PT"] = 1;

                        Globals.world.WorldItemUpdate(signdata2);
                        OutgoingMessages.SendWorldItemUpdateMessage(Globals.Player.currentPlayerMapPoint, signData, ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(Globals.Player.currentPlayerMapPoint)));
                    }
                    OutgoingMessages.SendWorldItemUpdateMessage(Globals.Player.currentPlayerMapPoint, signData, ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(Globals.Player.currentPlayerMapPoint)));
                }
                catch
                {
                }
            }

            /*
            TPTrsh += Time.deltaTime;
            if (Globals.PickyTrasher && TPTrsh >= 0.5f)
            {
                TPTrsh = 0f;

                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = World.BlockType.WeaponPickaxeCrappy;
                IK.itemType = InventoryItemType.Weapon;
                short Amount = Globals.PlayerData.GetCount(IK);

                Globals.gameplayUI.inventoryControl.ActualTrashOrRecycleAction(IK, Amount);

                foreach (CollectableData CD in Globals.world.collectables)
                {
                    if (CD.mapPoint == Globals.CurrentMapPoint)
                    {
                        OutgoingMessages.SendCollectCollectableMessage(CD.id);
                    }
                }
            }
            */

            if (Globals.Fwker)
            {
                PlayerData.InventoryKey currentSelection2 = Globals.gameplayUI.inventoryControl.GetCurrentSelection();

                for (int i = 0; i < 1; i++)
                {
                    OutgoingMessages.SendDoFirewoksMessage(currentSelection2.blockType);
                    OutgoingMessages.SendDoFirewoksMessage(currentSelection2.blockType);
                    OutgoingMessages.SendDoFirewoksMessage(currentSelection2.blockType);
                    OutgoingMessages.SendDoFirewoksMessage(currentSelection2.blockType);
                }
            }

            iTTT += Time.deltaTime;
            if (Globals.IRef && iTTT >= 0.5f)
            {
                iTTT = 0f;
                OutgoingMessages.AddOneMessageToList(new BSONObject()
                {
                    ["ID"] = "AGI",
                    ["PT"] = 0
                });
            }

            if (Globals.solvingSteps != null && Globals.solvingSteps.Count > 0)
            {
                for (int l = 0; l < Math.Min(Globals.Speed, Globals.solvingSteps.Count); l++)
                {
                    int num3 = Globals.solvingSteps[0];
                    Globals.solvingSteps.RemoveAt(0);
                    Globals.CustomPacket = "{ \"ID\": \"MGA\", \"MGT\": 1, \"MGD\": " + num3 + "}";
                    AMod.SendCustomPacket();
                }
                if (Globals.solvingSteps.Count == 0 && Globals.solvingFossils)
                {
                    AMod.StartAutoSolveFossil();
                }
            }

            if (Globals.autoPickup)
            {
                LastPickupTime += Time.deltaTime;
                if (LastPickupTime >= PickupInterval)
                {
                    LastPickupTime = 0f;
                    foreach (CollectableData CD in Globals.world.collectables)
                    {
                        if (CD.mapPoint == Globals.CurrentMapPoint && CD.amount > 900 && 
                            (int)InventoryKey.IntToInventoryKey((int) CD.blockType).blockType == 
                            (int)World.BlockType.FossilPuzzle)
                        {
                            OutgoingMessages.SendCollectCollectableMessage(CD.id);
                            break;
                        }
                    } 
                }
            }
            
            if (Globals.BytesBuyer)
            {
                for (int i = 0; i < Globals.Speed; i++)
                {
                    OutgoingMessages.BuyItemPack("ByteCoin04");
                    OutgoingMessages.BuyItemPack("ByteCoin03");
                    OutgoingMessages.BuyItemPack("ByteCoin02");
                    OutgoingMessages.BuyItemPack("ByteCoin01");
                    Globals.PlayerData.AddByteCoins(7800);
                }
            }

            if (Globals.BytesBuyer2)
            {
                for (int i = 0; i < Globals.Speed; i++)
                {
                    OutgoingMessages.BuyItemPack("ByteCoin04");
                    OutgoingMessages.BuyItemPack("ByteCoin04");
                    Globals.PlayerData.AddByteCoins(12000);
                }
            }

            Gemmer22 += Time.deltaTime;
            if (Globals.GemGemGem)
            {
                Globals.GemmerOG = false;
                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = (World.BlockType)117445102;

                foreach (CollectableData CD in Globals.world.collectables)
                {
                    if (CD.mapPoint == Globals.CurrentMapPoint)
                    {
                        OutgoingMessages.SendCollectCollectableMessage(CD.id);
                    }
                }

                for (int i = 0; i < 1; i++)
                {
                    OutgoingMessages.RecycleFish(IK, 999);
                    Globals.PlayerData.AddGems(9590400);
                }
            }

            Gemmer22 += Time.deltaTime;
            if (Globals.GemmerOG && Gemmer22 >= 0.08f)
            {
                Globals.GemGemGem = false;
                Gemmer22 = 0f;
                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = (World.BlockType)117445102;
                foreach (CollectableData CD in Globals.world.collectables)
                {
                    if (CD.mapPoint == Globals.CurrentMapPoint)
                    {
                        OutgoingMessages.SendCollectCollectableMessage(CD.id);
                    }
                }
                OutgoingMessages.RecycleFish(IK, 999);
                Globals.PlayerData.AddGems(9590400);
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.F))
            {
                foreach (NetworkPlayer NwP in NetworkPlayers.otherPlayers)
                {
                    NwP.gameObject.SetActive(false);
                }
                foreach (Collectable C in Globals.WorldController.currentCollectables)
                {
                    C.gameObject.SetActive(false);
                }
                try
                {
                    for (int x = 0; x < Globals.world.worldSizeX; x++)
                    {
                        for (int y = 0; y < Globals.world.worldSizeY; y++)
                        {
                            if (Globals.world.wiringItemsData[x][y] != null)
                            {
                                Globals.world.wiringItemsData[x][y] = null;
                            }
                        }
                    }
                }
                catch
                {
                }
                Globals.WorldController.ChangeBackground(Globals.world.worldBackground);
                OutgoingMessages.RequestOtherPlayersFromWorld();
                OutgoingMessages.RequestAIPetsFromWorld();
                OutgoingMessages.RequestAIEnemiesFromWorld();
                OutgoingMessages.ReadyToPlay();
                Globals.WorldController.ReInit();
            }
            SelectRandom += Time.deltaTime;
            if (Globals.RSect && SelectRandom >= 0.02f)
            {
                try
                {
                    Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));


                    WorldItemBase animData = Globals.world.GetWorldItemData(CurrentMousePoint);
                    bool isNull2 = animData != null;
                    if (isNull2)
                    {

                        Il2CppKernys.Bson.BSONObject BSONanimData = animData.GetAsBSON();
                        Il2CppKernys.Bson.BSONObject animData2 = new Il2CppKernys.Bson.BSONObject();
                        animData2["ID"] = "WIU";
                        animData2["WiB"] = BSONanimData;
                        animData2["x"] = CurrentMousePoint.x;
                        animData2["y"] = CurrentMousePoint.y;
                        BSONanimData["itemInventoryKeyAsInt"] = random.Next(1, 5000);
                        BSONanimData["itemAmount"] = 999;
                        BSONanimData["takeAmount"] = random.Next(1, 5000);
                        BSONanimData["PT"] = 1;

                        Globals.world.WorldItemUpdate(animData2);
                        OutgoingMessages.SendWorldItemUpdateMessage(CurrentMousePoint, animData, ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(CurrentMousePoint)));
                    }
                    OutgoingMessages.SendWorldItemUpdateMessage(CurrentMousePoint, animData, ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(CurrentMousePoint)));
                }
                catch
                {
                }
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Y))
            {
                try
                {
                    Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    WorldItemBase animData = Globals.world.GetWorldItemData(CurrentMousePoint);
                    bool isNull2 = animData != null;
                    if (isNull2)
                    {

                        Il2CppKernys.Bson.BSONObject BSONanimData = animData.GetAsBSON();
                        Il2CppKernys.Bson.BSONObject animData2 = new Il2CppKernys.Bson.BSONObject();
                        animData2["ID"] = "WIU";
                        animData2["WiB"] = BSONanimData;
                        animData2["x"] = CurrentMousePoint.x;
                        animData2["y"] = CurrentMousePoint.y;
                        BSONanimData["itemInventoryKeyAsInt"] = random.Next(1, 5000);
                        BSONanimData["itemAmount"] = 999;
                        BSONanimData["takeAmount"] = 999;
                        BSONanimData["PT"] = 1;

                        Globals.world.WorldItemUpdate(animData2);
                        OutgoingMessages.SendWorldItemUpdateMessage(CurrentMousePoint, animData, ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(CurrentMousePoint)));
                    }
                    OutgoingMessages.SendWorldItemUpdateMessage(CurrentMousePoint, animData, ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(CurrentMousePoint)));
                }
                catch
                {
                }
            }


            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Q))
            {
                OutgoingMessages.SendPlayerActivateOutPortal(Globals.world.playerStartPosition);

                InfoPopupUI.SetupInfoPopup("Bypassing AntiCheat..", "Success!");
                InfoPopupUI.ForceShowMenu();
                InfoPopupUI.maxWindowLifetime = 100;

                Globals.AudioManager.PlaySFX(AudioManager.SoundType.NetherExitOpen);
            }

            // Teleport
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown((int)MouseButton.RightMouse) && !Globals.Isteleporting)
            {

                Vector2i targetMapPoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                World.BlockType blockType = Globals.world.GetBlockType(targetMapPoint.x, targetMapPoint.y);

                Console.Write($"Attempting to teleport to {targetMapPoint} of block type {blockType}");

                if (blockType == (World.BlockType)110)
                {
                    Globals.WarpPortal(targetMapPoint.x, targetMapPoint.y);
                    return;
                }

                if (ConfigData.IsBlockPortalWireable(Globals.world.GetBlockType(targetMapPoint)))
                {
                    if (Globals.world.GetWorldItemData(Globals.Player.currentPlayerMapPoint) == null)
                    {
                        Globals.StartTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, targetMapPoint.x, targetMapPoint.y);
                        return;
                    }
                    if (Globals.world.DoesPlayerHaveRightToModifyItemData(Globals.Player.currentPlayerMapPoint, Globals.PlayerData, false))
                    {
                        Globals.WarpPortal(targetMapPoint.x, targetMapPoint.y);
                    }
                    else
                    {
                        if (blockType == (World.BlockType)1799 || blockType == (World.BlockType)4373 || blockType == (World.BlockType)2001)
                        {
                            Globals.WarpPortal(targetMapPoint.x, targetMapPoint.y);
                        }
                        else
                        {
                            if (ConfigData.IsBlockPortal(blockType) && blockType != (World.BlockType)110)
                            {
                                BSONObject asBSON = Globals.world.GetWorldItemData(Globals.Player.currentPlayerMapPoint).GetAsBSON();
                                BSONObject asBSON2 = Globals.world.GetWorldItemData(targetMapPoint).GetAsBSON();
                                if (asBSON != null && asBSON2 != null)
                                {
                                    if (asBSON["targetEntryPointID"].stringValue == asBSON2["entryPointID"].stringValue && Globals.world.DoesPlayerHaveRightToGoPortal(Globals.PlayerData, targetMapPoint))
                                    {
                                        Globals.WarpPortal(targetMapPoint.x, targetMapPoint.y);
                                    }
                                    else
                                    {
                                        if (!asBSON["isLocked"].boolValue && asBSON["targetWorldID"].stringValue.ToUpper() != Globals.world.worldName.ToUpper() && !string.IsNullOrWhiteSpace(asBSON["targetWorldID"].stringValue))
                                        {
                                            Globals.WarpPortal(targetMapPoint.x, targetMapPoint.y);
                                        }
                                        else
                                        {
                                            Globals.StartTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, targetMapPoint.x, targetMapPoint.y);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Globals.StartTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, targetMapPoint.x, targetMapPoint.y);
                }
            }
            else if (Globals.Isteleporting)
            {
                Globals.teleportTimer += Time.deltaTime;
                Vector2i targetMapPoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if (Globals.teleportTimer >= Globals.teleportInterval)
                {
                    Globals.ProcessTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, targetMapPoint.x, targetMapPoint.y);
                    Globals.TeleportTimer = 0f;
                }
            }

            // ViewGUI
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                showGUI = !showGUI;
            }

            // View Notes
            if (Globals.viewNotes)
            {
                showNotes = true;
            }
            else
            {
                showNotes = false;
            }

            /*
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Q))
            {

                OutgoingMessages.SendPlayerActivateOutPortal(Globals.world.playerStartPosition);

                InfoPopupUI.SetupInfoPopup("Bypassing AntiCheat..", "Success!");
                InfoPopupUI.ForceShowMenu();
                InfoPopupUI.maxWindowLifetime = 100;

                Globals.AudioManager.PlaySFX(AudioManager.SoundType.NetherExitOpen);
            }
            */


            // JetSpammer
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha2))
            {
                Globals.Jetpacker = !Globals.Jetpacker;

                if (Globals.Jetpacker != true)
                {
                    InfoPopupUI.SetupInfoPopup("JetSpammer Toggled!", "Currently: Off");
                    InfoPopupUI.ForceShowMenu();
                    Globals.AudioManager.PlaySFX(AudioManager.SoundType.BlueParticleDisappear);
                }
                if (Globals.Jetpacker != false)
                {
                    InfoPopupUI.SetupInfoPopup("JetSpammer Toggled!", "Currently: On");
                    InfoPopupUI.ForceShowMenu();
                    Globals.AudioManager.PlaySFX(AudioManager.SoundType.BlueParticleAppear);
                }
            }
            if (Globals.VisualName && Globals.Player)
            {
                // Update ticks
                ticks += 0.03f;

                // Get the real player name
                string realPlayerName = StaticPlayer.theRealPlayername;

                // Build the color-changing name
                StringBuilder stringBuilder = new StringBuilder();
                for (int k = 0; k < realPlayerName.Length; k++)
                {
                    Color color = new Color
                    {
                        r = 0.5f + 0.5f * Mathf.Cos(ticks + k / (realPlayerName.Length / 4f)),
                        g = 0.5f + 0.5f * Mathf.Cos(ticks + k / (realPlayerName.Length / 4f) + 2f),
                        b = 0.5f + 0.5f * Mathf.Cos(ticks + k / (realPlayerName.Length / 4f) + 4f),
                        a = 1f
                    };

                    string colorHex = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}",
                        (int)(color.r * 255f),
                        (int)(color.g * 255f),
                        (int)(color.b * 255f),
                        (int)(color.a * 255f)
                    );

                    stringBuilder.Append($"<color={colorHex}>{realPlayerName[k]}</color>");
                }

                // Assuming you have a reference to the player's TextMeshPro component
                ControllerHelper.worldController.player.playerNameTextMeshPro.SetText(stringBuilder.ToString(), true);
            }
            if (Globals.AirbronzeName && Globals.Player)
            {
                // Update ticks
                ticks += 0.03f;

                // Build the color-changing name
                StringBuilder stringBuilder = new StringBuilder();
                for (int k = 0; k < this.VisualPlayerName.Length; k++)
                {
                    Color color = new Color
                    {
                        r = 0.2f + 0.6f * Mathf.Cos(ticks + k / (this.VisualPlayerName.Length / 4f)),
                        g = 0.2f + 0.6f * Mathf.Cos(ticks + k / (this.VisualPlayerName.Length / 4f) + 2f),
                        b = 0.5f + 0.2f * Mathf.Cos(ticks + k / (this.VisualPlayerName.Length / 4f) + 4f),
                        a = 1f
                    };

                    string colorHex = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}",
                        (int)(color.r * 255f),
                        (int)(color.g * 255f),
                        (int)(color.b * 255f),
                        (int)(color.a * 255f)
                    );

                    stringBuilder.Append($"<color={colorHex}>{this.VisualPlayerName[k]}</color>");
                }

                // Assuming you have a reference to the player's TextMeshPro component
                ControllerHelper.worldController.player.playerNameTextMeshPro.SetText(stringBuilder.ToString(), true);
            }


            // GemPouch Spammer
            GemPouchTimer += Time.deltaTime;
            if (Globals.GemPoucher2 && GemPouchTimer >= 0.02f)
            {
                GemPouchTimer = 0f;

                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = (World.BlockType)Globals.GemPoucher1;
                IK.itemType = ConfigData.GetBlockTypeInventoryItemType((World.BlockType)Globals.GemPoucher1);
                OutgoingMessages.SendOpenTreasureMessage(IK, true);
            }

            //CardPack Spammer
            CardPackTimer += Time.deltaTime;
            if (Globals.CardPack1 && CardPackTimer >= 0.02f)
            {
                CardPackTimer = 0f;
                OutgoingMessages.OpenCardPack(PlayerData.InventoryKey.IntToInventoryKey(117445216));
            }

            //Gambler
            GambleTimer += Time.deltaTime;
            if (Globals.Gambler && GambleTimer >= 0.3f)
            {
                GambleTimer = 0f;
                Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;
                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = (World.BlockType.FantasyWell);

                OutgoingMessages.SendDonateByteCoinsMessage(IK, currentPlayerMapPoint, 1);
            }

            // Recycler
            RecyclerTimer += Time.deltaTime;
            if (Globals.Recycler && RecyclerTimer >= 1f)
            {
                RecyclerTimer = 0f;
                Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;

                PlayerData.InventoryKey currentSelection2 = Globals.gameplayUI.inventoryControl.GetCurrentSelection();

                OutgoingMessages.RecycleBlock(currentSelection2, 1, currentPlayerMapPoint);
            }



            DropTrashTimer += Time.deltaTime;
            if (Globals.Trasher && DropTrashTimer >= 1f)
            {
                DropTrashTimer = 0f;

                PlayerData.InventoryKey IK = Globals.gameplayUI.inventoryControl.GetCurrentSelection();
                short Amount = Globals.PlayerData.GetCount(IK);

                Globals.gameplayUI.inventoryControl.ActualTrashOrRecycleAction(IK, Amount);
            }

            DropTrashTimer += Time.deltaTime;
            if (Globals.Dropper && DropTrashTimer >= 1f)
            {
                DropTrashTimer = 0f;
                PlayerData.InventoryKey IK = Globals.gameplayUI.inventoryControl.GetCurrentSelection();
                short Amount = Globals.PlayerData.GetCount(IK);
                Globals.Player.DropItems(IK, Amount);
                Globals.PlayerData.RemoveItemsFromInventory(IK, Amount);
            }

            //Potion Crafter
            PotionCrafterTimer += Time.deltaTime;
            if (Globals.PotionCrafter && PotionCrafterTimer >= 0.5f)
            {

                PotionCrafterTimer = 0f;

                Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;
                WorldItemBase wID = Globals.world.GetWorldItemData(Globals.CurrentMapPoint);
                bool isNull = wID != null;
                if (isNull)
                {
                    Il2CppKernys.Bson.BSONObject BSONwID = wID.GetAsBSON();
                    bool yadadada = ConfigData.IsConsumablePotion((World.BlockType)Globals.PotionID);
                    if (yadadada)
                    {

                        BSONwID["craftingStartTimeInTicks"] = 0L;
                        wID.SetViaBSON(BSONwID);
                        PlayerData.InventoryKey IKPot = new PlayerData.InventoryKey();
                        IKPot.blockType = (World.BlockType)Globals.PotionID;
                        IKPot.itemType = ConfigData.GetBlockTypeInventoryItemType((World.BlockType)Globals.PotionID);

                        OutgoingMessages.SendCraftFAMFoodMessage(currentPlayerMapPoint, Globals.world.GetWorldItemData(currentPlayerMapPoint), IKPot);
                        OutgoingMessages.SendCollectFAMFoodMessage(currentPlayerMapPoint, Globals.world.GetWorldItemData(currentPlayerMapPoint), IKPot);
                    }
                }
            }

            AnimationTimer += Time.deltaTime;
            if (Globals.AnimOnner && AnimationTimer >= 0.2f)
            {
                try
                {
                    Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    WorldItemBase animData = Globals.world.GetWorldItemData(CurrentMousePoint);
                    bool isNull2 = animData != null;
                    if (isNull2)
                    {

                        Il2CppKernys.Bson.BSONObject BSONanimData = animData.GetAsBSON();
                        BSONanimData["animOn"] = true;
                        Il2CppKernys.Bson.BSONObject animData2 = new Il2CppKernys.Bson.BSONObject();
                        animData2["ID"] = "WIU";
                        animData2["WiB"] = BSONanimData;
                        animData2["x"] = CurrentMousePoint.x;
                        animData2["y"] = CurrentMousePoint.y;
                        animData2["PT"] = 1;
                        animData2["U"] = "AverageAModUser";

                        Globals.world.WorldItemUpdate(animData2);
                        OutgoingMessages.SendWorldItemUpdateMessage(CurrentMousePoint, Globals.world.GetWorldItemData(CurrentMousePoint), ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(CurrentMousePoint)));
                    }
                }
                catch
                {
                }
            }

            AnimationTimer += Time.deltaTime;
            if (Globals.AnimOffer && AnimationTimer >= 0.2f)
            {
                try
                {
                    Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    WorldItemBase animData = Globals.world.GetWorldItemData(CurrentMousePoint);
                    bool isNull2 = animData != null;
                    if (isNull2)
                    {

                        Il2CppKernys.Bson.BSONObject BSONanimData = animData.GetAsBSON();
                        BSONanimData["animOn"] = false;
                        Il2CppKernys.Bson.BSONObject animData2 = new Il2CppKernys.Bson.BSONObject();
                        animData2["ID"] = "WIU";
                        animData2["WiB"] = BSONanimData;
                        animData2["x"] = CurrentMousePoint.x;
                        animData2["y"] = CurrentMousePoint.y;
                        animData2["PT"] = 1;
                        animData2["U"] = "AverageAModUser";

                        Globals.world.WorldItemUpdate(animData2);
                        OutgoingMessages.SendWorldItemUpdateMessage(CurrentMousePoint, Globals.world.GetWorldItemData(CurrentMousePoint), ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(CurrentMousePoint)));
                    }
                }
                catch
                {
                }
            }

            // JetSpammerTimer
            JetSpammerTimer += Time.deltaTime;
            if (Globals.Jetpacker && JetSpammerTimer >= 0.3f)
            {
                JetSpammerTimer = 0f;

                OutgoingMessages.SendPlayPlayerAudioMessage(66, 881);
                Globals.AudioManager.PlaySFX(AudioManager.SoundType.PlayerRocketEmpty, World.BlockType.JetPackDark, 0f, -1);
            }

            // LizoEffect
            LizoTimer += Time.deltaTime;
            if (Globals.LizoEffect && LizoTimer >= 1f)
            {
                LizoTimer = 0f;

                OutgoingMessages.SendPlayerPoisonStart(World.BlockType.PoisonTrap);
                Globals.Player.CheckPoisoned();
            }


            // WL Placers
            WLPlacerTimer += Time.deltaTime;
            if (Globals.WLPlacer && WLPlacerTimer >= 0.02f)
            {
                WLPlacerTimer = 0f;

                Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                OutgoingMessages.SendSetBlockMessage(CurrentMousePoint, World.BlockType.LockWorld);
            }

            // Toggles
            if (Globals.InstantRes)
            {
                ConfigData.playerIsDeadBackupTimerCheck = 0f;
                Globals.Player.portalAnimationSpeed = 50f;
                Globals.Player.portalScaleAnimationSpeed = 50f;
            }

            if (Globals.AntiPush)
            {
                ConfigData.playerHitOtherPlayerVelocityMax = 0f;
            }
            
            if (Globals.InfiniteText)
            {
                ConfigData.chatCharacterLimit = 255;
                ConfigData.maxChatLines = 255;
                ConfigData.maxCharsPerSign = 9999;
                ConfigData.maxEmojisPerMessage = 255;
            }

                if (Globals.FlyHack)
                {  
                ConfigData.jumpMinHeight = 0.3f;
                ConfigData.jumpMinHeightRocket = 0.3f;
                ConfigData.rocketFuelConsumptionSpeed = 0f;
                ConfigData.rocketFuelConsumptionSpeed60FPS = 0f;
                Globals.Player.rocketFuelRegenerationSpeed = 99999f;
                Globals.Player.rocketFuel = 99999f;
                }


            // Invisible Hack
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                Globals.InvisibleHack = !Globals.InvisibleHack;

                if (Globals.InvisibleHack != true)
                {
                    InfoPopupUI.SetupInfoPopup("GhostHack Toggled!", "Currently: Off");
                    InfoPopupUI.ForceShowMenu();
                    Globals.AudioManager.PlaySFX(AudioManager.SoundType.BlueParticleDisappear);
                }

                if (Globals.InvisibleHack != false)
                {
                    InfoPopupUI.SetupInfoPopup("GhostHack Toggled!", "Currently: On");
                    InfoPopupUI.ForceShowMenu();
                    Globals.AudioManager.PlaySFX(AudioManager.SoundType.BlueParticleAppear);
                }
            }
            InvisibleHackTimer += Time.deltaTime;
            if (Globals.InvisibleHack && InvisibleHackTimer >= 0.2f)
            {
                InvisibleHackTimer = 0f;

                OutgoingMessages.SendPlayerActivateInPortal(Globals.world.playerStartPosition);
                OutgoingMessages.SendPlayerActivateOutPortal(Globals.world.playerStartPosition);
            }

            SpikeBomberTimer += Time.deltaTime;
            if (Globals.SpikeBomber && SpikeBomberTimer >= 0.1f)
            {
                SpikeBomberTimer = 0f;
                OutgoingMessages.SendPlayPlayerAudioMessage(14, 959);
                Globals.AudioManager.PlaySFX(AudioManager.SoundType.GotHitFromBlock, World.BlockType.SpikeBall, 0f - 1);
            }

            TeamSwitcherTimer += Time.deltaTime;
            if (Globals.TeamSwitcher && TeamSwitcherTimer >= 0.1f)
            {
                TeamSwitcherTimer = 0f;
                PlayerData.Gender gender = (PlayerData.Gender)random.Next(0, 2);
                int SkinColor = random.Next(0, 15);
                OutgoingMessages.PlayerInfoUpdated(gender, Globals.PlayerData.countryCode, SkinColor, Globals.PlayerData.skinColorIndexBeforeOverride);
                Globals.Player.ChangeSkinColor(SkinColor);
                Globals.PlayerData.gender = gender;
            }

            // Private Servers

            // PWE Buyer
            PWEBuyerTImer += Time.deltaTime;
            if (Globals.PWEBuyerAuto && PWEBuyerTImer >= 0.5f)
            {
                PWEBuyerTImer = 0f;

                Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;

                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = (World.BlockType)int.Parse(Globals.InventoryKeyBT);
                IK.itemType = ConfigData.GetBlockTypeInventoryItemType((World.BlockType)int.Parse(Globals.InventoryKeyIT));

                OutgoingMessages.SendBuyOutAuctionHouseItem(currentPlayerMapPoint, IK, 1);
            }

            DropTimer += Time.deltaTime;
            if (Globals.CustomItemDrop && DropTimer >= 0.02f)
            {
                DropTimer = 0f;

                Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = (World.BlockType)int.Parse(Globals.InventoryKeyBT);
                IK.itemType = ConfigData.GetBlockTypeInventoryItemType((World.BlockType)int.Parse(Globals.InventoryKeyIT));
                Globals.DropAmtRandomized = random.Next(1, 999);

                OutgoingMessages.SendBuyOutAuctionHouseItem(CurrentMousePoint, IK, 999);
                OutgoingMessages.SendDropItemMessage(CurrentMousePoint, IK, (short)Globals.DropAmtRandomized, Globals.PlayerData.GetInventoryData(Globals.gameplayUI.inventoryControl.GetCurrentSelection()));
            }

            DropTimer += Time.deltaTime;
            if (Globals.RandomCDrop && DropTimer >= 0.02f)
            {
                DropTimer = 0f;
                Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                IK.blockType = (World.BlockType)random.Next(1, 5000);
                IK.itemType = ConfigData.GetBlockTypeInventoryItemType((World.BlockType)random.Next(0, 10));

                Globals.DropAmtRandomized = random.Next(1, 999);

                OutgoingMessages.SendBuyOutAuctionHouseItem(CurrentMousePoint, IK, 999);
                OutgoingMessages.SendDropItemMessage(CurrentMousePoint, IK, (short)Globals.DropAmtRandomized, Globals.PlayerData.GetInventoryData(Globals.gameplayUI.inventoryControl.GetCurrentSelection()));
            }


            SeedTimer += Time.deltaTime;
            if (Globals.CustomSeeder && SeedTimer >= 0.02f)
            {
                try
                {
                    Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    WorldItemBase animData = Globals.world.GetWorldItemData(CurrentMousePoint);
                    bool isNull2 = animData != null;
                    if (isNull2)
                    {
                    
                        Il2CppKernys.Bson.BSONObject BSONanimData = animData.GetAsBSON();
                        Il2CppKernys.Bson.BSONObject animData2 = new Il2CppKernys.Bson.BSONObject();
                        animData2["ID"] = "WIU";
                        animData2["WiB"] = BSONanimData;
                        animData2["x"] = CurrentMousePoint.x;
                        animData2["y"] = CurrentMousePoint.y;
                        BSONanimData["itemInventoryKeyAsInt"] = random.Next(1, 5000);
                        BSONanimData["itemAmount"] = 999;
                        BSONanimData["takeAmount"] = random.Next(1, 999);
                        BSONanimData["PT"] = 1;

                        Globals.world.WorldItemUpdate(animData2);
                        OutgoingMessages.SendWorldItemUpdateMessage(CurrentMousePoint, Globals.world.GetWorldItemData(CurrentMousePoint), ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(CurrentMousePoint)));
                    }
                }
                catch
                {
                }
            }

            WearTimer += Time.deltaTime;
            if (Globals.Wearer && WearTimer >= 0.02f)
            {
                Globals.BValue = random.Next(1, 5000);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)Globals.BValue);
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)Globals.BValue, 0);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)random.Next(1, 5000));
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)random.Next(1, 5000), 0);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)random.Next(1, 5000));
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)random.Next(1, 5000), 0);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)random.Next(1, 5000));
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)random.Next(1, 5000), 0);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)random.Next(1, 5000));
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)random.Next(1, 5000), 0);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)random.Next(1, 5000));
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)random.Next(1, 5000), 0);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)random.Next(1, 5000));
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)random.Next(1, 5000), 0);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)random.Next(1, 5000));
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)random.Next(1, 5000), 0);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)random.Next(1, 5000));
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)random.Next(1, 5000), 0);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)random.Next(1, 5000));
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)random.Next(1, 5000), 0);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)random.Next(1, 5000));
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)random.Next(1, 5000), 0);
                OutgoingMessages.SendWearableOrWeaponChange((World.BlockType)random.Next(1, 5000));
                Globals.Player.ChangeWearableOrWeaponRemote((World.BlockType)random.Next(1, 5000), 0);

            }

            ForcePlace += Time.deltaTime;
            if (Globals.ForcePlace && ForcePlace >= 0.02f)
            {
                Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                OutgoingMessages.SendSetBlockBackgroundMessage(CurrentMousePoint, World.BlockType.LockWorld);
            }

            BedrockBreaker += Time.deltaTime;
            if (Globals.Bbreak && BedrockBreaker >= 0.02f)
            {
                Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                BedrockBreaker = 0f;
                OutgoingMessages.SendSetBlockBackgroundMessage(CurrentMousePoint, World.BlockType.Bedrock);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
                OutgoingMessages.SendSetBlockBackgroundMessage(CurrentMousePoint, World.BlockType.Bedrock);
                OutgoingMessages.SendSetBlockBackgroundMessage(CurrentMousePoint, World.BlockType.Bedrock);
                OutgoingMessages.SendSetBlockBackgroundMessage(CurrentMousePoint, World.BlockType.Bedrock);
                OutgoingMessages.SendSetBlockBackgroundMessage(CurrentMousePoint, World.BlockType.Bedrock);
                OutgoingMessages.SendSetBlockBackgroundMessage(CurrentMousePoint, World.BlockType.Bedrock);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
            }

            BFarmerTime += Time.deltaTime;
            if (Globals.BFarmer && BFarmerTime >= 0.02f)
            {
                BFarmerTime = 0f;
                Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));

                OutgoingMessages.SendSetBlockBackgroundMessage(CurrentMousePoint, (World.BlockType)int.Parse(Globals.BFValue));
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
            }

            NukeTimer += Time.deltaTime;
            if (Globals.Nuker && NukeTimer >= 0.00002f)
            {
                Vector2i CurrentMousePoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                NukeTimer = 0f;
                OutgoingMessages.SendHitBlockMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now, false);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
                OutgoingMessages.SendHitBlockMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now, false);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
                OutgoingMessages.SendHitBlockMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now, false);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
                OutgoingMessages.SendHitBlockMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now, false);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
                OutgoingMessages.SendHitBlockMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now, false);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
                OutgoingMessages.SendHitBlockMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now, false);
                OutgoingMessages.SendHitBlockBackgroundMessage(CurrentMousePoint, Il2CppSystem.DateTime.Now);
            }

            // Botting
            SpamBotTimer += Time.deltaTime;
            JoinRandomWorlds += Time.deltaTime;
            if (Globals.SpamTexter && SpamBotTimer > Globals.SendMsgTime)
            {
                SpamBotTimer = 0f;

                OutgoingMessages.SubmitWorldChatMessage(Globals.SpamTextOfChoice);
                Globals.chatUI.Submit(Globals.SpamTextOfChoice);



                if (Globals.JoinRandomWorlds && Globals.SpamTexter && JoinRandomWorlds > Globals.WorldTime)
                {
                    JoinRandomWorlds = 0f;
                    OutgoingMessages.SendTryToJoinRandomMessage();
                }
            }

            FriendReqTimer += Time.deltaTime;
            if (Globals.FriendReqSpam && Globals.NetworkClient.playerConnectionStatus == PlayerConnectionStatus.InRoom)
            {
                if (FriendReqTimer > Globals.SendMsgTime)
                {
                    foreach (NetworkPlayer noobs in NetworkPlayers.otherPlayers)
                    {
                        FriendReqTimer = 0f;
                        OutgoingMessages.AddFriend(noobs.clientId);
                    }

                }
            }

            EmoteTimer += Time.deltaTime;
            if (Globals.EmoteSpam && EmoteTimer > 2f)
            {
                EmoteTimer = 0f;
                OutgoingMessages.SendPetPetMessage(3);
            }

            ReqTimer += Time.deltaTime;
            if (Globals.TradeSpam && ReqTimer > 3f)
            {
                ReqTimer = 0f;
                foreach (NetworkPlayer nabs in NetworkPlayers.otherPlayers)
                {
                    OutgoingMessages.AskTrade(nabs.clientId);
                }
            }

            MSwapTimer += Time.deltaTime;
            if (Globals.MannequinSpam && MSwapTimer > 0.002f)
            {
                MSwapTimer = 0f;
                Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;
                OutgoingMessages.SendAdjustMannequinAndInventoryMessage(currentPlayerMapPoint, Globals.world.GetWorldItemData(currentPlayerMapPoint), true);
            }
        }

        public Tab tab = Tab.Main;
        public enum Tab
        {
            Main,
            Data,
            PacketExtra,
            Troll,
            Misc,
            Accounts,
            Utils,
            PatchNotes,
            ClothesBugger,
            Music,
            WLPlacing,
            GemPouch,
            SavedAccs,
            Potions,
            Extra,
            Botting,
            SpamBotting,
            SpamBottingSettings,
            Settings,
            Auto,
            PrivateServers,
            Temp,
            Portal,
            Csign,
            GMer9000,
            SpamSettingsPacket,
            AutoBuy,
            IDLookup,
            Experi,
            DExtra,
            Main2,
            Textures,
            SimplePack,
            RecolorPack,
            CitemPack,
            AllApplied,
            Memeified,
            TTTHack,
            TTTHack2,
            CashExtra,
            MyTab,
        }
        private void DrawMenu(int ID) // Tab Buttons main
        {
            if (GUI.Button(new Rect(1, 15, 50, 35), "Main"))
            {
                Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                tab = Tab.Main;
            }
            if (GUI.Button(new Rect(51, 15, 50, 35), "Data"))
            {
                Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                tab = Tab.Data;
            }
            if (GUI.Button(new Rect(101, 15, 50, 35), "Misc"))
            {
                Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                tab = Tab.Misc;
            }
            if (GUI.Button(new Rect(151, 15, 75, 35), "Accounts"))
            {
                Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                tab = Tab.Accounts;
            }
            if (GUI.Button(new Rect(226, 15, 50, 35), "Utils"))
            {
                Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                tab = Tab.Utils;
            }
            if (GUI.Button(new Rect(276, 15, 70, 35), "Settings"))
            {
                Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                tab = Tab.Settings;
            }
            if (GUI.Button(new Rect(356, 15, 36, 35), "$"))
            {
                tab = Tab.DExtra;
            }

            switch (tab)
            {

                case Tab.Settings:

                    GUI.Label(new Rect(10, 55, 10000, 10000), "Mod Detector & Logging:");

                    Globals.LeaveOnDetect = GUI.Toggle(new Rect(10, 85, 150, 25), Globals.LeaveOnDetect, "Leave on Detect");
                    Globals.HardDetect = GUI.Toggle(new Rect(10, 115, 150, 25), Globals.HardDetect, "Hard-Detect");
                    Globals.LogAllPlayers = GUI.Toggle(new Rect(10, 145, 150, 25), Globals.LogAllPlayers, "Log All Players (lag)");

                    if (GUI.Button(new Rect(190, 85, 145, 25), "Lookup Mod/Admin ID"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.IDLookup;
                    }

                    if (!Globals.viewNotes)
                    {
                        if (GUI.Button(new Rect(15, 175, 150, 25), "My Notepad OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.viewNotes = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 175, 150, 25), "My Notepad ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.viewNotes = false;
                        }
                    }

                    if (GUI.Button(new Rect(15, 205, 180, 25), "Experimental"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.Experi;
                    }

                    if (GUI.Button(new Rect(15, 235, 180, 25), "Status Editor"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        statusGUI = true;
                    }
                    /* Globals.ACBPass = GUI.Toggle(new Rect(15, 235, 130, 25), Globals.ACBPass, "AntiCheat Bypass"); */

                    break;
                case Tab.Experi:

                    if (GUI.Button(new Rect(15, 55, 110, 25), "Send Sign?"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        WorldItemBase signData = Globals.world.GetWorldItemData(Globals.Player.currentPlayerMapPoint);

                        Il2CppKernys.Bson.BSONObject BSONsignData = signData.GetAsBSON();
                        Il2CppKernys.Bson.BSONObject signdata2 = new Il2CppKernys.Bson.BSONObject();
                        signdata2["ID"] = "WIU";
                        signdata2["WiB"] = BSONsignData;
                        signdata2["x"] = Globals.Player.currentPlayerMapPoint.x;
                        signdata2["y"] = Globals.Player.currentPlayerMapPoint.y;
                        BSONsignData["text"] = Globals.SignText12;
                        signdata2["PT"] = 1;
                        BSONsignData["PT"] = 1;

                        Globals.world.WorldItemUpdate(signdata2);
                        OutgoingMessages.SendWorldItemUpdateMessage(Globals.Player.currentPlayerMapPoint, signData, ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(Globals.Player.currentPlayerMapPoint)));
                    }

                    if (GUI.Button(new Rect(15, 85, 110, 25), "Send Portal?"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        WorldItemBase signData = Globals.world.GetWorldItemData(Globals.Player.currentPlayerMapPoint);

                        Il2CppKernys.Bson.BSONObject BSONsignData = signData.GetAsBSON();
                        Il2CppKernys.Bson.BSONObject signdata2 = new Il2CppKernys.Bson.BSONObject();
                        signdata2["ID"] = "WIU";
                        signdata2["WiB"] = BSONsignData;
                        signdata2["x"] = Globals.Player.currentPlayerMapPoint.x;
                        signdata2["y"] = Globals.Player.currentPlayerMapPoint.y;
                        BSONsignData["name"] = Globals.SignText12;
                        signdata2["PT"] = 1;
                        BSONsignData["PT"] = 1;

                        Globals.world.WorldItemUpdate(signdata2);
                        OutgoingMessages.SendWorldItemUpdateMessage(Globals.Player.currentPlayerMapPoint, signData, ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(Globals.Player.currentPlayerMapPoint)));
                    }

                    if (GUI.Button(new Rect(15, 115, 110, 25), "C-Sign"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.Csign;
                    }


                    break;
                case Tab.IDLookup:

                    GUI.Label(new Rect(15, 55, 10000, 10000), "Write ID:");
                    Globals.IDLookup1 = GUI.TextArea(new Rect(15, 85, 125, 25), Globals.IDLookup1);
                    if (GUI.Button(new Rect(15, 115, 100, 25), "Get name"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        if (Globals.PWStaff.ContainsKey(Globals.IDLookup1))
                        {
                            Globals.GNameID = Globals.PWStaff[Globals.IDLookup1];
                        }
                        if (!Globals.PWStaff.ContainsKey(Globals.IDLookup1))
                        {
                        Globals.GNameID = "Mod / Admin does not match with ID!";
                        }
                    }

                    GUI.Label(new Rect(15, 140, 10000, 10000), Globals.GNameID);

                    break;
                case Tab.Csign:

                    Globals.SignText12 = GUI.TextArea(new Rect(15, 55, 300, 300), Globals.SignText12);

                    break;
                case Tab.Portal:

                    Globals.TWorld = GUI.TextArea(new Rect(15, 65, 120, 20), Globals.TWorld);

                    GUI.Label(new Rect(15, 115, 10000, 10000), "Visual World Name");
                    Globals.NameVis = GUI.TextArea(new Rect(15, 140, 120, 20), Globals.NameVis);

                    if (GUI.Button(new Rect(15, 95, 90, 20), "Go"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        WorldItemBase worldItemData = Globals.world.GetWorldItemData(Globals.CurrentMapPoint);
                        BSONObject asBSON = worldItemData.GetAsBSON();
                        if (asBSON["class"].stringValue == "BluePortalData")
                        {
                            asBSON["secretBaseLaboratoryID"] = Globals.TWorld.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                            asBSON["isActivated"] = true;
                            Globals.WorldController.ActivateBluePortal(Globals.CurrentMapPoint);
                        }
                        else
                        {
                            if (asBSON["class"].stringValue == "NetherGroupPortalData")
                            {
                                asBSON["netherworldID"] = Globals.TWorld.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                                asBSON["isActivated"] = true;
                                Globals.WorldController.ActivateNetherGroupPortal(Globals.CurrentMapPoint);
                            }
                            else
                            {
                                bool flag8 = asBSON["class"].stringValue == "JetRaceGroupPortalData";
                                if (!flag8)
                                {
                                    Globals.DoCustomNotification("Please stand on a Blue / Red / JetRace Portal", Globals.CurrentMapPoint);
                                    return;
                                }
                                asBSON["jetRaceWorldID"] = Globals.TWorld.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                                asBSON["isActivated"] = true;
                                Globals.WorldController.ActivateJetRaceGroupPortal(Globals.CurrentMapPoint);
                            }
                        }
                        asBSON["targetWorldID"] = Globals.NameVis;
                        BSONObject bsonobject = new BSONObject();
                        bsonobject["ID"] = "WIU";
                        bsonobject["WiB"] = asBSON;
                        bsonobject["x"] = Globals.CurrentMapPoint.x;
                        bsonobject["y"] = Globals.CurrentMapPoint.y;
                        bsonobject["PT"] = 1;
                        bsonobject["U"] = "JustARando";
                        Globals.world.WorldItemUpdate(bsonobject);
                        OutgoingMessages.SendWorldItemUpdateMessage(Globals.CurrentMapPoint, Globals.world.GetWorldItemData(Globals.CurrentMapPoint), ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(Globals.CurrentMapPoint)));
                        Globals.Player.GoFromPortal(Globals.CurrentMapPoint);
                    }

                    if (GUI.Button(new Rect(195, 95, 90, 20), "Air Stash"))
                    {
                        string WNK = "Airb<color=#FFFFFC></color>ronzey2024";
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        WorldItemBase worldItemData = Globals.world.GetWorldItemData(Globals.CurrentMapPoint);
                        BSONObject asBSON = worldItemData.GetAsBSON();
                        if (asBSON["class"].stringValue == "BluePortalData")
                        {
                            asBSON["secretBaseLaboratoryID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                            asBSON["isActivated"] = true;
                            Globals.WorldController.ActivateBluePortal(Globals.CurrentMapPoint);
                        }
                        else
                        {
                            if (asBSON["class"].stringValue == "NetherGroupPortalData")
                            {
                                asBSON["netherworldID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                                asBSON["isActivated"] = true;
                                Globals.WorldController.ActivateNetherGroupPortal(Globals.CurrentMapPoint);
                            }
                            else
                            {
                                bool flag8 = asBSON["class"].stringValue == "JetRaceGroupPortalData";
                                if (!flag8)
                                {
                                    InfoPopupUI.SetupInfoPopup("ERROR!", "Please stand on a Blue / Red / JetRace Portal");
                                    InfoPopupUI.ForceShowMenu();
                                    return;
                                }
                                asBSON["jetRaceWorldID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                                asBSON["isActivated"] = true;
                                Globals.WorldController.ActivateJetRaceGroupPortal(Globals.CurrentMapPoint);
                            }
                        }
                        asBSON["targetWorldID"] = "NETHERWORLD";
                        BSONObject bsonobject = new BSONObject();
                        bsonobject["ID"] = "WIU";
                        bsonobject["WiB"] = asBSON;
                        bsonobject["x"] = Globals.CurrentMapPoint.x;
                        bsonobject["y"] = Globals.CurrentMapPoint.y;
                        bsonobject["PT"] = 1;
                        bsonobject["U"] = "JustARando";
                        Globals.world.WorldItemUpdate(bsonobject);
                        OutgoingMessages.SendWorldItemUpdateMessage(Globals.CurrentMapPoint, Globals.world.GetWorldItemData(Globals.CurrentMapPoint), ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(Globals.CurrentMapPoint)));
                        Globals.Player.GoFromPortal(Globals.CurrentMapPoint);
                    }
                    /*
                    break;
                case Tab.Temp:

                    
                    GUI.Label(new Rect(15, 55, 10000, 10000), "IK");
                    Globals.INK = GUI.TextArea(new Rect(15, 85, 130, 25), Globals.INK);
                    GUI.Label(new Rect(15, 155, 10000, 10000), "Item Amount");
                    Globals.INKAMT = GUI.TextArea(new Rect(15, 185, 100, 25), Globals.INKAMT);

                    GUI.Label(new Rect(215, 55, 10000, 10000), "Type (use 2)");
                    Globals.INKKey = GUI.TextArea(new Rect(215, 85, 100, 25), Globals.INKKey);

                    if (GUI.Button(new Rect(215, 150, 120, 25), "Create?"))
                    {
                        if (Globals.world.worldName == "NETHERWORLD" && !Globals.world.worldID.ToLower().Contains("nether"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                            IK.blockType = (World.BlockType)int.Parse(Globals.INK);
                            IK.itemType = (PlayerData.InventoryItemType)int.Parse(Globals.INKKey);
                            short nig = (short)int.Parse(Globals.INKAMT);
                            OutgoingMessages.SendDropItemMessage(Globals.CurrentMapPoint, IK, nig, Globals.PlayerData.GetInventoryData(IK));
                        }
                    }

                    if (GUI.Button(new Rect(215, 185, 120, 25), "Create? Dir."))
                    {
                        Vector2i PDir = Globals.Player.GetNextPlayerPositionBasedOnLookDirection(0);
                        if (Globals.world.worldName == "NETHERWORLD" && !Globals.world.worldID.ToLower().Contains("nether"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                            IK.blockType = (World.BlockType)int.Parse(Globals.INK);
                            IK.itemType = (PlayerData.InventoryItemType)int.Parse(Globals.INKKey);
                            short nig = (short)int.Parse(Globals.INKAMT);
                            OutgoingMessages.SendDropItemMessage(PDir, IK, nig, Globals.PlayerData.GetInventoryData(IK));
                        }
                    }

                    if (GUI.Button(new Rect(215, 215, 120, 25), "Clear All"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.INKKey = "";
                        Globals.INK = "";
                        Globals.INKAMT = "";
                    }

                    if (GUI.Button(new Rect(215, 255, 120, 25), "Page 2"))
                    {
                        tab = Tab.DExtra;
                    }

                    Globals.IRef = GUI.Toggle(new Rect(15, 235, 100, 20), Globals.IRef, "Inv Ref");

                    if (!Globals.GemGemGem)
                    {
                        if (GUI.Button(new Rect(15, 280, 130, 25), "Gemmer OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.GemGemGem = true;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(15, 280, 130, 25), "Gemmer ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.GemGemGem = false;
                        }
                    }

                    if (!Globals.BytesBuyer2)
                    {
                        if (GUI.Button(new Rect(15, 305, 130, 25), "BBuyer2 OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.BytesBuyer2 = true;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(15, 305, 130, 25), "BBuyer2 ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.BytesBuyer2 = false;
                        }
                    }


                    if (!Globals.BytesBuyer)
                    {
                        if (GUI.Button(new Rect(15, 335, 130, 25), "BBuyer OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.BytesBuyer = true;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(15, 335, 130, 25), "BBuyer ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.BytesBuyer = false;
                        }
                    }


                    Globals.Speed = GUI.HorizontalSlider(new Rect(15, 370, 100, 20), Globals.Speed, 1f, 5f);

                    int roundedValueSpTime = (int)Math.Round(Globals.Speed);

                    GUI.Label(new Rect(165f, 340f, 2000000000000f, 33f), "Speed: " + string.Format("[{0}]" + " times every Frame.", roundedValueSpTime));
                    */
                    break;
                case Tab.DExtra:

                    if (!Globals.BytesBuyer2)
                    {
                        if (GUI.Button(new Rect(15, 125, 130, 25), "ByteBuyer OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.BytesBuyer2 = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 125, 130, 25), "ByteBuyer ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.BytesBuyer2 = false;
                        }
                    }
                    /*
                    if (!Globals.PLCv)
                    {
                        if (GUI.Button(new Rect(155, 125, 130, 25), "PL Converter OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.PLCv = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(155, 125, 130, 25), "PL Converter ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.PLCv = false;
                        }
                    }
                    */
                    Globals.Speed = GUI.HorizontalSlider(new Rect(15, 85, 100, 20), Globals.Speed, 1f, 5f);

                    int roundedValueBSpTime2 = (int)Math.Round(Globals.Speed);

                    GUI.Label(new Rect(15f, 55f, 2000000000000f, 33f), "ByteSpeed: " + string.Format("[{0}]" + " times every Frame.", roundedValueBSpTime2));

                    if (!Globals.GemmerOG)
                    {
                        if (GUI.Button(new Rect(15, 155, 130, 25), "OG-Gem OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.GemmerOG = true;
                            Globals.GemGemGem = false;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(15, 155, 130, 25), "OG-Gem ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.GemmerOG = false;
                        }
                    }

                    if (!Globals.GemGemGem)
                    {
                        if (GUI.Button(new Rect(165, 155, 130, 25), "FastGem OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.GemGemGem = true;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(165, 155, 130, 25), "FastGem ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.GemGemGem = false;
                        }
                    }

                    if (!Globals.ByteGemmer)
                    {
                        if (GUI.Button(new Rect(15, 255, 130, 25), "ByteGemmer OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.ByteGemmer = true;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(15, 255, 130, 25), "ByteGemmer ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.ByteGemmer = false;
                        }
                    }

                    if (GUI.Button(new Rect(15, 315, 130, 25), "More"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.CashExtra;
                    }

                    Globals.IRef = GUI.Toggle(new Rect(15, 185, 130, 20), Globals.IRef, "Inventory Refresher");

                    Globals.autoVIP = GUI.Toggle(new Rect(165, 185, 90, 20), Globals.autoVIP, "Auto-VIP");

                    Globals.autoVIP2 = GUI.Toggle(new Rect(165, 225, 90, 20), Globals.autoVIP2, "Auto-VIP 2");

                  
                    if (GUI.Button(new Rect(165, 255, 90, 25), "AutoVIP Info"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        BluePopupUI.SetPopupValue(PopupMode.JustClose, "", "How to use", "AutoVIP 1 - Stand on a duped Mining Token seed, and enable AutoVIP.<br <br AutoVIP 2 - Stand on a duped JetRace token seed, and enable AutoVIP.<br <br Yes, they both spam the Mining Wheel and JetRace Wheel packets.<br Huge credit to JED5729", "Continue", "", null, null, 0, 0, false, false, false);
                        ControllerHelper.rootUI.OnOrOffMenu(Il2CppType.Of<BluePopupUI>());
                    }

                    if (GUI.Button(new Rect(15, 225, 90, 25), "Back?"))
                    {
                        tab = Tab.Main;
                    }


                    /*if (GUI.Button(new Rect(250, 115, 130, 20), "DStsh"))
                    {
                        string WNK = "Airb<color=#FFFFFC></color>ronzey2024";
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        WorldItemBase worldItemData = Globals.world.GetWorldItemData(Globals.CurrentMapPoint);
                        BSONObject asBSON = worldItemData.GetAsBSON();
                        if (asBSON["class"].stringValue == "BluePortalData")
                        {
                            asBSON["secretBaseLaboratoryID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                            asBSON["isActivated"] = true;
                            Globals.WorldController.ActivateBluePortal(Globals.CurrentMapPoint);
                        }
                        else
                        {
                            if (asBSON["class"].stringValue == "NetherGroupPortalData")
                            {
                                asBSON["netherworldID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                                asBSON["isActivated"] = true;
                                Globals.WorldController.ActivateNetherGroupPortal(Globals.CurrentMapPoint);
                            }
                            else
                            {
                                bool flag8 = asBSON["class"].stringValue == "JetRaceGroupPortalData";
                                if (!flag8)
                                {
                                    InfoPopupUI.SetupInfoPopup("ERROR!", "Please stand on a Blue / Red / JetRace Portal");
                                    InfoPopupUI.ForceShowMenu();
                                    return;
                                }
                                asBSON["jetRaceWorldID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                                asBSON["isActivated"] = true;
                                Globals.WorldController.ActivateJetRaceGroupPortal(Globals.CurrentMapPoint);
                            }
                        }
                        asBSON["targetWorldID"] = "NETHERWORLD";
                        BSONObject bsonobject = new BSONObject();
                        bsonobject["ID"] = "WIU";
                        bsonobject["WiB"] = asBSON;
                        bsonobject["x"] = Globals.CurrentMapPoint.x;
                        bsonobject["y"] = Globals.CurrentMapPoint.y;
                        bsonobject["PT"] = 1;
                        bsonobject["U"] = "JustARando";
                        Globals.world.WorldItemUpdate(bsonobject);
                        OutgoingMessages.SendWorldItemUpdateMessage(Globals.CurrentMapPoint, Globals.world.GetWorldItemData(Globals.CurrentMapPoint), ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(Globals.CurrentMapPoint)));
                        Globals.Player.GoFromPortal(Globals.CurrentMapPoint);
                    }
                    */

                    break;
                case Tab.CashExtra:

                    if (!Globals.AutoVendor)
                    {
                        if (GUI.Button(new Rect(15, 55, 130, 25), "AutoVendor OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.AutoVendor = true;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(15, 55, 130, 25), "AutoVendor ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.AutoVendor = false;
                        }
                    }

                    Globals.EndAmtVend2 = GUI.HorizontalSlider(new Rect(15, 125, 300, 20), Globals.EndAmtVend2, 100f, 999f);

                    int roundedValueVB = (int)Math.Round(Globals.EndAmtVend2);

                    GUI.Label(new Rect(15, 90f, 2000000000000f, 33f), string.Format("AutoVendor will stop at " + " {0} " + " amount of the item.", roundedValueVB));

                    Globals.GetOpen = GUI.Toggle(new Rect(15, 155, 130, 25), Globals.GetOpen, "Get Auto-Open ID");

                    if (!Globals.autoOPENER)
                    {
                        if (GUI.Button(new Rect(15, 185, 130, 25), "Auto-Opener OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.autoOPENER = true;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(15, 185, 130, 25), "Auto-Opener ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.autoOPENER = false;
                            InfoPopupUI.SetupInfoPopup("Auto-Opener OFF!", "Disabled manually by user. \r\n InventoryKey has been reset.");
                            Globals.AOINVK = 0;
                            InfoPopupUI.ForceShowMenu();
                        }
                    }

                    Globals.CTimer = GUI.HorizontalSlider(new Rect(15, 245, 100, 20), Globals.CTimer, 0.05f, 1f);

                    int roundedValueCSpeed = (int)Globals.CTimer;
                    
                    GUI.Label(new Rect(15f, 215f, 2000000000000f, 33f), "Collect Time: Every " + string.Format("[{0}]" + " seconds.", roundedValueCSpeed));

                    Globals.collectAll = GUI.Toggle(new Rect(15, 285, 100, 20), Globals.collectAll, "Giveaway Mode");

                    break;
                case Tab.MyTab:



                    break;
                case Tab.Main: // Main Tab

                    Globals.GodMode = GUI.Toggle(new Rect(10, 55, 85, 25), Globals.GodMode, "GodMode");
                    Globals.AutoMath = GUI.Toggle(new Rect(130, 55, 90, 25), Globals.AutoMath, "AutoMath");

                    if (GUI.Button(new Rect(255, 55, 135, 25), "PDupe"))
                    {
                        tab = Tab.Portal;
                    }

                    if (GUI.Button(new Rect(255, 85, 135, 25), "Status Editor"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        statusGUI = true;
                    }

                    Globals.PCrafter = GUI.Toggle(new Rect(255, 115, 90, 25), Globals.PCrafter, "MiningGear");
                    Globals.FCrafter = GUI.Toggle(new Rect(255, 145, 90, 25), Globals.FCrafter, "FishingGear");


                    if (GUI.Button(new Rect(255, 175, 90, 25), "My Tab"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.MyTab;
                    }
                    Globals.autoPickup= GUI.Toggle(new Rect(255, 205, 90, 25), Globals.autoPickup, "Auto Pickup");

                    /*
                    if (GUI.Button(new Rect(255, 85, 135, 25), "RBT"))
                    {
                        int RN = random.Next(1, 500);
                        string WNK = "DYNAMIC_THEBLACKTOWER" + RN;
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        WorldItemBase worldItemData = Globals.world.GetWorldItemData(Globals.CurrentMapPoint);
                        BSONObject asBSON = worldItemData.GetAsBSON();
                        if (asBSON["class"].stringValue == "BluePortalData")
                        {
                            asBSON["secretBaseLaboratoryID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                            asBSON["isActivated"] = true;
                            Globals.WorldController.ActivateBluePortal(Globals.CurrentMapPoint);
                        }
                        else
                        {
                            if (asBSON["class"].stringValue == "NetherGroupPortalData")
                            {
                                asBSON["netherworldID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                                asBSON["isActivated"] = true;
                                Globals.WorldController.ActivateNetherGroupPortal(Globals.CurrentMapPoint);
                            }
                            else
                            {
                                bool flag8 = asBSON["class"].stringValue == "JetRaceGroupPortalData";
                                if (!flag8)
                                {
                                    InfoPopupUI.SetupInfoPopup("ERROR!", "Please stand on a Blue / Red / JetRace Portal");
                                    InfoPopupUI.ForceShowMenu();
                                    return;
                                }
                                asBSON["jetRaceWorldID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                                asBSON["isActivated"] = true;
                                Globals.WorldController.ActivateJetRaceGroupPortal(Globals.CurrentMapPoint);
                            }
                        }
                        asBSON["targetWorldID"] = "THEBLACKTOWER";
                        BSONObject bsonobject = new BSONObject();
                        bsonobject["ID"] = "WIU";
                        bsonobject["WiB"] = asBSON;
                        bsonobject["x"] = Globals.CurrentMapPoint.x;
                        bsonobject["y"] = Globals.CurrentMapPoint.y;
                        bsonobject["PT"] = 1;
                        bsonobject["U"] = "JustARando";
                        Globals.world.WorldItemUpdate(bsonobject);
                        OutgoingMessages.SendWorldItemUpdateMessage(Globals.CurrentMapPoint, Globals.world.GetWorldItemData(Globals.CurrentMapPoint), ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(Globals.CurrentMapPoint)));
                        Globals.Player.GoFromPortal(Globals.CurrentMapPoint);
                    }

                    /*
                    if (GUI.Button(new Rect(255, 85, 90, 20), "DStsh"))
                    {
                        string WNK = "Airb<color=#FFFFFC></color>ronzey2024";
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        WorldItemBase worldItemData = Globals.world.GetWorldItemData(Globals.CurrentMapPoint);
                        BSONObject asBSON = worldItemData.GetAsBSON();
                        if (asBSON["class"].stringValue == "BluePortalData")
                        {
                            asBSON["secretBaseLaboratoryID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                            asBSON["isActivated"] = true;
                            Globals.WorldController.ActivateBluePortal(Globals.CurrentMapPoint);
                        }
                        else
                        {
                            if (asBSON["class"].stringValue == "NetherGroupPortalData")
                            {
                                asBSON["netherworldID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                                asBSON["isActivated"] = true;
                                Globals.WorldController.ActivateNetherGroupPortal(Globals.CurrentMapPoint);
                            }
                            else
                            {
                                bool flag8 = asBSON["class"].stringValue == "JetRaceGroupPortalData";
                                if (!flag8)
                                {
                                    InfoPopupUI.SetupInfoPopup("ERROR!", "Please stand on a Blue / Red / JetRace Portal");
                                    InfoPopupUI.ForceShowMenu();
                                    return;
                                }
                                asBSON["jetRaceWorldID"] = WNK.Replace('\n'.ToString(), "").Replace('\r'.ToString(), "");
                                asBSON["isActivated"] = true;
                                Globals.WorldController.ActivateJetRaceGroupPortal(Globals.CurrentMapPoint);
                            }
                        }
                        asBSON["targetWorldID"] = "NETHERWORLD";
                        BSONObject bsonobject = new BSONObject();
                        bsonobject["ID"] = "WIU";
                        bsonobject["WiB"] = asBSON;
                        bsonobject["x"] = Globals.CurrentMapPoint.x;
                        bsonobject["y"] = Globals.CurrentMapPoint.y;
                        bsonobject["PT"] = 1;
                        bsonobject["U"] = "JustARando";
                        Globals.world.WorldItemUpdate(bsonobject);
                        OutgoingMessages.SendWorldItemUpdateMessage(Globals.CurrentMapPoint, Globals.world.GetWorldItemData(Globals.CurrentMapPoint), ConfigData.GetToolUsableForBlock(Globals.world.GetBlockType(Globals.CurrentMapPoint)));
                        Globals.Player.GoFromPortal(Globals.CurrentMapPoint);
                    }
                    */

                    /*Globals.IRef = GUI.Toggle(new Rect(255, 115, 100, 20), Globals.IRef, "IRef"); */

                    Globals.MuteGMs = GUI.Toggle(new Rect(130, 75, 90, 25), Globals.MuteGMs, "Mute GMs");
                    Globals.WLPlacer = GUI.Toggle(new Rect(130, 95, 90, 25), Globals.WLPlacer, "Auto-WL");
                    Globals.ignoreFwk = GUI.Toggle(new Rect(130, 115, 90, 25), Globals.ignoreFwk, "Anti-Fireworks");
                    Globals.NiceTry = GUI.Toggle(new Rect(130, 135, 105, 25), Globals.NiceTry, "Ban Firework");
                    Globals.KFwk = GUI.Toggle(new Rect(130, 155, 105, 25), Globals.KFwk, "Kick Firework");
                    Globals.PlaceholderBool = GUI.Toggle(new Rect(130, 175, 90, 25), Globals.PlaceholderBool, "Placeholder");
                    Globals.refOnCollect = GUI.Toggle(new Rect(130, 195, 115, 25), Globals.refOnCollect, "IRef on Collect");
                    Globals.IRef = GUI.Toggle(new Rect(130, 215, 90, 20), Globals.IRef, "Inv. Refresh");
                    Globals.Custompack = GUI.Toggle(new Rect(130, 235, 90, 25), Globals.Custompack, "C-Pack");
                    GUI.Label(new Rect(130, 255, 200, 10000), "World IP: " + Globals.worldIP);
                    Globals.FlyHack = GUI.Toggle(new Rect(10, 75, 85, 25), Globals.FlyHack, "Fly");
                    Globals.AntiVortex = GUI.Toggle(new Rect(10, 95, 95, 25), Globals.AntiVortex, "Anti-Vortex");
                    Globals.AntiPush = GUI.Toggle(new Rect(10, 115, 105, 25), Globals.AntiPush, "No KnockBack");
                    Globals.BlockKill = GUI.Toggle(new Rect(10, 135, 110, 25), Globals.BlockKill, "Block Kill");
                    Globals.InstantRes = GUI.Toggle(new Rect(10, 155, 95, 25), Globals.InstantRes, "Instant Res");
                    Globals.InfiniteText = GUI.Toggle(new Rect(10, 175, 95, 25), Globals.InfiniteText, "Long Text");
                    Globals.AntiCollect = GUI.Toggle(new Rect(10, 195, 85, 25), Globals.AntiCollect, "Anti-Collect");
                    Globals.AntiBounce = GUI.Toggle(new Rect(10, 215, 85, 25), Globals.AntiBounce, "Anti-Bounce");
                    Globals.Test = GUI.Toggle(new Rect(10, 235, 85, 25), Globals.Test, "Test");
                    Globals.SpikeBomber = GUI.Toggle(new Rect(10, 255, 105, 25), Globals.SpikeBomber, "Annoy Sounds");
                    Globals.VisualName = GUI.Toggle(new Rect(10, 275, 105, 25), Globals.VisualName, "Colored Name");

                    if (GUI.Button(new Rect(100, 350, 90, 25), "AUTOBUY"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.AutoBuy;
                    }

                    if (GUI.Button(new Rect(190, 320, 90, 25), "AUTOGM"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.GMer9000;
                    }


                    if (!Globals.Fwker)
                    {
                        if (GUI.Button(new Rect(190, 350, 130, 25), "Firework OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Fwker = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(190, 350, 130, 25), "Firework ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Fwker = false;
                        }
                    }

                    if (GUI.Button(new Rect(10, 320, 90, 25), "Support us?"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        SceneLoader.CheckIfWeCanGoFromWorldToWorld("GIFTBRONZE", "", null, false, null);
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                    }
                    if (GUI.Button(new Rect(10, 350, 90, 25), "Join Discord"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        UnityEngine.Application.OpenURL("https://discord.gg/aKTa85hrwG");
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                    }

                    /*
                    if (GUI.Button(new Rect(190, 320, 90, 25), "<color=green>???</color>"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.Temp;
                    }
                    */

                    if (GUI.Button(new Rect(100, 320, 90, 25), "More"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.Main2;
                    }
                    break;
                case Tab.Main2:



                    /* if (GUI.Button(new Rect(15, 55, 90, 25), "TTT Hack"))
                     {
                         Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                         tab = Tab.TTTHack;
                     }
                    */

                    /*   break;
                   case Tab.TTTHack:

                       GUI.Label(new Rect(15, 55, 10000, 10000), "[You Start] Tab");
                       if (GUI.Button(new Rect(165, 55, 110, 25), "Opponent Starts"))
                       {
                           Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           tab = Tab.TTTHack2;
                       }

                       if (!Globals.Fwker)
                       {
                           if (GUI.Button(new Rect(15, 100, 50, 50), "X"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(15, 100, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       if (!Globals.Fwker)
                       {
                           if (GUI.Button(new Rect(15, 150, 50, 50), "X"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(15, 150, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       if (!Globals.Fwker)
                       {
                           if (GUI.Button(new Rect(15, 200, 50, 50), "X"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(15, 200, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       if (!Globals.Fwker)
                       {
                           if (GUI.Button(new Rect(85, 100, 50, 50), "X"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(85, 100, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       if (!Globals.Fwker)
                       {
                           if (GUI.Button(new Rect(85, 150, 50, 50), "X"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(85, 150, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       if (!Globals.Fwker)
                       {
                           if (GUI.Button(new Rect(85, 200, 50, 50), "X"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(85, 200, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       if (!Globals.Fwker)
                       {
                           if (GUI.Button(new Rect(155, 100, 50, 50), "X"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(155, 100, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       if (!Globals.Fwker)
                       {
                           if (GUI.Button(new Rect(155, 150, 50, 50), "X"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(155, 150, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       if (!Globals.Fwker)
                       {
                           if (GUI.Button(new Rect(155, 200, 50, 50), "X"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(155, 200, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       if (GUI.Button(new Rect(15, 285, 100, 25), "Reset Values?"))
                       {

                       }

                       break;
                   case Tab.TTTHack2:
                       GUI.Label(new Rect(15, 55, 10000, 10000), "[Opponent Starts] Tab");
                       if (GUI.Button(new Rect(165, 55, 110, 25), "You Start"))
                       {
                           Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           tab = Tab.TTTHack;
                       }

                       if (!Globals.TT)
                       {
                           if (GUI.Button(new Rect(15, 100, 50, 50), "Y"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                               Globals.TT = true;
                               Globals.TT4 = true;
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(15, 100, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                               Globals.TT = false;
                               Globals.TT4 = false;
                           }
                       }
                       if (!Globals.TT2)
                       {
                           if (GUI.Button(new Rect(15, 150, 50, 50), "Y"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(15, 150, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       if (!Globals.TT3)
                       {
                           if (GUI.Button(new Rect(15, 200, 50, 50), "Y"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       else
                       {
                           if (GUI.Button(new Rect(15, 200, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       if (!Globals.TT4)
                       {
                           if (GUI.Button(new Rect(85, 100, 50, 50), "Y"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       else
                       {
                           if (GUI.Button(new Rect(85, 100, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       if (!Globals.TT5)
                       {
                           if (GUI.Button(new Rect(85, 150, 50, 50), "Y"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       else
                       {
                           if (GUI.Button(new Rect(85, 150, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       if (!Globals.TT6)
                       {
                           if (GUI.Button(new Rect(85, 200, 50, 50), "Y"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       else
                       {
                           if (GUI.Button(new Rect(85, 200, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       if (!Globals.TT6)
                       {
                           if (GUI.Button(new Rect(155, 100, 50, 50), "Y"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(155, 100, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       if (!Globals.TT7)
                       {
                           if (GUI.Button(new Rect(155, 150, 50, 50), "Y"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(155, 150, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       if (!Globals.TT8)
                       {
                           if (GUI.Button(new Rect(155, 200, 50, 50), "Y"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }
                       else
                       {
                           if (GUI.Button(new Rect(155, 200, 50, 50), "O"))
                           {
                               Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                           }
                       }

                       if (GUI.Button(new Rect(15, 285, 100, 25), "Reset Values?"))
                       {

                       }
                       */
                    break;
                case Tab.AutoBuy:


                    if (!Globals.AutoBuy1)
                    {
                        if (GUI.Button(new Rect(15, 55, 130, 25), "AUTOBUY OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.AutoBuy1 = true;
                            InfoPopupUI.SetupInfoPopup("AutoBuyer ENABLED!", "Buy an item from gem shop to start!");
                            InfoPopupUI.ForceShowMenu();
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(15, 55, 130, 25), "AUTOBUY ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.AutoBuy1 = false;
                            InfoPopupUI.SetupInfoPopup("AutoBuyer DISABLED!", "");
                            InfoPopupUI.ForceShowMenu();
                        }
                    }

                    GUI.Label(new Rect(155, 55, 10000, 10000), "Current: " + Globals.IPID1);

                    GUI.Label(new Rect(10f, 85f, 2000000000000f, 33f), "Speed: every " + string.Format("[{0}]" + " seconds.", Globals.AutoBuyCT));

                    Globals.AutoBuyCT = GUI.HorizontalSlider(new Rect(10, 115, 100, 20), Globals.AutoBuyCT, 0.01f, 5f);

                    Globals.AutoBuyCT2 = GUI.TextArea(new Rect(10, 165, 70, 20), Globals.AutoBuyCT2);

                    if (GUI.Button(new Rect(15, 195, 100, 20), "Set Speed"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.AutoBuyCT = float.Parse(Globals.AutoBuyCT2);
                    }


                    if (GUI.Button(new Rect(15, 215, 100, 25), "Reset All"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.AutoBuyCT = 1f;
                        Globals.AutoBuy1 = false;
                        Globals.IPID1 = "";
                    }

                    break;
                case Tab.GMer9000:

                    Globals.autoGM = GUI.Toggle(new Rect(10, 55, 120, 25), Globals.autoGM, "Get Base64 Msg");

                    if (GUI.Button(new Rect(20, 85, 145, 25), "READ ME! Important!"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        BluePopupUI.SetPopupValue(PopupMode.JustClose, "", "Important Notice!", "To begin this, turn on \"Get Base64 Msg\", you have to Global Message manually <u>ONCE</u>, so we can get & save your message. The toggle will be automatically turned off afterwards.</u>. <br <br Please click the reset button upon enterring a different world / account as you may get permaban!", "Understood.", "", null, null, 0, 0, false, false, false);
                        ControllerHelper.rootUI.OnOrOffMenu(Il2CppType.Of<BluePopupUI>());
                    }

                    Globals.StartGM = GUI.Toggle(new Rect(10, 125, 120, 25), Globals.StartGM, "Auto GM");

                    if (GUI.Button(new Rect(10, 155, 100, 25), "Reset"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.autoGM = false;
                        Globals.StartGM = false;
                        Globals.Base64Msg = "";
                        MelonLogger.Msg(Globals.Base64Msg);
                    }

                    Globals.GMSpeed = GUI.HorizontalSlider(new Rect(10, 225, 100, 20), Globals.GMSpeed, 0.1f, 1f);

                    Globals.CAmtSpd = GUI.TextArea(new Rect(130, 225, 70, 20), Globals.CAmtSpd);

                    if (GUI.Button(new Rect(200, 225, 90, 20), "Set Time"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.GMSpeed = float.Parse(Globals.CAmtSpd);
                    }

                    if (GUI.Button(new Rect(300, 225, 90, 20), "Reset Time"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.GMSpeed = 0.5f;
                    }

                    GUI.Label(new Rect(10f, 185f, 2000000000000f, 33f), "GM Speed: every " + string.Format("[{0}]" + " seconds.", Globals.GMSpeed));

                    GUI.Label(new Rect(10, 250, 10000, 20), "Minimum Recommended Speed: 0.5");
                    GUI.Label(new Rect(10, 300, 10000, 40), "Otherwise, you may experience disconnects!");

                    break;
                case Tab.Data: // Data Tab

                    Globals.cmIncoming = GUI.Toggle(new Rect(15, 55, 120, 25), Globals.cmIncoming, "Capture Incoming");

                    Globals.cmOutgoing = GUI.Toggle(new Rect(15, 85, 120, 25), Globals.cmOutgoing, "Capture Outgoing");

                    if (!Globals.ViewOcaptureOrICapture)
                    {
                        if (GUI.Button(new Rect(190, 55, 150, 25), "Outgoing Packet:"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.ViewOcaptureOrICapture = true;
                        }

                        Globals.CurrentCaptureOUTGOING = GUI.TextArea(new Rect(155, 85, 220, 250), Globals.CurrentCaptureOUTGOING);
                    }

                    else
                    {
                        if (GUI.Button(new Rect(190, 55, 150, 25), "Incoming Packet:"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.ViewOcaptureOrICapture = false;
                        }

                        Globals.CurrentCaptureINCOMING = GUI.TextArea(new Rect(155, 85, 220, 250), Globals.CurrentCaptureINCOMING);
                    }

                    if (GUI.Button(new Rect(15, 115, 120, 25), "Send Packet"))
                    {
                        if (!Globals.ViewOcaptureOrICapture)
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.CustomPacket = Globals.CurrentCaptureOUTGOING;
                        }
                        if (Globals.ViewOcaptureOrICapture)
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.CustomPacket = Globals.CurrentCaptureINCOMING;
                        }

                        SendCustomPacket();
                    }

                    if (GUI.Button(new Rect(15, 145, 120, 25), "Load Packet"))
                    {
                        string FilePath = AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Packets\\" + Globals.CPacketFile + ".json";
                        if (!Globals.ViewOcaptureOrICapture)
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.CurrentCaptureOUTGOING = File.ReadAllText(FilePath);
                        }

                        if (Globals.ViewOcaptureOrICapture)
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.CurrentCaptureINCOMING = File.ReadAllText(FilePath);
                        }
                    }

                    if (GUI.Button(new Rect(15, 175, 120, 25), "Save Packet"))
                    {
                        if (!Globals.ViewOcaptureOrICapture)
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            FileStream FileStream = File.Open(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Packets\\" + Globals.CPacketFile + ".json", FileMode.Create);
                            FileStream.Close();
                            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Packets\\" + Globals.CPacketFile + ".json", Globals.CurrentCaptureOUTGOING);
                        }

                        if (Globals.ViewOcaptureOrICapture)
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            FileStream FileStream = File.Open(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Packets\\" + Globals.CPacketFile + ".json", FileMode.Create);
                            FileStream.Close();
                            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Packets\\" + Globals.CPacketFile + ".json", Globals.CurrentCaptureINCOMING);
                        }
                    }

                    if (GUI.Button(new Rect(15, 205, 120, 25), "More"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.PacketExtra;
                    }

                    Globals.PSpamP = GUI.Toggle(new Rect(15, 235, 120, 25), Globals.PSpamP, "Spam Packet");

                    if (GUI.Button(new Rect(15, 265, 120, 25), "Spam Settings"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.SpamSettingsPacket;
                    }


                    if (GUI.Button(new Rect(15, 295, 120, 25), "Auto XP " + (Globals.solvingFossils ? "(ON)" : "(OFF)")))
                    {
                        Globals.solvingFossils = !Globals.solvingFossils;
                        if (Globals.solvingFossils)
                        {
                            StartAutoSolveFossil();
                        }
                        else
                        {
                            StopAutoSolveFossil();
                        }
                    }
                    break;
                case Tab.SpamSettingsPacket:

                    GUI.Label(new Rect(10f, 55f, 2000000000000f, 33f), "Packet Spam Speed: every " + string.Format("[{0}]" + " seconds.", Globals.PTimerP2));

                    Globals.PTimerP2 = GUI.HorizontalSlider(new Rect(10, 85, 100, 20), Globals.PTimerP2, 0.1f, 10f);

                    GUI.Label(new Rect(10, 105, 10000, 10000), "Custom SpamTimer:");

                    Globals.PTimerP3 = GUI.TextArea(new Rect(10, 130, 100, 20), Globals.PTimerP3);

                    if (GUI.Button(new Rect(10, 160, 105, 20), "Set SpamTime"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.PTimerP2 = float.Parse(Globals.PTimerP3);
                    }

                    if (GUI.Button(new Rect(10, 190, 120, 20), "Reset SpamTime"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.PTimerP2 = 1f;
                    }

                    GUI.Label(new Rect(10, 220, 10000, 10000), "Recommended Time: 0.5 - 1");

                    Globals.PSpamP = GUI.Toggle(new Rect(10, 255, 120, 25), Globals.PSpamP, "Spam Packet");

                    break;
                case Tab.PacketExtra:

                    GUI.Label(new Rect(15, 55, 10000, 10000), "Save/Load Packet as:");

                    GUI.Label(new Rect(170, 95, 10000, 10000), ".json");
                    Globals.CPacketFile = GUI.TextArea(new Rect(15, 95, 150, 25), Globals.CPacketFile);

                    if (GUI.Button(new Rect(15, 125, 120, 25), "Load Packet"))
                    {
                        string FilePath = AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Packets\\" + Globals.CPacketFile + ".json";
                        if (!Globals.ViewOcaptureOrICapture)
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.CurrentCaptureOUTGOING = File.ReadAllText(FilePath);
                        }

                        if (Globals.ViewOcaptureOrICapture)
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.CurrentCaptureINCOMING = File.ReadAllText(FilePath);
                        }
                    }

                    if (GUI.Button(new Rect(140, 125, 120, 25), "Save Packet"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        FileStream FileStream = File.Open(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Packets\\" + Globals.CPacketFile + ".json", FileMode.Create);
                        FileStream.Close();
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Packets\\" + Globals.CPacketFile + ".json", Globals.CustomPacket);
                    }

                    if (GUI.Button(new Rect(15, 165, 120, 25), "Back?"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.Data;
                    }

                    Globals.ignoreID = GUI.TextArea(new Rect(15, 195, 120, 25), Globals.ignoreID);

                    if (GUI.Button(new Rect(15, 225, 120, 25), "Ignore Outgoing"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);

                        Globals.OutgoingBlock.Add(Globals.ignoreID);
                        string bb33 = "";
                        foreach (string nig1 in Globals.OutgoingBlock)
                        {
                            bb33 = bb33 + nig1 + ", ";
                        }
                        ChatUI.SendMinigameMessage("Outgoing Ignores: " + bb33);
                        MelonLogger.Msg("Outgoing Ignores: " + bb33);
                    }
                    if (GUI.Button(new Rect(135, 225, 120, 25), "Ignore Incoming"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.IncomingBlock.Add(Globals.ignoreID);
                        string bb33 = "";
                        foreach (string nig1 in Globals.IncomingBlock)
                        {
                            bb33 = bb33 + nig1 + ", ";
                        }
                        ChatUI.SendMinigameMessage("Incoming Ignores: " + bb33);
                        MelonLogger.Msg("Incoming Ignores: " + bb33);
                    }
                    if (GUI.Button(new Rect(255, 225, 120, 25), "Reset Ignores"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        ChatUI.SendMinigameMessage("Ignore Packets list cleared!");
                        Globals.IncomingBlock.Clear();
                        Globals.OutgoingBlock.Clear();
                    }

                    break;
                case Tab.Misc: // Misc Tab

                    if (GUI.Button(new Rect(15, 55, 90, 30), "Troll"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.Troll;
                    }
                    if (GUI.Button(new Rect(15, 85, 90, 30), "WL Hack"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.WLPlacing;
                    }


                    if (!Globals.GWarper)
                    {
                        if (GUI.Button(new Rect(15, 230, 130, 25), "Global Finder OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.GWarper = true;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(15, 230, 130, 25), "Global Finder ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.GWarper = false;
                        }
                    }

                    GUI.Label(new Rect(15, 155, 10000, 10000), "Global Finder KEYWORD:");

                    Globals.LocateGM = GUI.TextArea(new Rect(15, 195, 150, 25), Globals.LocateGM);

                    GUI.Label(new Rect(15, 255, 100, 25), "Ignore Worlds:");
                    Globals.GIgn = GUI.TextArea(new Rect(15, 275, 100, 25), Globals.GIgn);

                    if (GUI.Button(new Rect(15, 305, 100, 25), "Ignore World"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.IgnoreGMW.Add(Globals.GIgn.ToLower());
                        ChatUI.SendMinigameMessage("Inoring world: @" + Globals.GIgn);
                    }
                    if (GUI.Button(new Rect(15, 335, 100, 25), "Reset Ignores?"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.IgnoreGMW.Clear();
                        ChatUI.SendMinigameMessage("GM Ignore Worlds reset!");
                    }
                    if (GUI.Button(new Rect(155, 55, 90, 25), "Botting"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.Botting;
                    }

                    break;
                case Tab.Botting:

                    GUI.Label(new Rect(15, 55, 10000, 10000), "Botting \r\n Manual Botting, use alt");

                    GUI.Label(new Rect(15, 85, 100, 100), "Username");
                    Globals.AccountName = GUI.TextArea(new Rect(15, 115, 100, 20), Globals.AccountName);
                    GUI.Label(new Rect(15, 145, 100, 100), "Password");
                    Globals.AccountPassy = GUI.TextArea(new Rect(15, 175, 100, 20), Globals.AccountPassy);

                    if (GUI.Button(new Rect(15, 205, 90, 30), "Login"))
                    {
                        UserIdent.LoginWithUsernameAndPassword(Globals.AccountName, Globals.AccountPassy, false);
                        SceneLoader.ReloadGame();
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.AccountName = "";
                        Globals.AccountPassy = "";
                    }

                    GUI.Label(new Rect(15, 255, 10000, 10000), "Current Account: " + StaticPlayer.theRealPlayername);

                    if (GUI.Button(new Rect(15, 285, 120, 25), "SpamBot"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.SpamBotting;
                    }

                    break;
                case Tab.SpamBotting:


                    GUI.Label(new Rect(15, 65, 10000, 10000), "Write text to spam");

                    Globals.SpamTextOfChoice = GUI.TextArea(new Rect(15, 95, 150, 180), Globals.SpamTextOfChoice);

                    if (!Globals.SpamTexter)
                    {
                        if (GUI.Button(new Rect(15, 295, 160, 25), "Message Spammer OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.SpamTexter = true;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(15, 295, 160, 25), "Message Spammer ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.SpamTexter = false;
                        }
                    }

                    Globals.JoinRandomWorlds = GUI.Toggle(new Rect(15, 325, 130, 25), Globals.JoinRandomWorlds, "Join random worlds");
                    Globals.TeamSwitcher = GUI.Toggle(new Rect(15, 355, 120, 25), Globals.TeamSwitcher, "Skin Blink");
                    Globals.EmoteSpam = GUI.Toggle(new Rect(145, 325, 90, 25), Globals.EmoteSpam, "Spam Emote");
                    Globals.FriendReqSpam = GUI.Toggle(new Rect(115, 355, 150, 25), Globals.FriendReqSpam, "Friend Req Spam");
                    Globals.TradeSpam = GUI.Toggle(new Rect(175, 295, 100, 25), Globals.TradeSpam, "SpamTrade");

                    if (!Globals.MannequinSpam)
                    {
                        if (GUI.Button(new Rect(220, 250, 150, 25), "Mannequin Spam OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.MannequinSpam = true;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(220, 250, 150, 25), "Mannequin Spam ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.MannequinSpam = false;
                        }
                    }

                    if (GUI.Button(new Rect(255, 55, 120, 25), "Settings"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.SpamBottingSettings;
                    }



                    break;
                case Tab.SpamBottingSettings:

                    Globals.SendMsgTime = GUI.HorizontalSlider(new Rect(15, 145, 100, 20), Globals.SendMsgTime, 5f, 15f);

                    int roundedValueMTime = (int)Math.Round(Globals.SendMsgTime);

                    GUI.Label(new Rect(15, 55, 10000, 10000), "How fast should your bot \r\n Spam Messages?");
                    GUI.Label(new Rect(15f, 115f, 2000000000000f, 33f), "Every " + string.Format("[{0}]  " + " Seconds", roundedValueMTime));

                    GUI.Label(new Rect(15, 165, 10000, 10000), "We reccommend using it at 3-4 seconds \r\n to prevent disconnects.");


                    Globals.WorldTime = GUI.HorizontalSlider(new Rect(15, 310, 100, 20), Globals.WorldTime, 10f, 30f);

                    int roundedValueWTime = (int)Math.Round(Globals.WorldTime);

                    GUI.Label(new Rect(15, 230, 10000, 10000), "How fast should your bot \r\n Join/Leave Worlds?");
                    GUI.Label(new Rect(15f, 280f, 2000000000000f, 33f), "Every " + string.Format("[{0}]  " + " Seconds", roundedValueWTime));

                    if (GUI.Button(new Rect(255, 55, 90, 25), "Back?"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.SpamBotting;
                    }

                    break;
                case Tab.Accounts: // Accounts Tab

                    GUI.Label(new Rect(150, 55, 100, 100), "Username");
                    Globals.AccountName = GUI.TextArea(new Rect(150, 85, 100, 20), Globals.AccountName);
                    GUI.Label(new Rect(150, 115, 100, 100), "Password");
                    Globals.AccountPassy = GUI.TextArea(new Rect(150, 145, 100, 20), Globals.AccountPassy);

                    if (GUI.Button(new Rect(150, 175, 90, 30), "Login"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        UserIdent.LoginWithUsernameAndPassword(Globals.AccountName, Globals.AccountPassy, false);
                        SceneLoader.ReloadGame();

                        FileStream FileStream = File.Open(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Accounts\\" + Globals.AccountName + ".json", FileMode.Create);
                        FileStream.Close();
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Accounts\\" + Globals.AccountName + ".json", UserIdent.CognitoID);

                        FileStream FileStream2 = File.Open(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Accounts\\" + Globals.AccountName + "2.json", FileMode.Create);
                        FileStream2.Close();
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Accounts\\" + Globals.AccountName + "2.json", UserIdent.lastLogin);

                        Globals.AccountName = "";
                        Globals.AccountPassy = "";

                        MelonLogger.Msg("Account saved!");
                    }
                    if (GUI.Button(new Rect(260, 90, 130, 30), "Saved Accounts"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.SavedAccs;
                    }
                    if (GUI.Button(new Rect(15, 90, 115, 30), "Refresh Cognito"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        UserIdent.ForceNewCognitoId();
                        SceneLoader.ReloadGame();

                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Accounts\\" + Globals.AccountName + ".json", UserIdent.CognitoID);
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Accounts\\" + Globals.AccountName + "2.json", UserIdent.lastLogin);
                    }

                    if (GUI.Button(new Rect(15, 120, 115, 30), "Get Cognito"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        MelonLogger.Msg(StaticPlayer.theRealPlayername + "'s Cognito:\r\n \r\n Cognito ID: \r\n" + UserIdent.CognitoID + "\r\n \r\n Cognito Key:\r\n" + UserIdent.lastLogin);
                    }

                    if (GUI.Button(new Rect(15, 150, 115, 30), "LogOut"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        UserIdent.LogOut();
                        SceneLoader.ReloadGame();
                    }


                    break;

                case Tab.SavedAccs:

                    GUI.Label(new Rect(150, 55, 100, 100), "Username");

                    Globals.AccountName = GUI.TextArea(new Rect(150, 85, 100, 20), Globals.AccountName);

                    if (GUI.Button(new Rect(150, 115, 90, 30), "Login"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        string FilePath1 = AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Accounts\\" + Globals.AccountName + ".json";
                        string FilePath2 = AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\Accounts\\" + Globals.AccountName + "2.json";

                        ChatUI.SendLogMessage("Logging In..");
                        UserIdent.LogOut();
                        UserIdent.SetCognitoIDAndMarkReady(File.ReadAllText(FilePath1));
                        UserIdent.UpdateLastLogin(File.ReadAllText(FilePath2));
                        SceneLoader.ReloadGame();

                        Globals.AccountName = "";
                        Globals.AccountPassy = "";
                    }

                    GUI.Label(new Rect(150, 150, 150, 150), "Your account names save so you only have to write the username!");


                    break;


                case Tab.Utils: // Utils Tab

                    if (GUI.Button(new Rect(15, 55, 110, 30), "AutoPouch"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.GemPouch;
                    }

                    GUI.Label(new Rect(150, 55, 1000, 1000), "This only opens Basic Card Packs!");

                    if (!Globals.CardPack1)
                    {
                        if (GUI.Button(new Rect(190, 90, 150, 25), "CardPack Opener OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            InfoPopupUI.SetupInfoPopup("Opener Started!", "You may disconnect, if so, turn off the opener.");
                            InfoPopupUI.ForceShowMenu();
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.BlueParticleAppear);
                            Globals.CardPack1 = true;
                        }
                    }

                    else
                    {
                        if (GUI.Button(new Rect(190, 90, 150, 25), "CardPack Opener ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.CardPack1 = false;
                        }
                    }

                    if (GUI.Button(new Rect(190, 140, 120, 30), "Potion Exploits"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.Potions;
                    }


                    if (Globals.SwordPuller = GUI.Toggle(new Rect(15, 85, 105, 25), Globals.SwordPuller, "Sword in Stone"))
                    {


                        if (GUI.Button(new Rect(15, 115, 125, 25), "Info"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            BluePopupUI.SetPopupValue(PopupMode.JustClose, "", "How to use", "This allows you to pull any Sword in Stone!<br Even after it's already been pulled, you can still pull. <br No rights on lock needed to pull them, so you can go to any world and pull!<br Note: The 24hour cooldown still applies!<br <br Have fun!<br by Airbronze <3", "Continue", "", null, null, 0, 0, false, false, false);
                            ControllerHelper.rootUI.OnOrOffMenu(Il2CppType.Of<BluePopupUI>());
                        }

                        if (GUI.Button(new Rect(15, 145, 125, 25), "Begin Exploit"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;
                            OutgoingMessages.SendPullSwordInStone(currentPlayerMapPoint);
                        }

                    }

                    Globals.AnimOnner = GUI.Toggle(new Rect(15, 175, 125, 25), Globals.AnimOnner, "AnimOn Spam");

                    Globals.AnimOffer = GUI.Toggle(new Rect(15, 205, 125, 25), Globals.AnimOffer, "AnimOff Spam");



                    if (!Globals.Gambler)
                    {
                        if (GUI.Button(new Rect(15, 235, 130, 25), "Gambler OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Gambler = true;
                            InfoPopupUI.SetupInfoPopup("Gambler activated!", "Stand on any Wishing Well to start! \r\n To update your BC amount, turn off Gambler");
                            InfoPopupUI.ForceShowMenu();
                            InfoPopupUI.maxWindowLifetime = 100;
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.BlueParticleAppear);
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 235, 130, 25), "Gambler ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Gambler = false;
                            ChatUI.SendLogMessage("Updating BC Amount..");
                            SceneLoader.ReloadGame();
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.BlueParticleDisappear);
                        }
                    }

                    if (GUI.Button(new Rect(15, 265, 130, 25), "Free Panorama"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendTakePhoto(World.BlockType.ConsumableCameraWorld);
                    }

                    if (!Globals.Recycler)
                    {
                        if (GUI.Button(new Rect(15, 295, 130, 25), "Auto-Recycle OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Recycler = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 295, 130, 25), "Auto-Recycle ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Recycler = false;
                        }
                    }

                    GUI.Label(new Rect(15, 325, 10000, 100000), "Select the item in your inventory \r\n you want to recycle!");


                    if (GUI.Button(new Rect(230, 290, 130, 25), "Extra"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.Extra;
                    }

                    if (GUI.Button(new Rect(230, 245, 130, 25), "Skip EvolveTime"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;
                        WorldItemBase wID = Globals.world.GetWorldItemData(currentPlayerMapPoint);
                        Il2CppKernys.Bson.BSONObject BSONwID = wID.GetAsBSON();
                        BSONwID["evolveStartTimeInTicks"] = 0L;
                        BSONwID["level"] = 5;
                        wID.SetViaBSON(BSONwID);
                    }

                    if (GUI.Button(new Rect(230, 340, 130, 25), "Private Servers"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.PrivateServers;
                    }

                    break;
                case Tab.PrivateServers:

                    GUI.Label(new Rect(15, 55, 10000, 10000), "Private Server Tools");

                    GUI.Label(new Rect(15, 75, 1000, 1000), "Block Type");
                    Globals.InventoryKeyBT = GUI.TextArea(new Rect(15, 95, 100, 20), Globals.InventoryKeyBT);

                    GUI.Label(new Rect(15, 125, 1000, 1000), "Inventory Type");
                    Globals.InventoryKeyIT = GUI.TextArea(new Rect(15, 155, 100, 20), Globals.InventoryKeyIT);

                    if (GUI.Button(new Rect(15, 185, 150, 25), "Purchase Custom Item"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;

                        PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                        IK.blockType = (World.BlockType)int.Parse(Globals.InventoryKeyBT);
                        IK.itemType = (PlayerData.InventoryItemType)int.Parse(Globals.InventoryKeyIT);

                        OutgoingMessages.SendBuyOutAuctionHouseItem(currentPlayerMapPoint, IK, 1);
                    }

                    if (!Globals.PWEBuyerAuto)
                    {
                        if (GUI.Button(new Rect(15, 215, 185, 25), "Auto-Buy Custom Item OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.PWEBuyerAuto = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 215, 185, 25), "Auto-Buy Custom Item ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.PWEBuyerAuto = false;
                        }
                    }

                    if (!Globals.CustomItemDrop)
                    {
                        if (GUI.Button(new Rect(15, 245, 185, 25), "Custom Item Dropper OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.CustomItemDrop = true;
                            Globals.AntiCollect = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 245, 185, 25), "Custom Item Dropper ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.CustomItemDrop = false;
                            Globals.AntiCollect = false;
                        }
                    }

                    if (!Globals.RandomCDrop)
                    {
                        if (GUI.Button(new Rect(15, 275, 185, 25), "Random Item Dropper OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.RandomCDrop = true;
                            Globals.AntiCollect = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 275, 185, 25), "Random Item Dropper ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.RandomCDrop = false;
                            Globals.AntiCollect = false;
                        }
                    }

                    if (!Globals.Nuker)
                    {
                        if (GUI.Button(new Rect(15, 305, 150, 25), "World Nuker OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Nuker = true;
                            Globals.AntiCollect = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 305, 150, 25), "World Nuker ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Nuker = false;
                            Globals.AntiCollect = false;
                        }
                    }

                    if (!Globals.CustomSeeder)
                    {
                        if (GUI.Button(new Rect(15, 345, 150, 25), "Seeder OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.CustomSeeder = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 345, 150, 25), "Seeder ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.CustomSeeder = false;
                        }
                    }
                    if (!Globals.Wearer)
                    {
                        if (GUI.Button(new Rect(205, 345, 150, 25), "Wearer OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Wearer = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(205, 345, 150, 25), "Wearer ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Wearer = false;
                        }
                    }

                    if (!Globals.ForcePlace)
                    {
                        if (GUI.Button(new Rect(205, 305, 150, 25), "ForcePlace OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.ForcePlace = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(205, 305, 150, 25), "ForcePlace ON"))
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        {
                            Globals.ForcePlace = false;
                        }
                    }


                    if (!Globals.Bbreak)
                    {
                        if (GUI.Button(new Rect(215, 265, 150, 25), "BBreak OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Bbreak = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(215, 265, 150, 25), "BBreak ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Bbreak = false;
                        }
                    }

                    Globals.BFValue = GUI.TextArea(new Rect(215, 185, 90, 25), Globals.BFValue);


                    if (!Globals.BFarmer)
                    {
                        if (GUI.Button(new Rect(215, 225, 150, 25), "BFarmer OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.BFarmer = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(215, 225, 150, 25), "BFarmer ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.BFarmer = false;
                        }
                    }

                    break;
                case Tab.Extra:

                    GUI.Label(new Rect(15, 55, 10000, 10000), "Select items in your inventory to AutoDrop!");

                    if (!Globals.Dropper)
                    {
                        if (GUI.Button(new Rect(15, 85, 150, 25), "AutoDropper OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Dropper = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 85, 150, 25), "AutoDropper ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Dropper = false;
                        }
                    }

                    GUI.Label(new Rect(15, 125, 10000, 10000), "Select items in your inventory to AutoTrash!");

                    if (!Globals.Trasher)
                    {
                        if (GUI.Button(new Rect(15, 155, 150, 25), "AutoTrasher OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Trasher = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 155, 150, 25), "AutoTrasher ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Trasher = false;
                        }
                    }

                    if (!Globals.Disencht)
                    {
                        if (GUI.Button(new Rect(15, 185, 150, 25), "Disenchanter OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Disencht = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(15, 185, 150, 25), "Disenchanter ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.Disencht = false;
                        }
                    }

                    Globals.captureCard = GUI.Toggle(new Rect(15, 225, 90, 25), Globals.captureCard, "Capture Card");

                    break;
                case Tab.Potions:


                    Globals.PotionID = GUI.HorizontalSlider(new Rect(15, 90, 300, 20), Globals.PotionID, 2303f, 2309f);

                    int roundedValuePot = (int)Math.Round(Globals.PotionID);

                    Globals.PotionName = TextManager.GetBlockTypeName(((World.BlockType)roundedValuePot));

                    GUI.Label(new Rect(125f, 70f, 2000000000000f, 33f), string.Format("[{0}]  " + Globals.PotionName, roundedValuePot));

                    if (!Globals.PotionCrafter)
                    {
                        if (GUI.Button(new Rect(125, 110, 130, 25), "Auto-Cauldron OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.PotionCrafter = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(125, 110, 130, 25), "Auto-Cauldron ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.PotionCrafter = false;
                        }
                    }

                    if (GUI.Button(new Rect(125, 145, 130, 25), "Skip CraftTime"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;
                        WorldItemBase wID = Globals.world.GetWorldItemData(Globals.CurrentMapPoint);
                        Il2CppKernys.Bson.BSONObject BSONwID = wID.GetAsBSON();
                        BSONwID["craftingStartTimeInTicks"] = 0L;
                        wID.SetViaBSON(BSONwID);
                    }

                    break;




                case Tab.GemPouch:

                    Globals.GemPoucher1 = GUI.HorizontalSlider(new Rect(125, 90, 100, 20), Globals.GemPoucher1, 3364f, 3366f);

                    int roundedValueGP = (int)Math.Round(Globals.GemPoucher1);

                    GUI.Label(new Rect(125f, 70f, 130f, 33f), string.Format("[{0}] GemPouch", roundedValueGP));

                    GUI.Label(new Rect(15, 55, 150, 150), "GemPouch IDs \r\n 3364 - Puny\r\n 3365 - Fine \r\n 3366 - Lux");

                    if (!Globals.GemPoucher2)
                    {
                        if (GUI.Button(new Rect(125, 110, 115, 25), "Auto-Pouch OFF"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.GemPoucher2 = true;
                        }
                    }
                    else
                    {
                        if (GUI.Button(new Rect(125, 110, 115, 25), "Auto-Pouch ON"))
                        {
                            Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                            Globals.GemPoucher2 = false;
                        }
                    }

                    if (GUI.Button(new Rect(180, 365, 105, 25), "Back?"))
                    {
                        tab = Tab.Utils;
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                    }

                    break;
                case Tab.Troll: // Troll Tab
                    if (GUI.Button(new Rect(15, 55, 110, 30), "ClothesBugger"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.ClothesBugger;
                    }
                    if (GUI.Button(new Rect(15, 85, 110, 30), "MusicBugger"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        tab = Tab.Music;
                    }

                    if (GUI.Button(new Rect(15, 115, 125, 25), "Donation Bugger"))
                    {
                        Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;

                        PlayerData.InventoryKey IK = new PlayerData.InventoryKey();
                        IK.blockType = World.BlockType.Poop;
                        IK.itemType = PlayerData.InventoryItemType.BlockWater;
                        OutgoingMessages.SendAddItemToDonationBox(currentPlayerMapPoint, Globals.world.GetWorldItemData(currentPlayerMapPoint), IK, -420, null);


                        InfoPopupUI.SetupInfoPopup("Donation Box Bugged!", "Can be used 3 times on any donation box!");
                        InfoPopupUI.ForceShowMenu();
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.HalloweenTowerStart);
                    }

                    Globals.LizoEffect = GUI.Toggle(new Rect(15, 145, 105, 25), Globals.LizoEffect, "Lizo Effect");

                    if (GUI.Button(new Rect(15, 175, 105, 25), "ChestBugger"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        InfoPopupUI.SetupInfoPopup("Unfinished :(", "");
                        InfoPopupUI.ForceShowMenu();
                        InfoPopupUI.maxWindowLifetime = 10f;
                    }

                    if (GUI.Button(new Rect(150, 365, 105, 25), "Back?"))
                    {
                        tab = Tab.Misc;
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                    }

                    break;
                case Tab.Music: // Music Tab

                    Globals.MusicPicker = GUI.HorizontalSlider(new Rect(15, 90, 300, 20), Globals.MusicPicker, 1f, 27f);

                    int roundedValueMB = (int)Math.Round(Globals.MusicPicker);

                    Globals.MusicName = Globals.AudioManager.GetMusicName(roundedValueMB);

                    GUI.Label(new Rect(125f, 70f, 2000000000000f, 33f), string.Format("[{0}]  " + Globals.MusicName, roundedValueMB));

                    if (GUI.Button(new Rect(110f, 120f, 125f, 35f), "Play Music"))
                    {
                        Vector2i currentPlayerMapPoint = Globals.Player.currentPlayerMapPoint;
                        OutgoingMessages.SendChangeWorldMusicMessage(currentPlayerMapPoint, roundedValueMB, PlayerTool.WrenchTool);
                        Globals.AudioManager.PlayMusic(roundedValueMB);
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                    }

                    if (GUI.Button(new Rect(150, 365, 105, 25), "Back?"))
                    {
                        tab = Tab.Troll;
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                    }

                    break;

                case Tab.ClothesBugger: // ClothesBugger Tab


                    if (GUI.Button(new Rect(15, 10, 0.3f, 0.3f), "E"))
                    {
                        BluePopupUI.SetPopupValue(PopupMode.JustClose, "", "Easter Egg Unlocked", "Type: Secret Button ;)<br <br Congratulations on finding this easter egg!<br <br Your Name: " + StaticPlayer.theRealPlayername + "<br <br (This was also sent in console)", "Close", "", null, null, 0, 0, true, false, false);
                        ControllerHelper.rootUI.OnOrOffMenu(Il2CppType.Of<BluePopupUI>());
                        MelonLogger.Msg("Easter Egg Winner: Secret Button - Name: " + StaticPlayer.theRealPlayername);
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.RoulettePrizeFiveStar);
                    }


                    if (GUI.Button(new Rect(15, 55, 90, 30), "Face"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicFace);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicFace);
                    }
                    if (GUI.Button(new Rect(15, 85, 90, 30), "Eyeballs"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicEyeballs);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicEyeballs);
                    }
                    if (GUI.Button(new Rect(15, 115, 90, 30), "Pupils"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicPupil);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicPupil);
                    }
                    if (GUI.Button(new Rect(15, 145, 90, 30), "Eyebrows"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicEyebrows);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicEyebrows);
                    }
                    if (GUI.Button(new Rect(15, 175, 90, 30), "Eyelashes (useless)"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicEyelashes);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicEyelashes);
                    }
                    if (GUI.Button(new Rect(15, 205, 90, 30), "Mouth"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicMouth);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicMouth);
                    }
                    if (GUI.Button(new Rect(15, 235, 90, 30), "Torso"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicTorso);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicTorso);
                    }
                    if (GUI.Button(new Rect(15, 265, 90, 30), "TopArm"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicTopArm);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicTopArm);
                    }
                    if (GUI.Button(new Rect(15, 295, 90, 30), "BottomArm"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicBottomArm);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicBottomArm);
                    }
                    if (GUI.Button(new Rect(115, 55, 90, 30), "Legs"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicLegs);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicLegs);
                    }
                    if (GUI.Button(new Rect(115, 85, 90, 30), "Maskless"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.MaskHoodBlack);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.MaskHoodBlack);
                    }
                    if (GUI.Button(new Rect(115, 115, 90, 30), "Hatless"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.HatHeroHoodBlack);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.HatHeroHoodBlack);
                    }
                    if (GUI.Button(new Rect(115, 145, 90, 30), "Torsoless"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.ShirtGargoyle);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.ShirtGargoyle);
                    }
                    if (GUI.Button(new Rect(115, 175, 90, 30), "Bodyless"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.SuitPWRBlack);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.SuitPWRBlack);
                    }
                    if (GUI.Button(new Rect(115, 205, 90, 30), "Legless"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.PantsGargoyle);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.PantsGargoyle);
                    }
                    if (GUI.Button(new Rect(115, 235, 90, 30), "Legless 2"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.Underwear);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.Underwear);
                    }
                    if (GUI.Button(new Rect(115, 265, 90, 30), "Gloves"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.GlovesPWRBlack);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.GlovesPWRBlack);
                    }
                    if (GUI.Button(new Rect(115, 295, 90, 30), "Back Item"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.JetPackDark);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.JetPackDark);
                    }
                    if (GUI.Button(new Rect(115, 325, 90, 30), "Headless"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.HatHelmetVisorPWRBlack);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.HatHelmetVisorPWRBlack);
                    }
                    if (GUI.Button(new Rect(15, 325, 90, 30), "Weaponless"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.WeaponPickAxe);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.WeaponPickAxe);
                    }

                    Globals.TeamSwitcher = GUI.Toggle(new Rect(15, 365, 115, 25), Globals.TeamSwitcher, "Team Switcher");

                    if (GUI.Button(new Rect(180, 365, 105, 25), "Back?"))
                    {
                        tab = Tab.Troll;
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                    }

                    if (GUI.Button(new Rect(215, 55, 90, 30), "MaskBugger"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.MaskHoodBlack);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.MaskHoodBlack);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicMouth);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicMouth);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicEyebrows);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicEyebrows);

                        OutgoingMessages.PlayerInfoUpdated(Globals.PlayerData.gender, Globals.PlayerData.countryCode, Globals.PlayerData.skinColorIndex, Globals.PlayerData.skinColorIndexBeforeOverride);
                    }
                    if (GUI.Button(new Rect(215, 240, 90, 30), "Reset Set"))
                    {
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.SuitCamouflageSoilblock);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.SuitCamouflageSoilblock);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicLegs);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicLegs);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicTorso);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicTorso);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicFace);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicFace);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicPupil);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicPupil);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicEyeballs);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicEyeballs);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicEyebrows);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicEyebrows);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicBottomArm);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicBottomArm);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicTopArm);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicTopArm);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicMouth);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicMouth);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicEyelashes);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicEyelashes);
                        InfoPopupUI.SetupInfoPopup("Character Reset!", "");
                        InfoPopupUI.ForceShowMenu();
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.BlueParticleAppear);
                    }
                    if (GUI.Button(new Rect(215, 95, 110, 30), "MaskBugger 2"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.SuitCamouflageSoilblock);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.SuitCamouflageSoilblock);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicLegs);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicLegs);
                    }

                    GUI.Label(new Rect(235, 135, 150, 150), "Works on DIM and similar masks");

                    if (GUI.Button(new Rect(235, 175, 120, 25), "Reset Wolf"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponChange(World.BlockType.CostumeWerewolf);
                        Globals.Player.ChangeWearableOrWeaponRemote(World.BlockType.CostumeWerewolf, 0);
                    }
                    if (GUI.Button(new Rect(215, 325, 90, 30), "EPWR Float"))
                    {
                        Globals.AudioManager.PlaySFX(AudioManager.SoundType.ButtonClick);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.SuitPWRBlack);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.SuitPWRBlack);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.Underwear);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.Underwear);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicLegs);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicLegs);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicTopArm);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicTopArm);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicBottomArm);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicBottomArm);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.BasicTorso);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.BasicTorso);
                        OutgoingMessages.SendWearableOrWeaponUndress(World.BlockType.JetPackDark);
                        Globals.Player.UndressWearableOrWeaponRemote(World.BlockType.JetPackDark);
                    }

                    break;
                case Tab.PatchNotes: // old n unused

                    break;
                case Tab.WLPlacing: // WLPlacing Tab

                    break;
                case Tab.Auto:



                    break;
            }

            GUI.DragWindow();
        }

        public Tab2 tab2 = Tab2.Writing;
        public enum Tab2
        {
            Writing,
            Saving,

        }
        private void DrawMenu2(int id)
        {

            switch (tab2)
            {
                case Tab2.Writing:

                    Globals.Notes = GUI.TextArea(new Rect(15, 35, 365, 320), Globals.Notes);

                    if (GUI.Button(new Rect(15, 350, 120, 25), "Options"))
                    {
                        tab2 = Tab2.Saving;
                    }

                    if (GUI.Button(new Rect(135, 350, 120, 25), "Close Notes"))
                    {
                        showNotes = false;
                        Globals.viewNotes = false;
                    }
                    break;

                case Tab2.Saving:

                    if (GUI.Button(new Rect(15, 55, 120, 25), "Save Notes"))
                    {
                        FileStream FileStream2 = File.Open(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\" + Globals.SaveAs + ".json", FileMode.Create);
                        FileStream2.Close();
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\" + Globals.SaveAs + ".json", Globals.Notes);
                    }

                    if (GUI.Button(new Rect(15, 85, 120, 25), "Load Notes"))
                    {
                        string MyNotes = AppDomain.CurrentDomain.BaseDirectory + "AMod Official\\" + Globals.SaveAs + ".json";
                        Globals.Notes = File.ReadAllText(MyNotes);
                    }

                    GUI.Label(new Rect(135, 55, 10000, 10000), "Write the name to save/load file as:");
                    Globals.SaveAs = GUI.TextArea(new Rect(135, 85, 150, 25), Globals.SaveAs);

                    if (GUI.Button(new Rect(15, 115, 90, 25), "Back?"))
                    {
                        tab2 = Tab2.Writing;
                    }
                    break;
            }
            GUI.DragWindow();
        }

        private void DrawMenu3(int id)
        {
            if (GUI.Button(new Rect(15, 15, 115, 20), "Close"))
            {
                statusGUI = false;
            }

            if (GUI.Button(new Rect(15, 55, 115, 20), "Admin Status"))
            {
                Globals.PlayerData.playerAdminStatus = PlayerData.AdminStatus.AdminStatus_Admin;
                InfoPopupUI.SetupInfoPopup("Status updated!", "You are now an admin.");
                InfoPopupUI.ForceShowMenu();
                statusGUI = false;
            }
            if (GUI.Button(new Rect(15, 75, 115, 20), "Mod Status"))
            {
                Globals.PlayerData.playerAdminStatus = PlayerData.AdminStatus.AdminStatus_Moderator;
                InfoPopupUI.SetupInfoPopup("Status updated!", "You are now a mod.");
                InfoPopupUI.ForceShowMenu();
                statusGUI = false;
            }
            if (GUI.Button(new Rect(15, 95, 115, 20), "Creator Status"))
            {
                Globals.PlayerData.playerAdminStatus = PlayerData.AdminStatus.AdminStatus_Influencer;
                InfoPopupUI.SetupInfoPopup("Status updated!", "You are now a creator.");
                InfoPopupUI.ForceShowMenu();
                statusGUI = false;
            }
            if (GUI.Button(new Rect(15, 115, 115, 20), "Regular Status"))
            {
                Globals.PlayerData.playerAdminStatus = PlayerData.AdminStatus.AdminStatus_None;
                InfoPopupUI.SetupInfoPopup("Status updated!", "You are now a regular.");
                InfoPopupUI.ForceShowMenu();
                statusGUI = false;
            }

            GUI.DragWindow();
        }

        public override void OnGUI()
        {
            if (showBox)
            {
                GUI.Box(new Rect(10f, (float)Screen.height - 540, 178f, 240f), "AMod InfoBox | F7 to hide.");
                GUI.Label(new Rect(13f, (float)Screen.height - 530, 178f, 240f), string.Concat(new string[]
                {
                "\n TAB Show/Hide GUI\n[",
                Globals.GetOnOff(Globals.InvisibleHack),
                "]ALT + 1 InvisHack\n",
                Globals.GetOnOff(Globals.Jetpacker),
                "]ALT + 2 JetSpam\n[",
                "ALT + F] Load locked worlds\n[",
                Globals.GetOnOff(Globals.IRef),
                "] Inventory Ref.\nALT + E  Leave World\nALT + G  Reload World\nSHIFT + G  Reload Game",
                string.Format("\n Ping {0}ms", KukouriTime.lag),
                string.Format("\nTime {0}:{1}:{2}", DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, DateTime.Now.TimeOfDay.Seconds),
                }));
            }

            if (showGUI)
            {
                WindowSize = GUI.Window(2008, WindowSize, (GUI.WindowFunction)DrawMenu, "<color=yellow>AMod v2.2</color> | " + string.Format("Ping {0}ms", KukouriTime.lag) + string.Format(" | Time {0}:{1}:{2}", DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, DateTime.Now.TimeOfDay.Seconds)); // DLL Ver mentioned 1                
            }
            if (showNotes)
            {
                WindowSize2 = GUI.Window(1515, WindowSize2, (GUI.WindowFunction)DrawMenu2, "My Notes");
            }
            if (statusGUI)
            {
                WindowSize3 = GUI.Window(1515, WindowSize3, (GUI.WindowFunction)DrawMenu3, "Status Tool");
            }
        }

        private static void StartAutoSolveFossil()
        {
            if (!Globals.solvingFossils)
            {
                return;
            }
            if (!Globals.OutgoingBlock.Contains("MGSp"))
            {
                Globals.OutgoingBlock.Add("MGSp");
            }
            Globals.CustomPacket = "{\"ID\" : \"MGSt\", \"MGT\" : 1, \"IK\" : 117441649}";
            AMod.SendCustomPacket();
        }

        // Token: 0x06000267 RID: 615 RVA: 0x00002C4C File Offset: 0x00000E4C
        private static void StopAutoSolveFossil()
        {
            if (Globals.OutgoingBlock.Contains("MGSp"))
            {
                Globals.OutgoingBlock.Remove("MGSp");
            }
        }
    }
}