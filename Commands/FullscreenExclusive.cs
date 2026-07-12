using CommandsExtended.Common;
using CommandsExtended.Patches;
using MoreCommands.Accessors;
using MoreCommands.Common;
using System;
using System.Linq;
using UnityEngine;

namespace CommandsExtended.Commands
{
    public sealed class FullscreenExclusive : CommandBase
    {
        public override string[] Aliases => ["exclusivemode", "exmode"];

        public override CommandTag Tag => CommandTag.Player;

        public override string Description => "Toggle exclusive fullscreen/borderless, or set resolution if two numbers are provided. Allows for stretched resolutions (example: fsmode 1920 1440)";

        public override bool EnablesCheatsOnUse => false;

        public override Action<string[]> GetLogicCallback()
        {
            return args =>
            {
                if (args?.Length == 2 || !ResolutionPatch.Enabled.Value)
                {
                    ResolutionPatch.Enabled.Value = true;
                    ResolutionPatch.CustomResActive.Value = false;

                    if (args != null && args.Length >= 2 &&
                        int.TryParse(args[0], out int width) &&
                        int.TryParse(args[1], out int height))
                    {
                        ResolutionPatch.Width.Value = width;
                        ResolutionPatch.Height.Value = height;
                        ResolutionPatch.CustomResActive.Value = true;

                        CommandConsoleAccessor.EchoToConsole(Colors.Highlighted(Aliases.First()) + " " + "Custom resolution set");
                    }

                    ResolutionPatch.Enabled.ConfigFile.Save();

                    CommandConsoleAccessor.EchoToConsole(Colors.Highlighted(Aliases.First()) + " " + "Exclusive fullscreen enabled");
                }
                else if (ResolutionPatch.Enabled.Value)
                {
                    ResolutionPatch.Enabled.Value = false;
                    ResolutionPatch.CustomResActive.Value = false;

                    ResolutionPatch.Enabled.ConfigFile.Save();

                    CommandConsoleAccessor.EchoToConsole(Colors.Highlighted(Aliases.First()) + " " + "Exclusive fullscreen disabled");
                }
                else
                {
                    CommandConsoleAccessor.EchoToConsole(Colors.Highlighted(Aliases.First()) + " " + "Invalid input");
                    return;
                }

                Global.RefreshSettings();
            };
        }
    }
}