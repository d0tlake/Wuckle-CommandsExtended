using CommandsExtended.Commands.Common;

namespace CommandsExtended.Commands;

public sealed class Sensitivity : FloatSettingCommand
{
    public override string[] Aliases => ["sensitivity", "sens"];

    protected override float Min => 0.0f;

    protected override float Max => 100.0f;

    protected override string SettingName => "Sensitivity";

    protected override string DisplayPrecision => "F4";

    protected override bool RequiresRefresh => false;

    protected override float SettingValue
    {
        get => SettingsManager.settings.mouseSensitivity;
        set => SettingsManager.settings.mouseSensitivity = value;
    }
}