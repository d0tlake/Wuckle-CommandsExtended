using MoreCommands.Common;
using System;
using UnityEngine.SceneManagement;

namespace CommandsExtended.Commands;

public sealed class Playground : CommandBase
{
    public override string[] Aliases => ["playground", "pg"];

    public override CommandTag Tag => CommandTag.World;

    public override string Description => "Loads the playground scene";

    public override bool EnablesCheatsOnUse => false;

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            M_Gamemode gamemodeAsset = CL_AssetManager.GetGamemodeAsset("GM_Playground");
            CL_GameManager.gMan.SetGamemode(gamemodeAsset);
            SceneManager.LoadScene("Playground");
        };
    }
}

