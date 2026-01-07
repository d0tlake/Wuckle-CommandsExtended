using CommandsExtended.Common;
using MoreCommands.Common;
using System;

namespace CommandsExtended.Commands;

public sealed class ClearPerks : CommandBase
{
    public override string[] Aliases => ["clearperk"];

    public override CommandTag Tag => CommandTag.Player;

    public override string Description => "Clear any current perks";

    public override bool CheatsOnly => true;

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            Player.ClearPerks();
        };
    }
}

