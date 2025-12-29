using CommandsExtended.Commands.Common;

namespace CommandsExtended.Commands;

public sealed class Volume : FloatSettingCommand
{
    public override string[] Aliases => ["volume", "vol"];

    protected override float Min => 0.0f;

    protected override float Max => 1.0f;

    protected override string SettingName => "master volume";

    protected override string DisplayPrecision => "F2";

    protected override bool RequiresRefresh => true;

    protected override float SettingValue
    {
        get => SettingsManager.settings.masterVolume;
        set => SettingsManager.settings.masterVolume = value;
    }
}