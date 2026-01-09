using CommandsExtended.Common;
using MoreCommands;
using MoreCommands.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommandsExtended.Commands;

public sealed class Marathon : CommandBase
{
    public override string[] Aliases => ["marathon"];

    public override CommandTag Tag => CommandTag.World;

    public override string Description => "Loads all levels in a completable manner";

    public override bool CheatsOnly => false;

    private static readonly string[] levelOrder =
        [
            "m1_intro_01",
            "m1_silos_storage_",
            "m1_silos_safearea_01",
            "m1_silos_broken_",
            "m1_silos_air_",
            "campaign_interlude_silo_to_pipeworks_01",
            "m2_pipeworks_drainage_",
            "m2_pipeworks_waste_",
            "m2_pipeworks_organ_",
            "campaign_interlude_pipeworks_to_habitation_01",
            "m3_habitation_shaft_intro",
            "m3_habitation_shaft_to_pier",
            "m3_habitation_pier_entrance_01",
            "m3_habitation_pier_03",
            "m3_habitation_pier_entrance_01",
            "m3_habitation_pier_04",
            "m3_habitation_pier_entrance_01",
            "m3_habitation_pier_01",
            "m3_habitation_pier_entrance_01",
            "m3_habitation_pier_02",
            "m3_habitation_lab_lobby",
            "m3_habitation_lab_",
            "m3_habitation_lab_ending",
            "campaign_interlude_habitation_to_abyss_01",
            "m4_abyss_transit_",
            "m4_abyss_handle_",
            "m4_abyss_garden_",
            "m4_abyss_outro_01",
        ];

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            List<string> allLevels = [.. Prefabs.Levels().Data().Select(x => x.name)];

            List<string> levels = [];
            List<string> levelsToAdd = [];
            for (int i = 0; i < levelOrder.Length; i++)
            {
                // get exact matches
                levelsToAdd = [.. allLevels
                    .Where(x => x.EqualsIgnoreCase(levelOrder[i]))];

                // if not exact match, get list of levels
                if(!levelsToAdd.Any())
                {
                    levelsToAdd = [.. allLevels
                        .Where(x => x.StartsWithIgnoreCase(levelOrder[i]))];
                    // remove levels that don't have unique names for their start/end
                    levelsToAdd.RemoveAll(x => x.ContainsIgnoreCase("lobby"));
                    levelsToAdd.RemoveAll(x => x.ContainsIgnoreCase("ending"));
                }

                levels.AddRange(levelsToAdd);
            }

            CL_GameManager.gMan.LoadLevels([.. levels]);
        };
    }
}

