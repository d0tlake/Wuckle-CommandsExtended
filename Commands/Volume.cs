using System;
using CommandsExtended.Commands.Common;

namespace CommandsExtended.Commands;

public sealed class Volume : DoubleSettingCommand
{
    public override string[] Aliases => ["volume", "vol"];

    protected override double Min => 0;

    protected override double Max => 1;

    protected override string SettingName => "master volume";

    protected override bool RequiresRefresh => true;

    protected override double SettingValue
    {
        get => SettingsManager.settings.masterVolume;
        set => SettingsManager.settings.masterVolume = value;
    }
}