using BepInEx;
using BepInEx.Logging;
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
        if (hasFocus && ResolutionPatch.OverrideEnabled != null && ResolutionPatch.OverrideEnabled.Value)
        {
            SettingsManager.RefreshSettings();
        }
    }

    private void LoadConfig()
    {
        ResolutionPatch.OverrideEnabled = Config.Bind("ExclusiveModeOverride", "OverrideEnabled", false, "Is exclusive fullscreen override active");
        ResolutionPatch.CustomResActive = Config.Bind("ExclusiveModeOverride", "CustomResActive", false, "Define custom resolution");
        ResolutionPatch.Width = Config.Bind("ExclusiveModeOverride", "Width", 1920, "Resolution width");
        ResolutionPatch.Height = Config.Bind("ExclusiveModeOverride", "Height", 1080, "Resolution height");
    }
}
