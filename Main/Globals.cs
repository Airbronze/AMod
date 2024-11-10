using Harmony;
using Il2Cpp;
using Il2CppBasicTypes;
using Il2CppInterop.Runtime;
using Il2CppKernys.Bson;
using Il2CppTMPro;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Il2CppAmazon.Runtime.Internal.Util.InternalLog4netLogger;

namespace AMod
{
    class Globals
    {
        public static WorldController WorldController
        {
            get
            {
                try
                {
                    return ControllerHelper.worldController;
                }
                catch
                {
                    return null;
                }
            }
        }
        public static AudioManager AudioManager
        {
            get
            {
                try
                {
                    return ControllerHelper.audioManager;
                }
                catch
                {
                    return null;
                }
            }
        }
        public static Player Player
        {
            get
            {
                try
                {
                    return Globals.WorldController.player;
                }
                catch
                {
                    return null;
                }

            }
        }
        public static PlayerData PlayerData
        {
            get
            {
                try
                {
                    return Globals.Player.myPlayerData;
                }
                catch
                {
                    return null;
                }

            }
        }
        public static World world
        {
            get
            {
                try
                {
                    return Globals.WorldController.world;
                }
                catch
                {
                    return null;
                }

            }
        }
        public static RootUI rootUI
        {
            get
            {
                try
                {
                    return Globals.rootUI;
                }
                catch
                {
                    return null;
                }

            }
        }
        public static ChatUI chatUI
        {
            get
            {
                ChatUI result;
                try
                {
                    result = ControllerHelper.chatUI;
                }
                catch
                {
                    result = null;
                }
                return result;
            }
        }

        public static GameplayUI GameplayUI
        {
            get
            {
                GameplayUI result;
                try
                {
                    result = ControllerHelper.gameplayUI;
                }
                catch
                {
                    result = null;
                }
                return result;
            }
        }

        public static Vector2i CurrentMapPoint
        {
            get
            {
                Vector2i result;
                try
                {
                    result = Globals.Player.currentPlayerMapPoint;
                }
                catch
                {
                    result = new Vector2i(-1, -1);
                }
                return result;
            }
        }
        public static NotificationController notificationController
        {
            get
            {
                NotificationController result;
                try
                {
                    result = ControllerHelper.notificationController;
                }
                catch
                {
                    result = null;
                }
                return result;
            }
        }
        public static NetworkClient NetworkClient
        {
            get
            {
                try
                {
                    return ControllerHelper.networkClient;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static GameplayUI gameplayUI
        {
            get
            {
                GameplayUI result;
                try
                {
                    result = ControllerHelper.gameplayUI;
                }
                catch
                {
                    result = null;
                }
                return result;
            }
        }

        public static void DoCustomNotification(string text, Vector2i mapPoint)
        {
            bool mapPoint1 = mapPoint.y >= ControllerHelper.worldController.world.worldSizeY;
            if (mapPoint1)
            {
                mapPoint.y = ControllerHelper.worldController.world.worldSizeY - 1;
            }
            bool notifIndex = Globals.notificationController.notifications[Globals.notificationController.notificationsIndex] == null;
            if (notifIndex)
            {
                Globals.notificationController.notifications[Globals.notificationController.notificationsIndex] = UnityEngine.Object.Instantiate<GameObject>(Globals.notificationController.notificationPrefab, ControllerHelper.worldController.ConvertMapPointToWorldPoint(mapPoint), Globals.notificationController.notificationPrefab.transform.rotation);
                Globals.notificationController.notificationTextMeshPros[Globals.notificationController.notificationsIndex] = Globals.notificationController.notifications[Globals.notificationController.notificationsIndex].GetComponent<TextMeshPro>();
                Globals.notificationController.notificationDestroyTextAnimation[Globals.notificationController.notificationsIndex] = Globals.notificationController.notifications[Globals.notificationController.notificationsIndex].GetComponent<DestroyTextAnimation>();
            }
            Globals.notificationController.notifications[Globals.notificationController.notificationsIndex].transform.position = ControllerHelper.worldController.ConvertMapPointToWorldPoint(mapPoint);
            Globals.notificationController.notificationTextMeshPros[Globals.notificationController.notificationsIndex].text = text;
            Globals.notificationController.notificationDestroyTextAnimation[Globals.notificationController.notificationsIndex].StartAnimation();
            Globals.notificationController.notifications[Globals.notificationController.notificationsIndex].SetActive(true);
            NotificationController notificationController = Globals.notificationController;
            int notificationsIndex = notificationController.notificationsIndex;
            notificationController.notificationsIndex = notificationsIndex + 1;
            bool notifIndex2 = Globals.notificationController.notificationsIndex >= Globals.notificationController.notifications.Length;
            if (notifIndex2)
            {
                Globals.notificationController.notificationsIndex = 0;
            }
        }

        public static bool GodMode = false;
        public static bool FlyHack = false;
        public static bool AntiVortex = false;
        public static bool AntiPush = false;
        public static bool BlockKill = false;
        public static bool InstantRes = false;
        public static bool AntiCollect = false;
        public static bool AntiBounce = false;
        public static bool Test = false;
        public static bool WLPlacer = false;
        public static bool WLPlacerAbove = false;
        public static bool WLPlacerBelow = false;
        public static bool WLPlacerLeft = false;
        public static bool WLPlacerRight = false;
        public static bool TeamSwitcher = false;
        public static bool InvisibleHack = false;
        public static bool PotionCrafter = false;
        public static bool SpikeBomber = false;
        public static bool Jetpacker = false;
        public static bool LizoEffect = false;
        public static bool ProDono = false;
        public static bool PotionCrafterXHealPots = false;
        public static bool InfiniteText = true;
        public static bool Musically = false; // Unused (old)
        public static int AnimationCount;
        public static bool AutoTittyrial = false;
        public static bool AutoTittyrial2 = false;
        public static bool AutoTittyrial3 = false;
        public static bool AutoTittyrial4 = false;
        public static bool AutoTittyrial5 = false;
        public static bool SwordPuller = true;
        public static Vector2i lastpos = new Vector2i(0, 0);
        public static string AccountName = "";
        public static string AccountPassy = "";
        public static string LastLoginString1 = "";
        public static string LastLoginString2 = "";
        public static bool AirbronzeButton = false;
        public static bool AirbronzeName = false;
        public static float GemPoucher1 = 3364f;
        public static bool GemPoucher2 = false;
        public static float MusicPicker = 1f;
        public static string MusicName = "";
        public static int WorldPage = 1;
        public static bool CardPack1 = false;
        public static bool Troll = false;
        public static bool handleKick = false;
        public static float PotionID = 2303;
        public static string PotionName = "";
        public static bool VisualName = false;
        public static bool AnimOnner = false;
        public static bool AnimOffer = false;
        public static bool Gambler = false;
        public static bool Recycler = false;
        public static int RecycledItem = 0;
        public static string WorldNameSecret = "AIRBRONZE";
        public static string WorldNameSecret2 = "JEPE";
        public static string WorldNameRiddle1 = "RIDDLE";
        public static string WorldNameRiddle2 = "SUN";
        public static string WorldNameRiddle3 = "WINTER";
        public static string WorldNameRiddle4 = "JAKE";
        public static string WorldNameRiddle5 = "EARTH";
        public static bool Trasher = false;
        public static bool Dropper = false;
        public static bool LeaveOnDetect = true;
        public static bool HardDetect = false;
        public static bool LogAllPlayers = false;
        public static bool AntiRetard = false;
        public static bool viewNotes = false;
        public static string Notes = "Write your notes here!";
        public static string SaveAs = "MyNotes";
        public static bool CaptureIncomingID = false;
        public static bool CaptureOutgoingID = false;
        public static bool ViewOcaptureOrICapture = false;
        public static string CurrentCaptureINCOMING = "";
        public static string CurrentCaptureOUTGOING = "";
        public static string CustomPacket = "";
        public static string CPacketFile = "";
        public static string LocateGM = "";
        public static bool GWarper = false;
        public static bool PlaceSeedOnAir = false;
        public static bool CollectOn = true;
        public static string INK = "";
        public static string INKKey = "";
        public static string INKAMT = "";
        public static bool GemGemGem = false;
        public static bool BytesBuyer = false;
        public static bool BytesBuyer2 = false;
        public static bool AutoMath = false;
        public static string GIgn = "";
        public static bool ItemID = false;
        public static int ID1 = 0;
        public static int ID2 = 0;
        public static bool shouldRem = false;
        public static float BValueB = 1f;
        public static string JustIdk123 = "";
        public static bool GemmerOG = false;
        public static bool PlaceholderBool = false;
        public static bool Custompack = true;
        public static int Timer = 1;
        public static bool startTime = false;
        public static bool Disencht = false;
        public static int cardID = 0;
        public static int cardAmt = 0;
        public static bool captureCard = false;
        public static bool autoVIP = false;
        public static bool autoVIP2 = false;
        public static bool clearCh = false;
        public static bool PLCv = false;
        public static bool PLCv2 = false;
        public static bool ByteGemmer = false;
        public static bool ignoreFwk = false;
        public static int VendID;
        public static int VendCID;
        public static int VendIK;
        public static bool AutoVendor = false;
        public static int EndAmtVend = 0;
        public static float EndAmtVend2 = 100f;
        public static int vendIKAMT = 0;
        public static bool autoOPENER = false;
        public static bool GetOpen = false;
        public static int AOINVK = 0;
        public static bool AutoGear = false;
        public static bool AutoBP = false;
        public static bool NiceTry = false;
        public static bool collectAll = false;
        public static float CTimer = 0.5f;
        public static bool viewGMCode = false;
        public static bool aFossil = false;
        public static bool KFwk = false;
        public static int num2;
        public static bool refOnCollect = false;
        public static string worldIP = "";
        public static GUIStyle IBStyle = new GUIStyle
        {
            alignment = TextAnchor.UpperLeft,
            fontSize = 16,
            fontStyle = FontStyle.Bold,
        };

        public static string GetOnOff(bool value)
        {
            return value ? "ON" : "OFF";
        }

        /*
        public static bool TT = false;
        public static bool TT2 = false;
        public static bool TT3 = false;
        public static bool TT4 = false;
        public static bool TT5 = false;
        public static bool TT6 = false;
        public static bool TT7 = false;
        public static bool TT8 = false;
        public static bool TT9 = false;
        public static bool TT10 = false;
        public static bool TT11 = false;
        public static bool TT12 = false;
        public static bool TT13 = false;
        public static bool TT14 = false;
        public static bool TT15 = false;
        public static bool TT16 = false;
        public static bool TT17 = false;
        public static bool TT18 = false;
        public static bool TT19 = false;
        public static bool TT20 = false;
        */
        public static Dictionary<string, string> Commands = new Dictionary<string, string>()
        {
             {"info", "Dawg?"},
            {"credits", "Displays credits of people who helped make AMod what it is today."},
            {"help", "Displays a list of all commands."},
            {"ahelp", "Displays a list of all commands. AMod-Exclusive command so it does not interfere with other clients."},
            {"keys", "Displays hotkeys and what they do."},
            {"love", "Does the pet love emote."},
            {"pet1", "Does the pet cleaning emote."},
            {"pet2", "Does the pet training emote."},
            {"sleep", "Activate sleep animation."},
            {"wake", "De-activate sleep animation."},
            {"quit", "Close your game."},
            {"pwe", "Place a PWE on your mapPoint."},
            {"support", "Visit our support world!"},
            {"gbt", "Copy itemID of the current selected item."},
            {"rsc", "Reset your clipboard."},
            {"ait", "Add an item inside a regular chest you are standing on. Buggy command."},
            {"rit", "Remove an item from a chest. Best to use if you have an untradable stuck inside chest. Stand on chest to use."},
            {"uait", "Select an untradable item in your inventory, stand on a swapped BT chest, and type /uait to add the item inside."},
            {"urit", "Originally planned to remove untradeables from chest, but pretty buggy and useless."},
            {"sbt", "Swap BlockType of a chest, from normal to untradable chest, or the other way around. \r\n WARNING: If you leave the world with the chest as untradable, you will lose the items inside!"},
            {"cdata", "Just copy paste the data of the current selected item."},
            {"ref", "Refreshes your inventory if you collected a bugged duped seed."},
            {"aref", "Auto-Refreshes your inventory if you collected a bugged duped seed. Can be toggled on/off with the command."},
            {"drop", "Click an item in your inventory and use /drop amount to drop a custom amount of the item!"},
            {"dall", "Drop all of the current selected item!"},
            {"d1", "Drop only 1 of the current selected item!"},
            {"dupe", "Creates a duped seed of the current selected item. Only works on seeds, consumables, & familiars."},
            {"cdupe", "Creates a custom amount duped seed of the current selected item. Only works on seeds consumables & familiars."},
            {"poblock", "Block a specific outgoing packet ID from being sent or captured. /poblock packetID"},
            {"piblock", "Block a specific incoming packet ID from being received or captured. /piblock packetID"},
            {"iclear", "Clear the list of blocked incoming packets."},
            {"oclear", "Clear the list of blocked outgoing packets"},
            {"eject", "Removes AMod from your game until next game session!"},
            {"craft", "Set the itemID for CraftBypasser." },
            {"set", "Set custom values of game components. Dev-Only feature, very buggy and unfinished." },
        };
        public static List<string> IgnoreGMW = new List<string>();

        public static Dictionary<string, string> PWStaff = new Dictionary<string, string>()
{
    // PW Mods
    {"DY4LVBNE", "|BlackWight|"},
    {"1BYM5371", "Rabia"},
    {"VSL1HVDO", "Citrina"},
    {"LKB469T7", "Starfire1178"},
    {"95F6JEWA", "ionas"},
    {"I501W0UX", "xSHANEx"},
    {"60FPOJ55", "Invalid"},
    {"VZ7RALO", "ColdUnwanted"},
    {"LNBJ9SK", "Quqqo"},
    {"IRIME6M", "Lupuss"},
    {"SAZUT30", "Freak"},
    {"OMD5ECO", "Hinter"},
    {"2SYJQ2R", "SEAF"},
    {"5V6MYXQ3", "NicoKapell"},
    {"G2D8FGE", "MrGrandman"},
    {"48WESIAE", "|Serxan|"},
    {"WUAOAQ4T", "Luucsas"},
    {"MXH9XJ3I", "MrBeast"},
    {"LSPMU8CV", "[MOD_CADET]"},
    {"B7D7Q1YI", "Kaluub" },
    {"VAVR096E", "Decay" },
    {"EIOFU41X", "Miwsky" },
    {"HDAEYVAA", "RetNos" },
    {"FBNA4ZK", "Sign" },
    {"FT066B8B", "zithii" },
    {"Y35ID055", "AP0KALYPSE"},
    {"HKD2ARL4", "Baroness"},
    {"EIR62J9A", "lemz"},
    {"RRP2BPSP", "BubblySky"},
    {"WU73CSFL", "Bergelmir" },
    {"14MCSJHG", "JennyFei" },
    

    // PW Admins
    {"34N8P51", "Jake"},
    {"HN55GSS", "EndlesS"},
    {"74RL1AE", "MidnightWalker"},
    {"8HN45WF", "Dev"},
    {"F2RQK1W", "Commander_K"},
    {"666", "Commander_K/Server"},
    {"NZRV2SD", "Sorsa" },
    {"FAL8MX5Y", "Siskea" },
    {"1YAFK4YS", "Lokalapsi" },
    {"Q38SV2L8", "bbricks" },
    {"HF3VQSK5", "PIXELLOX" },
};

        public static Dictionary<string, string> PacketsToIgnore = new Dictionary<string, string>()
        {
            { "mP", "" },
            {"PSicU", "" },
            {"PPA", "" }
        };

        public static Dictionary<string, string> Retards = new Dictionary<string, string>()
        {
            {"G2OK9IVV", "ImAGoddess" }, // Charon Alt
            {"IJ0CLEM3", "JokingWomen" }, // Charon Alt
        };

        public static List<string> oCapture = new List<string>();

        public static List<string> iCapture = new List<string>();

        public static List<string> OutgoingFullCapture = new List<string>();

        public static List<string> OutgoingBlock = new List<string>();

        public static List<string> IncomingBlock = new List<string>();

        public static bool cmIncoming = false;

        public static bool cmOutgoing = false;


        // Botting
        public static string SpamTextOfChoice = "Hello, World!";
        public static bool SpamTexter = false;
        public static bool JoinRandomWorlds = false;
        public static bool EmoteSpam = false;
        public static bool FriendReqSpam = false;
        public static bool TradeSpam = false;
        public static float SendMsgTime = 5f;
        public static float WorldTime = 10f;
        public static bool MannequinSpam = false;

        // Admin Shitaroonies
        public static bool MuteGMs = true;
        public static string ignoreID = "";
        public static string GNameID = "";
        public static string IDLookup1 = "";
        public static float AutoBuySlct = 1f;
        public static string SetABNick = "";
        public static bool CaptureAB = false;
        public static string AutoBuyCT2 = "";
        public static float AutoBuyCT = 1f;
        public static string IPID1 = "";
        public static bool AutoBuy1 = false;
        public static string PTimerP3 = "";
        public static float PTimerP2 = 1f;
        public static bool PSpamP = false;
        public static int InventoryKeyCP;
        public static string CAmtSpd = "";
        public static float GMSpeed = 0.5f;
        public static bool StartGM = false;
        public static bool autoGM = false;
        public static string Base64Msg = "";
        public static string SignText12 = "";
        public static bool PickyTrasher = false;
        public static bool seeFwk = false;
        public static bool Fwker = false;
        public static bool IRef = false;
        public static string TWorld;
        public static string NameVis;
        public static float Speed = 1f;
        public static List<PNode> TeleportPath = new List<PNode>();
        public static int CurrentTp = 0;
        public static bool Isteleporting = false;
        public static float TeleportTimer = 0f;
        public static int targetIndex;
        public static Vector3 targetPosition;
        public static float teleportTimer = 0f;
        public static float teleportSpeed = 100f;
        public static float teleportInterval = 0f;
        public static bool ACBPass = false;
        public static bool PCrafter = false;
        public static bool FCrafter = false;
        public static int CraftitemID = 0;
        public static bool playMusic = false;
        public static bool RSect = false;
        public static int Niggerr = 1;
        public static List<string> AutoBanPackets = new List<string>
{
    "SetGoThroughDoorsByAdmin",
    "SetEditWorldByAdmin ",
    "LockWorld",
    "UnlockWorld",
    "SetAdminWantsToBeSummoned",
    "SetAdminWantsToGoGhostMode",
    "SetAdminWantsToGoUndercoverMode",
    "SetAdminWantsToIgnorePortals",
    "SetAdminWantsToShowKickBanInfo",
    "SetAdminWantsToGoNoobMode",
    "QueryReportsByAdmin",
    "QueryPlayerLocationByAdmin",
    "MarkReportAsResolvedByAdmin",
    "WarnPlayer",
    "BanPlayerFromGame",
    "AdminKillPlayer",
    "SetGoThroughDoorsByMod",
    "AdminTeleportMenuOpen",
    "AdminTeleportedTo",
    "MGMbP",
    "MFMbP",
    "BanPlayerFromGameByHammer"
};

        public static List<string> Classes = new List<string>
{
    "GiftBoxData",
    "NetherGiftBoxData",
};


        public static bool ignoreAutoban = false;

        public static void StartTeleportation2(int currentX, int currentY, int targetX, int targetY)
        {
            Globals.Player.gravity = 9.17f;

            Vector2i currentPlayerMapPoint = new Vector2i(currentX, currentY);
            Vector2i targetMapPoint = new Vector2i(targetX, targetY);

            pathfinder pathFinder = new pathfinder();
            pathFinder.Run(currentPlayerMapPoint, targetMapPoint);
        }


        public static void StartTeleportation(int currentX, int currentY, int targetX, int targetY)
        {
            if (Globals.Isteleporting) return;
            Globals.Player.gravity = 9.17f;

            MelonLogger.Msg($"Start teleportation from ({currentX}, {currentY}) to ({targetX}, {targetY})");

            Vector2i currentPlayerMapPoint = new Vector2i(currentX, currentY);
            Vector2i targetMapPoint = new Vector2i(targetX, targetY);

            pathfinder pathFinder = new pathfinder();
            pathFinder.Run(currentPlayerMapPoint, targetMapPoint);

            if (Globals.TeleportPath.Count > 0)
            {
                Globals.CurrentTp = 0;
                Globals.targetIndex = 0;
                Globals.targetPosition = Utils.ConvertMapPointToWorldPoint(new Vector2i(Globals.TeleportPath[Globals.targetIndex].x, Globals.TeleportPath[Globals.targetIndex].y));
                Globals.Isteleporting = true;
            }
            else
            {
                PathfindingResult result = pathFinder.Result;
                MelonLogger.Msg("No path found for teleportation.");
                string errorReason = GetPathfindingErrorReason(result);
                MelonLogger.Msg($"Pathfinding error: {errorReason}");
            }
        }

        public static void ProcessTeleportation(int currentX, int currentY, int targetX, int targetY)
        {
            if (!Globals.Isteleporting) return;

            if (Globals.CurrentTp >= Globals.TeleportPath.Count)
            {
                FinalizeTeleportation(null);
                return;
            }

            try
            {
                Vector3 currentPosition = Globals.Player.myTransform.position;
                Vector3 moveDirection = Globals.targetPosition - currentPosition;
                float distanceToTarget = moveDirection.magnitude;

                float captureFramerate = Time.captureFramerate > 0 ? Time.captureFramerate : 60;
                float adjustedTeleportSpeed = Globals.teleportSpeed * (captureFramerate / 60);

                if (distanceToTarget < adjustedTeleportSpeed * Time.deltaTime)
                {
                    Globals.Player.myTransform.position = Globals.targetPosition;
                    Globals.CurrentTp++;

                    if (Globals.CurrentTp >= Globals.TeleportPath.Count)
                    {
                        FinalizeTeleportation(Globals.TeleportPath[Globals.CurrentTp - -1]);
                    }
                    else
                    {
                        Globals.targetIndex = Globals.CurrentTp;
                        Globals.targetPosition = Utils.ConvertMapPointToWorldPoint(new Vector2i(Globals.TeleportPath[Globals.targetIndex].x, Globals.TeleportPath[Globals.targetIndex].y));
                    }
                }
                else
                {
                    Globals.Player.myTransform.position += moveDirection.normalized * adjustedTeleportSpeed * Time.deltaTime;
                }
            }
            catch (Exception ex)
            {
                HandleTeleportationError(ex);
            }
        }

        private static void FinalizeTeleportation(PNode pnode)
        {
            Globals.TeleportPath.Clear();
            Globals.CurrentTp = 0;
            Globals.Isteleporting = false;


            if (pnode != null)
            {
                Vector3 finalPosition = Utils.ConvertMapPointToWorldPoint(new Vector2i(pnode.x, pnode.y));
                finalPosition += new Vector3(0.1f, 0.2f, 0f);
                Globals.Player.myTransform.position = finalPosition;
            }
        }

        private static void HandleTeleportationError(Exception ex)
        {
            MelonLogger.Msg($"Teleport failed! Error: {ex.Message} | StackTrace: {ex.StackTrace}");
            Globals.TeleportPath.Clear();
            Globals.CurrentTp = 0;
            Globals.Isteleporting = false;
        }

        public static string GetPathfindingErrorReason(PathfindingResult result)
        {
            switch (result)
            {
                case PathfindingResult.ERROR_START_OUT_OF_BOUNDS:
                    return "Start position is out of bounds.";
                case PathfindingResult.ERROR_END_OUT_OF_BOUNDS:
                    return "End position is out of bounds.";
                case PathfindingResult.Same_Block:
                    return "Start and end positions are the same.";
                case PathfindingResult.ERROR_PATH_TOO_LONG:
                    return "The path is too long.";
                case PathfindingResult.Start_Not_Valid:
                    return "Start position is not valid.";
                case PathfindingResult.Invalid_Ending_Pos:
                    return "Ending position is invalid.";
                case PathfindingResult.Path_Not_Found:
                    return "No path could be found.";
                case PathfindingResult.Path_Not_Found_Block:
                    return "End position is blocked by a block.";
                default:
                    return "Unknown error.";
            }
        }

        public static void WarpPortal(int targetX, int targetY)
        {
            try
            {
                if (targetX < 0 || targetY < 0 || targetX >= Globals.world.worldSizeX || targetY >= Globals.world.worldSizeY)
                {
                    ChatUI.SendLogMessage("Error: Map point out of bounds.");
                    return;
                }

                Vector2i mapPoint = new Vector2i(targetX, targetY);
                if (!Globals.world.DoesPlayerHaveRightToModifyItemData(mapPoint, Globals.PlayerData, false))
                {
                    ChatUI.SendLogMessage("Error: No rights to teleport to the specified location.");
                    return;
                }

                Vector2i currentPoint = new Vector2i(
                (int)Globals.Player.myTransform.position.x,
                (int)Globals.Player.myTransform.position.z
            );

                Vector2i targetPointX = new Vector2i(targetX, currentPoint.y);
                Vector2i targetPointY = new Vector2i(currentPoint.x, targetY);

                Vector2i worldPoint = Globals.WorldController.ConvertWorldPointToMapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                Vector3 targetPosition = new Vector3(worldPoint.x, worldPoint.y, 0);

                // Move the player to the target position
                Globals.Player.myTransform.position = targetPosition;

                Console.WriteLine($"Warping to map point: {targetPosition}");

                ControllerHelper.audioManager.PlaySFX((AudioManager.SoundType)241, 0f, -1);

                ShowPopupMessage("idk123", "idk123 not made by airbronze");
            }
            catch (Exception ex)
            {
                HandleTeleportationError(ex);
            }
        }


        public static void ShowPopupMessage(string title, string message)
        {
            InfoPopupUI.SetupInfoPopup(title, message);
            InfoPopupUI.ForceShowMenu();
        }


        internal static List<Vector2i> mps = new List<Vector2i>();
        public static Task Auto1 = new Task(() =>
        {
            Vector2i cpMp = Player.currentPlayerMapPoint;

            Auto1.Wait(2000);

            OutgoingMessages.SendCharacterCreatedMessage(PlayerData.Gender.Male, 6, 8);

            Auto1.Wait(1000);

            OutgoingMessages.SendWearableOrWeaponChange(World.BlockType.HatJumpsuitMale);

            Auto1.Wait(500);

            OutgoingMessages.SendWearableOrWeaponChange(World.BlockType.JumpsuitMale);

            Auto1.Wait(2000);

            StartTeleportation2(Player.currentPlayerMapPoint.x, Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + 5, Player.currentPlayerMapPoint.y);

            if (Globals.Isteleporting)
            {
                Globals.teleportTimer += Time.deltaTime;
                if (Globals.teleportTimer >= Globals.teleportInterval)
                {
                    Globals.ProcessTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + 5, Player.currentPlayerMapPoint.y);
                    Globals.TeleportTimer = 0f;
                }
            }

            Auto1.Wait(2000);

            StartTeleportation2(Player.currentPlayerMapPoint.x, Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + 1, Player.currentPlayerMapPoint.y + 1);

            if (Globals.Isteleporting)
            {
                Globals.teleportTimer += Time.deltaTime;
                if (Globals.teleportTimer >= Globals.teleportInterval)
                {
                    Globals.ProcessTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + 1, Player.currentPlayerMapPoint.y + 1);
                    Globals.TeleportTimer = 0f;
                }
            }
            Auto1.Wait(2000);

            StartTeleportation2(Player.currentPlayerMapPoint.x, Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + 1, Player.currentPlayerMapPoint.y);

            if (Globals.Isteleporting)
            {
                Globals.teleportTimer += Time.deltaTime;
                if (Globals.teleportTimer >= Globals.teleportInterval)
                {
                    Globals.ProcessTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + 1, Player.currentPlayerMapPoint.y);
                    Globals.TeleportTimer = 0f;
                }
            }
            Auto1.Wait(2000);

            OutgoingMessages.SendGoFromPortalMessage(Player.currentPlayerMapPoint);
            OutgoingMessages.SendPlayerActivateInPortal(Player.currentPlayerMapPoint);
            OutgoingMessages.SendPlayerActivateOutPortal(Player.currentPlayerMapPoint);

            Auto1.Wait(2000);

            OutgoingMessages.SendSetBlockMessage(Player.currentPlayerRightMapPoint, World.BlockType.GemSoil);

            Auto1.Wait(2000);

            StartTeleportation2(Player.currentPlayerMapPoint.x, Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + -1, Player.currentPlayerMapPoint.y);

            if (Globals.Isteleporting)
            {
                Globals.teleportTimer += Time.deltaTime;
                if (Globals.teleportTimer >= Globals.teleportInterval)
                {
                    Globals.ProcessTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + -1, Player.currentPlayerMapPoint.y);
                    Globals.TeleportTimer = 0f;
                }
            }

            Auto1.Wait(500);

            OutgoingMessages.SendSetBlockMessage(Player.currentPlayerAboveMapPoint, World.BlockType.GemSoil);

            Auto1.Wait(500);

            StartTeleportation2(Player.currentPlayerMapPoint.x, Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + -1, Player.currentPlayerMapPoint.y);

            if (Globals.Isteleporting)
            {
                Globals.teleportTimer += Time.deltaTime;
                if (Globals.teleportTimer >= Globals.teleportInterval)
                {
                    Globals.ProcessTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + -1, Player.currentPlayerMapPoint.y);
                    Globals.TeleportTimer = 0f;
                }
            }

            Auto1.Wait(500);

            OutgoingMessages.SendSetBlockMessage(Player.currentPlayerAboveMapPoint, World.BlockType.GemSoil);

            Auto1.Wait(500);

            StartTeleportation2(Player.currentPlayerMapPoint.x, Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + -1, Player.currentPlayerMapPoint.y);

            if (Globals.Isteleporting)
            {
                Globals.teleportTimer += Time.deltaTime;
                if (Globals.teleportTimer >= Globals.teleportInterval)
                {
                    Globals.ProcessTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + -1, Player.currentPlayerMapPoint.y);
                    Globals.TeleportTimer = 0f;
                }
            }

            Auto1.Wait(500);

            OutgoingMessages.SendSetBlockMessage(Player.currentPlayerAboveMapPoint, World.BlockType.GemSoil);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            StartTeleportation2(Player.currentPlayerMapPoint.x, Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + +1, Player.currentPlayerMapPoint.y);

            if (Globals.Isteleporting)
            {
                Globals.teleportTimer += Time.deltaTime;
                if (Globals.teleportTimer >= Globals.teleportInterval)
                {
                    Globals.ProcessTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + +1, Player.currentPlayerMapPoint.y);
                    Globals.TeleportTimer = 0f;
                }
            }

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            StartTeleportation2(Player.currentPlayerMapPoint.x, Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + +1, Player.currentPlayerMapPoint.y);

            if (Globals.Isteleporting)
            {
                Globals.teleportTimer += Time.deltaTime;
                if (Globals.teleportTimer >= Globals.teleportInterval)
                {
                    Globals.ProcessTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + +1, Player.currentPlayerMapPoint.y);
                    Globals.TeleportTimer = 0f;
                }
            }

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerAboveMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            StartTeleportation2(Player.currentPlayerMapPoint.x, Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + +1, Player.currentPlayerMapPoint.y);

            if (Globals.Isteleporting)
            {
                Globals.teleportTimer += Time.deltaTime;
                if (Globals.teleportTimer >= Globals.teleportInterval)
                {
                    Globals.ProcessTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + +1, Player.currentPlayerMapPoint.y);
                    Globals.TeleportTimer = 0f;
                }
            }

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerRightMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerRightMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(500);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerRightMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(1000);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerRightMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(1000);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerRightMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(1000);

            OutgoingMessages.SendSetSeedMessage(Player.currentPlayerLeftMapPoint, World.BlockType.GemSoil);

            Auto1.Wait(1000);

            OutgoingMessages.SendSetSeedMessage(Player.currentPlayerLeftMapPoint, World.BlockType.Fertilizer);

            Auto1.Wait(1000);

            OutgoingMessages.SendHitBlockMessage(Player.currentPlayerLeftMapPoint, Il2CppSystem.DateTime.Now, false);

            Auto1.Wait(1000);

            Globals.CollectOn = true;

            Auto1.Wait(1000);

            StartTeleportation2(Player.currentPlayerMapPoint.x, Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + -2, Player.currentPlayerMapPoint.y);

            if (Globals.Isteleporting)
            {
                Globals.teleportTimer += Time.deltaTime;
                if (Globals.teleportTimer >= Globals.teleportInterval)
                {
                    Globals.ProcessTeleportation(Globals.Player.currentPlayerMapPoint.x, Globals.Player.currentPlayerMapPoint.y, Player.currentPlayerMapPoint.x + -2, Player.currentPlayerMapPoint.y);
                    Globals.TeleportTimer = 0f;
                }
            }

            Auto1.Wait(1000);

            Auto1.Wait(1000);

            OutgoingMessages.BuyItemPack("BasicClothes");

            Auto1.Wait(1000);

            OutgoingMessages.LeaveWorld();

            Auto1.Wait(4000);

            SceneLoader.ReloadGame();

        });

        // Private Servers
        public static string InventoryKeyIT = "";
        public static bool PWEBuyerAuto = false;
        public static bool CustomItemDrop = false;
        public static bool RandomCDrop = false;
        public static int DropAmtRandomized = 1;
        public static bool Nuker = false;
        public static bool ForcePlace = false;
        public static string InventoryKeyBT = "";
        public static bool CustomSeeder = false;
        public static int BValue = 0;
        public static bool Wearer = false;
        public static bool Bbreak = false;
        public static string BFValue = "";
        public static bool BFarmer = false;
        internal static bool solvingFossils = false;
        // Token: 0x0400025B RID: 603
        public static System.Collections.Generic.List<int> solvingSteps;
    }
}