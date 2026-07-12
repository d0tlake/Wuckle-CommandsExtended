using CommandsExtended.Commands.Common;
using System;

namespace CommandsExtended.Commands;

public sealed class Fov : DoubleSettingCommand
{
    public override string[] Aliases => ["fov"];

    protected override double Min => 60;

    protected override double Max => 140;

    protected override string SettingName => "Player FOV";

    protected override bool RequiresRefresh => false;

    protected override double SettingValue
    {
        get => SettingsManager.settings.playerFOV;
        set => SettingsManager.settings.playerFOV = (float)Math.Round(value, 2);
    }
}