using CommandsExtended.Common;
using MoreCommands.Common;
using System;

namespace CommandsExtended.Commands.CustomCrosshair
{
    public sealed class CrosshairColor : CommandBase
    {
        public override string[] Aliases => ["crosshaircolor", "xcolor"];
        public override CommandTag Tag => CommandTag.Player;
        public override string Description => "Adjust custom crosshair color (example: crosshaircolor 255 125 35 255)";
        public override bool EnablesCheatsOnUse => false;

        public override Action<string[]> GetLogicCallback()
        {
            return args =>
            {
                if (!CrosshairManager.IsCustomEnabled(this.Aliases)) return;

                if (CrosshairManager.TryUpdateColor(args, this.Aliases))
                {
                    return;
                }

                ConsoleExt.EchoWithCommand(this.Aliases, "Invalid input");
            };
        }
    }
}