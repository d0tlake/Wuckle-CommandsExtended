using CommandsExtended.Common;
using CommandsExtended.Patches;
using MoreCommands.Common;
using System;

namespace CommandsExtended.Commands.CustomCrosshair
{
    public sealed class CrosshairGap : CommandBase
    {
        public override string[] Aliases => ["crosshairgap", "xgap"];
        public override CommandTag Tag => CommandTag.Player;
        public override string Description => "Adjust custom crosshair gap";
        public override bool EnablesCheatsOnUse => false;

        public override Action<string[]> GetLogicCallback()
        {
            return args =>
            {
                if (!CrosshairManager.IsCustomEnabled(this.Aliases)) return;

                if (CrosshairManager.TryUpdateSingleFloat(args, CrosshairPatch.Gap, this.Aliases, "gap"))
                {
                    return;
                }

                ConsoleExt.EchoWithCommand(this.Aliases, "Invalid input");
            };
        }
    }
}