using System;
using CommandsExtended.Commands.Common;

namespace CommandsExtended.Commands;

public sealed class Sensitivity : DoubleSettingCommand
{
    public override string[] Aliases => ["sensitivity", "sens"];

    protected override double Min => 0;

    protected override double Max => 100;

    protected override string SettingName => "mouse sensitivity";

    protected override bool RequiresRefresh => false;

    protected override double SettingValue
    {
        get => SettingsManager.settings.mouseSensitivity;
        set => SettingsManager.settings.mouseSensitivity = value;
    }
}