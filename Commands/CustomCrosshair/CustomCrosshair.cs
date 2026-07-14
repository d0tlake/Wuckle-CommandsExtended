using CommandsExtended.Common;
using CommandsExtended.Patches;
using MoreCommands.Common;
using System;

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
                    CrosshairManager.ToggleState(this.Aliases);
                    return;
                }
                else if (args.Length == 1)
                {
                    if (args[0].Equals("show", StringComparison.OrdinalIgnoreCase))
                    {
                        CrosshairManager.ShowSettings(this.Aliases);
                        return;
                    }
                }
                else if (args.Length == 9)
                {
                    if (CrosshairManager.TryUpdateFullConfig(args, this.Aliases))
                    {
                        return;
                    }
                }

                ConsoleExt.EchoWithCommand(this.Aliases, "Invalid input");
            };
        }
    }
}