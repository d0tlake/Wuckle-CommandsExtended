using CommandsExtended.Common;
using HarmonyLib;
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
                ConsoleExt.EchoWithCommand(this.Aliases, $"Current {this.SettingName}: {this.SettingValue}");
                return;
            }

            bool valid = double.TryParse(args[0], out double val);

            if (valid)
            {
                if (val < this.Min)
                {
                    ConsoleExt.EchoWithCommand(this.Aliases, $"{this.SettingName} cannot be below {this.Min}");
                }
                else if (val > this.Max)
                {
                    ConsoleExt.EchoWithCommand(this.Aliases, $"{this.SettingName} cannot be above {this.Max}");
                }
                else
                {
                    SettingsManager.instance.LoadSettings();
                    this.SettingValue = val;
                    SettingsManager.instance.SaveSettings();
                    if (this.RequiresRefresh)
                        Global.RefreshAllSettings();
                    ConsoleExt.EchoWithCommand(this.Aliases, $"{this.SettingName} set to {val}");
                }
            }
            else
            {
                ConsoleExt.EchoWithCommand(this.Aliases, $"Invalid arguments for {this.SettingName} command: {args.Join(delimiter: " ")}");
            }
        };
    }
}