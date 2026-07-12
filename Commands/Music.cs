using CommandsExtended.Commands.Common;

namespace CommandsExtended.Commands;

public sealed class Music : DoubleSettingCommand
{
    public override string[] Aliases => ["music"];

    protected override double Min => 0;

    protected override double Max => 1;

    protected override string SettingName => "music volume";

    protected override bool RequiresRefresh => true;

    protected override double SettingValue
    {
        get => SettingsManager.settings.musicVolume;
        set => SettingsManager.settings.musicVolume = value;
    }
}