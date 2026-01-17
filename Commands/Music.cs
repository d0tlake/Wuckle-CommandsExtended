using System;
using System.ComponentModel;
using CommandsExtended.Commands.Common;

namespace CommandsExtended.Commands;

public sealed class Music : FloatSettingCommand
{
    public override string[] Aliases => ["music"];

    protected override float Min => 0.0f;

    protected override float Max => 1.0f;

    protected override string SettingName => "music volume";

    protected override string DisplayPrecision => "F2";

    protected override bool RequiresRefresh => true;

    protected override float SettingValue
    {
        get => Convert.ToSingle(SettingsManager.settings.musicVolume);
        set => SettingsManager.settings.musicVolume = Convert.ToSingle(value);
    }
}