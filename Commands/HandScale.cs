using System;
using CommandsExtended.Commands.Common;

namespace CommandsExtended.Commands;

public sealed class HandScale : DoubleSettingCommand
{
    public override string[] Aliases => ["handscale", "hscale"];

    protected override double Min => 0;

    protected override double Max => 10;

    protected override string SettingName => "hand scale";

    protected override bool RequiresRefresh => true;

    protected override double SettingValue
    {
        get => SettingsManager.settings.handIconScale;
        set => SettingsManager.settings.handIconScale = value;
    }
}