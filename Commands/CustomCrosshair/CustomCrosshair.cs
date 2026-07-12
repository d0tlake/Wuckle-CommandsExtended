using System;
using CommandsExtended.Common;
using CommandsExtended.Patches;
using MoreCommands.Accessors;
using MoreCommands.Common;

namespace CommandsExtended.Commands.CustomCrosshair
{
    public sealed class CustomCrosshair : CommandBase
    {
        public override string[] Aliases => ["customcrosshair", "xhair"];
        public override CommandTag Tag => CommandTag.Player;

        public override string Description =>
            "Toggles custom crosshair if no args. Pass args for config: [length] [thickness] [gap] [dot_scale] [r] [g] [b] [a] [invert(0/1)], " +
            "or 'customcrosshair show' to view config.";

        public override bool EnablesCheatsOnUse => false;

        public override Action<string[]> GetLogicCallback()
        {
            return args =>
            {
                if (CrosshairPatch.Enabled == null) return;

                if (args == null || args.Length == 0)
                {
                    CrosshairManager.ToggleState(Aliases);
                    return;
                }
                else if (args.Length == 1)
                {
                    if (args[0].Equals("show", StringComparison.OrdinalIgnoreCase))
                    {
                        CrosshairManager.ShowSettings(Aliases);
                        return;
                    }
                }
                else if (args.Length == 9)
                {
                    if (CrosshairManager.TryUpdateFullConfig(args, Aliases))
                    {
                        return;
                    }
                }

                ConsoleExt.EchoWithCommand(Aliases, "Invalid input");
            };
        }
    }
}