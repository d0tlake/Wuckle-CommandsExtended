using CommandsExtended.Commands.Common;
using MoreCommands.Common;
using System;
using UnityEngine;

namespace CommandsExtended.Commands;

public sealed class Timer : CommandBase
{
    public override string[] Aliases => ["timer"];

    public override CommandTag Tag => CommandTag.Player;

    public override string Description => "Show and hide timer.";

    public override bool EnablesCheatsOnUse => false;

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            bool newValue = !SettingsManager.settings.g_timer;

            SettingsManager.instance.LoadSettings();
            SettingsManager.settings.g_timer = newValue;
            SettingsManager.instance.SaveSettings();
            ((Component)(object)CL_UIManager.instance.timer).gameObject.SetActive(newValue);
        };
    }
}