using CommandsExtended.Commands.Common;

namespace CommandsExtended.Commands;

public sealed class Volume : FloatSettingCommand
{
    public override string[] Aliases => ["volume", "vol"];

    protected override float Min => 0;

    protected override float Max => 1;

    protected override string SettingName => "Master volume";

    protected override float SettingValue
    {
        get => SettingsManager.settings.masterVolume;
        set => SettingsManager.settings.masterVolume = value;
    }
}