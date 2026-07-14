using CommandsExtended.Common;
using CommandsExtended.Patches;
using MoreCommands.Common;
using System;

namespace CommandsExtended.Commands.CustomCrosshair
{
    public sealed class CrosshairLength : CommandBase
    {
        public override string[] Aliases => ["crosshairlength", "xlength"];
        public override CommandTag Tag => CommandTag.Player;
        public override string Description => "Adjust custom crosshair length";
        public override bool EnablesCheatsOnUse => false;

        public override Action<string[]> GetLogicCallback()
        {
            return args =>
            {
                if (!CrosshairManager.IsCustomEnabled(this.Aliases)) return;

                if (CrosshairManager.TryUpdateSingleFloat(args, CrosshairPatch.Length, this.Aliases, "length"))
                {
                    return;
                }

                ConsoleExt.EchoWithCommand(this.Aliases, "Invalid input");
            };
        }
    }
}