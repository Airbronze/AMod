using AMod;
using Boo.Lang.Compiler.TypeSystem;
using DiscordRPC;
using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Amod.DiscordTest
{
    internal static class Discord
    {
        public static void Init()
        {
            initialized = true;
            client = new DiscordRpcClient("1265666235627077736");
            client.Initialize();
            timestamps = Timestamps.Now;
            Update();
            MelonLogger.Msg("Hi AMod!");
        }

        public static void Update()
        {
            try
            {
                client.SetPresence(new RichPresence
                {
                    Details = GetUsername(),
                    State = GetWorldName(),
                    Timestamps = timestamps,
                    Assets = new Assets
                    {
                        LargeImageKey = "airbronzepfp",
                        LargeImageText = "TEST!!",
                        SmallImageKey = "airbronzepfp",
                        SmallImageText = "AMOD RPC TEST"
                    }
                });
            }
            catch
            {
            }
        }

        public static string GetWorldName()
        {
            if (ControllerHelper.networkClient != null)
            {
                PlayerConnectionStatus playerConnectionStatus = ControllerHelper.networkClient.playerConnectionStatus;
                if (playerConnectionStatus == 0)
                {
                    return "Not Connected";
                }
                if (playerConnectionStatus == (PlayerConnectionStatus)8)
                {
                    return "Joining World";
                }
                if (playerConnectionStatus == (PlayerConnectionStatus)7)
                {
                    return "In Menus";
                }
            }

            if (Globals.world != null && NetworkPlayers.otherPlayers != null && Globals.Player != null)
            {
                return showWorld
                    ? string.Format("{0} ({1}/50)", Globals.world.worldName, NetworkPlayers.otherPlayers.Count + 1)
                    : string.Format("In World ({0}/50)", NetworkPlayers.otherPlayers.Count + 1);
            }

            return "In Menus";
        }

        public static string GetUsername()
        {
            return !string.IsNullOrWhiteSpace(StaticPlayer.theRealPlayername)
                ? (showName ? StaticPlayer.theRealPlayername : "Hidden Username")
                : "Unknown Username";
        }

        public static bool initialized = false;
        public static bool showName = false;
        public static bool showWorld = true;
        internal static string firstPlayerName = "Unknown";
        public static DiscordRpcClient client;
        public static Timestamps timestamps = Timestamps.Now;
    }
}