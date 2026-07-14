using CommandsExtended.Common;
using CommandsExtended.Patches;
using MoreCommands.Common;
using System;

namespace CommandsExtended.Commands.CustomCrosshair
{
    public sealed class CrosshairDot : CommandBase
    {
        public override string[] Aliases => ["crosshairdot", "xdot"];
        public override CommandTag Tag => CommandTag.Player;
        public override string Description => "Adjust custom crosshair center dot scale";
        public override bool EnablesCheatsOnUse => false;

        public override Action<string[]> GetLogicCallback()
        {
            return args =>
            {
                if (!CrosshairManager.IsCustomEnabled(this.Aliases)) return;

                if (CrosshairManager.TryUpdateSingleFloat(args, CrosshairPatch.DotScale, this.Aliases, "dot scale"))
                {
                    return;
                }

                ConsoleExt.EchoWithCommand(this.Aliases, "Invalid input");
            };
        }
    }
}