using MoreCommands.Common;
using System;

namespace CommandsExtended.Commands;

public sealed class ClearBuff : CommandBase
{
    public override string[] Aliases => ["clearbuff", "clear"];

    public override CommandTag Tag => CommandTag.Player;

    public override string Description => "Clear any buffs";

    public override bool CheatsOnly => true;

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            ENT_Player.playerObject?.curBuffs?.Initialize();
        };
    }
}

