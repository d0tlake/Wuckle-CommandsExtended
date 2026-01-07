using CommandsExtended.Commands;
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
        }
    }
}
