using CommandsExtended.Common;
using MoreCommands.Common;
using System;

namespace CommandsExtended.Commands;

public sealed class ClearBuffs : CommandBase
{
    public override string[] Aliases => ["clearbuff"];

    public override CommandTag Tag => CommandTag.Player;

    public override string Description => "Clear any current buffs";

    public override bool CheatsOnly => true;

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            Player.ClearBuffs();
        };
    }
}

