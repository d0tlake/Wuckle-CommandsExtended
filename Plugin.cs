using System;
using BepInEx;
using BepInEx.Logging;

namespace CommandsExtended;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("shishyando.WK.MoreCommands", "0.11.2")]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource Beep;

    public void Awake()
    {
        Beep = Logger;
        Registry.RegisterAll();
        Beep.LogInfo($"{MyPluginInfo.PLUGIN_GUID} is loaded");
    }

    public static void Assert(bool condition)
    {
        if (!condition)
        {
            Beep.LogFatal($"Assert failed");
            throw new Exception($"[CommandsExtended] Assert failed");
        }
    }
}