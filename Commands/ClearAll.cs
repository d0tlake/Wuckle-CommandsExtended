using CommandsExtended.Common;
using MoreCommands.Common;
using System;

namespace CommandsExtended.Commands;

public sealed class ClearAll : CommandBase
{
    public override string[] Aliases => ["clear"];

    public override CommandTag Tag => CommandTag.Player;

    public override string Description => "Clear any current perks and buffs";

    public override bool CheatsOnly => true;

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            Player.ClearBuffs();
            Player.ClearPerks();
        };
    }
}

