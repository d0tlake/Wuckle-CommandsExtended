using HarmonyLib;
using MoreCommands.Accessors;
using MoreCommands.Common;
using System;
using System.Linq;

namespace CommandsExtended.Commands.Common;

public abstract class FloatSettingCommand : CommandBase
{
    public override string Description => $"Change {this.SettingName}, saves to player's settings (min: {this.Min}, max: {this.Max})";

    public override CommandTag Tag => CommandTag.Player;

    public override bool CheatsOnly => false;

    protected abstract float SettingValue { get; set; }

    protected abstract string SettingName { get; }

    protected abstract float Min { get; }

    protected abstract float Max { get; }

    protected abstract string DisplayPrecision { get; }

    protected abstract bool RequiresRefresh { get; }

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            if (args.Length == 0)
            {
                CommandConsoleAccessor.EchoToConsole($"Current {this.SettingName}: {this.SettingValue.ToString(DisplayPrecision)}");
                return;
            }

            bool valid = float.TryParse(args[0], out float val);

            if (valid)
            {
                if (val < Min)
                {
                    CommandConsoleAccessor.EchoToConsole($"{this.SettingName} cannot be below {this.Min.ToString(DisplayPrecision)}");
                }
                else if (val > Max)
                {
                    CommandConsoleAccessor.EchoToConsole($"{this.SettingName} cannot be above {this.Max.ToString(DisplayPrecision)}");
                }
                else
                {
                    SettingsManager.instance.LoadSettings();
                    this.SettingValue = val;
                    SettingsManager.instance.SaveSettings();
                    if(this.RequiresRefresh)
                        SettingsManager.RefreshSettings();
                    CommandConsoleAccessor.EchoToConsole($"{this.SettingName} set to {val.ToString(this.DisplayPrecision)}");
                }
            }
            else
            {
                CommandConsoleAccessor.EchoToConsole($"Invalid arguments for {this.SettingName} command: {args.Join(delimiter: " ")}");
            }
        };
    }
}