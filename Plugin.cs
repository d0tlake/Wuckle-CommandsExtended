using BepInEx;
using BepInEx.Logging;
using CommandsExtended.Common;
using CommandsExtended.Patches;
using HarmonyLib;
using System;
using UnityEngine;

namespace CommandsExtended;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("shishyando.WK.MoreCommands", "0.13.0")]
public class Plugin : BaseUnityPlugin
{
    public static new ManualLogSource Logger;

    public void Awake()
    {
        Logger = base.Logger;

        this.LoadConfig();

        Harmony harmony = new(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();

        Registry.RegisterAll();

        this.OnApplicationFocus(true);

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} is loaded");
    }

    public static void Assert(bool condition)
    {
        if (!condition)
        {
            Logger.LogFatal($"Assert failed");
            throw new Exception($"[CommandsExtended] Assert failed");
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && ResolutionPatch.Enabled != null && ResolutionPatch.Enabled.Value)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != null)
            {
                Global.RefreshAllSettings();
            }
        }
    }

    private void LoadConfig()
    {
        ResolutionPatch.Enabled = Config.Bind("FullscreenExclusive", "Enabled", false, "Is exclusive fullscreen override active");
        ResolutionPatch.CustomResActive = Config.Bind("FullscreenExclusive", "CustomResActive", false, "Define custom resolution");
        ResolutionPatch.Width = Config.Bind("FullscreenExclusive", "Width", 1920, "Resolution width");
        ResolutionPatch.Height = Config.Bind("FullscreenExclusive", "Height", 1080, "Resolution height");

        CrosshairPatch.Enabled = Config.Bind("CustomCrosshair", "Enabled", false, "Is custom crosshair enabled");
        CrosshairPatch.CrosshairColor = Config.Bind("CustomCrosshair", "Color", Color.green, "Crosshair color and transparency");
        CrosshairPatch.InvertBehind = Config.Bind("CustomCrosshair", "InvertBehind", false, "Is image behind crosshair inverted");
        CrosshairPatch.Length = Config.Bind("CustomCrosshair", "Length", 25f, "Length of crosshair lines");
        CrosshairPatch.Thickness = Config.Bind("CustomCrosshair", "Thickness", 25f, "Thickness of crosshair lines");
        CrosshairPatch.Gap = Config.Bind("CustomCrosshair", "Gap", 25f, "Gap distance of crosshair lines from center");
        CrosshairPatch.DotScale = Config.Bind("CustomCrosshair", "DotScale", 25f, "Scale of the center crosshair dot");
    }
}
