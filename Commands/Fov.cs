using CommandsExtended.Commands.Common;

namespace CommandsExtended.Commands;

public sealed class Fov : FloatSettingCommand
{
    public override string[] Aliases => ["fov"];

    protected override float Min => 60;

    protected override float Max => 140;

    protected override string SettingName => "Player FOV";

    protected override float SettingValue
    {
        get => SettingsManager.settings.playerFOV;
        set => SettingsManager.settings.playerFOV = value;
    }
}