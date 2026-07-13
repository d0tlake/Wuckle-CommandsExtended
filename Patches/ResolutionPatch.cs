using System;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace CommandsExtended.Patches
{
    [HarmonyPatch(typeof(Screen))]
    public static class ResolutionPatch
    {
        public static ConfigEntry<bool> Enabled;
        public static ConfigEntry<bool> CustomResActive;
        public static ConfigEntry<int> Width;
        public static ConfigEntry<int> Height;

        [HarmonyPatch(nameof(Screen.SetResolution), [typeof(int), typeof(int), typeof(FullScreenMode), typeof(RefreshRate)])]
        [HarmonyPrefix]
        public static void Prefix(ref int width, ref int height, ref FullScreenMode fullscreenMode)
        {
            if (Enabled == null || !Enabled.Value)
            {
                return;
            }

            fullscreenMode = FullScreenMode.ExclusiveFullScreen;

            if (CustomResActive != null && CustomResActive.Value)
            {
                width = Width.Value;
                height = Height.Value;
            }
        }
    }
}