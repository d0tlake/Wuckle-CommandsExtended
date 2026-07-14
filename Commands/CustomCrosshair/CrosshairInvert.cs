using CommandsExtended.Common;
using MoreCommands.Common;
using System;

namespace CommandsExtended.Commands.CustomCrosshair
{
    public sealed class CrosshairInvert : CommandBase
    {
        public override string[] Aliases => ["crosshairinvert", "xinvert"];
        public override CommandTag Tag => CommandTag.Player;
        public override string Description => "Toggle crosshair inversion effect (like original crosshair). 0 or 1";
        public override bool EnablesCheatsOnUse => false;

        public override Action<string[]> GetLogicCallback()
        {
            return args =>
            {
                if (!CrosshairManager.IsCustomEnabled(this.Aliases)) return;

                if (CrosshairManager.TryUpdateInversion(args, this.Aliases))
                {
                    return;
                }

                ConsoleExt.EchoWithCommand(this.Aliases, "Invalid input");
            };
        }
    }
}