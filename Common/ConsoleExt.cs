using MoreCommands.Accessors;
using System.Linq;

namespace CommandsExtended.Common
{
    public static class ConsoleExt
    {
        public static void EchoWithCommand(string[] aliases, string msg)
        {
            CommandConsoleAccessor.EchoToConsole($"[{Colors.Highlighted(aliases.First())}] {msg}");
        }
    }
}
