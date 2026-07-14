using CommandsExtended.Common;
using CommandsExtended.Patches;
using MoreCommands.Common;
using System;

namespace CommandsExtended.Commands.CustomCrosshair
{
    public sealed class CrosshairThickness : CommandBase
    {
        public override string[] Aliases => ["crosshairthickness", "xthick"];
        public override CommandTag Tag => CommandTag.Player;
        public override string Description => "Adjust custom crosshair thickness";
        public override bool EnablesCheatsOnUse => false;

        public override Action<string[]> GetLogicCallback()
        {
            return args =>
            {
                if (!CrosshairManager.IsCustomEnabled(this.Aliases)) return;

                if (CrosshairManager.TryUpdateSingleFloat(args, CrosshairPatch.Thickness, this.Aliases, "thickness"))
                {
                    return;
                }

                ConsoleExt.EchoWithCommand(this.Aliases, "Invalid input");
            };
        }
    }
}