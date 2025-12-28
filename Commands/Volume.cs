using BepInEx;
using CommandsExtended.Commands.Common;
using UnityEngine;

namespace CommandsExtended.Commands;

public sealed class Volume : FloatSettingCommand
{
    public override string[] Aliases => ["volume", "vol"];

    protected override float Min => 0.0f;

    protected override float Max => 1.0f;

    protected override string SettingName => "Master volume";

    protected override float SettingValue
    {

        get
        {
            return SettingsManager.settings.masterVolume;
        }

        set
        {
            SettingsManager.settings.masterVolume = value;
            AudioListener.volume = value;
        }
    }
}