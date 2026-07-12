using System;
using CommandsExtended.Common;
using MoreCommands.Accessors;
using MoreCommands.Common;

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
                if (!CrosshairManager.IsCustomEnabled(Aliases)) return;

                if (CrosshairManager.TryUpdateInversion(args, Aliases))
                {
                    return;
                }

                ConsoleExt.EchoWithCommand(Aliases, "Invalid input");
            };
        }
    }
}