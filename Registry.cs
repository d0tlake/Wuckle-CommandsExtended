using CommandsExtended.Commands;
using CommandsExtended.Commands.CustomCrosshair;
using MoreCommands.Common;

namespace CommandsExtended
{
    public static class Registry
    {
        public static void RegisterAll()
        {
            CommandRegistry.Register(new Raycast());
            CommandRegistry.Register(new ShowGrabs());
            CommandRegistry.Register(new Marathon());
            CommandRegistry.Register(new Fov());
            CommandRegistry.Register(new Volume());
            CommandRegistry.Register(new Music());
            CommandRegistry.Register(new Sensitivity());
            CommandRegistry.Register(new ClearBuffs());
            CommandRegistry.Register(new ClearPerks());
            CommandRegistry.Register(new ClearAll());
            CommandRegistry.Register(new Playground());
            CommandRegistry.Register(new HandScale());
            CommandRegistry.Register(new FullscreenExclusive());
            CommandRegistry.Register(new CustomCrosshair());
            CommandRegistry.Register(new CrosshairColor());
            CommandRegistry.Register(new CrosshairInvert());
            CommandRegistry.Register(new CrosshairDot());
            CommandRegistry.Register(new CrosshairGap());
            CommandRegistry.Register(new CrosshairLength());
            CommandRegistry.Register(new CrosshairScale());
            CommandRegistry.Register(new CrosshairThickness());
        }
    }
}
