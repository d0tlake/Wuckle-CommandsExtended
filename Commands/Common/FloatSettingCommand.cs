using HarmonyLib;
using MoreCommands.Accessors;
using MoreCommands.Common;
using System;
using System.Linq;

namespace CommandsExtended.Commands.Common;

public abstract class FloatSettingCommand : CommandBase
{
    public override string Description => $"Change {SettingName}, saves to player's settings (min: {Min}, max: {Max})";

    public override CommandTag Tag => CommandTag.Player;

    public override bool CheatsOnly => false;

    protected abstract float SettingValue { get; set; }

    protected abstract string SettingName { get; }

    protected abstract float Min { get; }

    protected abstract float Max { get; }


    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            if (args.Length == 0)
            {
                CommandConsoleAccessor.EchoToConsole($"Current {SettingName}: {SettingValue:F1}");
                return;
            }

            bool valid = float.TryParse(args[0], out float val);

            if (valid)
            {
                if (val < Min)
                {
                    CommandConsoleAccessor.EchoToConsole($"{SettingName} cannot be below {Min:F1}");
                }
                else if (val > Max)
                {
                    CommandConsoleAccessor.EchoToConsole($"{SettingName} cannot be above {Max:F1}");
                }
                else
                {
                    SettingsManager.instance.LoadSettings();
                    SettingValue = val;
                    SettingsManager.instance.SaveSettings();
                    CommandConsoleAccessor.EchoToConsole($"{SettingName} set to {val:F1}");
                }
            }
            else
            {
                CommandConsoleAccessor.EchoToConsole($"Invalid arguments for {SettingName} command: {args.Join(delimiter: " ")}");
            }
        };
    }
}