using CommandsExtended.Common;
using HarmonyLib;
using MoreCommands.Accessors;
using MoreCommands.Common;
using System;
using System.Linq;

namespace CommandsExtended.Commands.Common;

public abstract class DoubleSettingCommand : CommandBase
{
    public override string Description => $"Change {this.SettingName}, saves to player's settings (min: {this.Min}, max: {this.Max})";

    public override CommandTag Tag => CommandTag.Player;

    public override bool EnablesCheatsOnUse => false;

    protected abstract double SettingValue { get; set; }

    protected abstract string SettingName { get; }

    protected abstract double Min { get; }

    protected abstract double Max { get; }

    protected abstract bool RequiresRefresh { get; }

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            if (args.Length == 0)
            {
                CommandConsoleAccessor.EchoToConsole($"Current {this.SettingName}: {this.SettingValue}");
                return;
            }

            bool valid = double.TryParse(args[0], out double val);

            if (valid)
            {
                if (val < Min)
                {
                    CommandConsoleAccessor.EchoToConsole($"{this.SettingName} cannot be below {this.Min}");
                }
                else if (val > Max)
                {
                    CommandConsoleAccessor.EchoToConsole($"{this.SettingName} cannot be above {this.Max}");
                }
                else
                {
                    SettingsManager.instance.LoadSettings();
                    this.SettingValue = val;
                    SettingsManager.instance.SaveSettings();
                    if (this.RequiresRefresh)
                        Global.RefreshSettings();
                    CommandConsoleAccessor.EchoToConsole($"{this.SettingName} set to {val}");
                }
            }
            else
            {
                CommandConsoleAccessor.EchoToConsole($"Invalid arguments for {this.SettingName} command: {args.Join(delimiter: " ")}");
            }
        };
    }
}