using System;
using CommandsExtended.Commands.Common;

namespace CommandsExtended.Commands;

public sealed class CrosshairScale : DoubleSettingCommand
{
    public override string[] Aliases => ["crosshairscale", "chscale"];

    protected override double Min => 0;

    protected override double Max => 10;

    protected override string SettingName => "crosshair scale";

    protected override bool RequiresRefresh => true;

    protected override double SettingValue
    {
        get => SettingsManager.settings.crosshairScale;
        set => SettingsManager.settings.crosshairScale = value;
    }
}