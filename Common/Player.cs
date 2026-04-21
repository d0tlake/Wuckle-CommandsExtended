using HarmonyLib;
using System.Reflection;

namespace CommandsExtended.Common
{
    public static class Player
    {
        public static void ClearBuffs()
        {
            ENT_Player.playerObject?.curBuffs?.Initialize();
        }

        public static void ClearPerks()
        {
            ENT_Player.playerObject?.RemoveAllPerks();
            if (ENT_Player.playerObject != null )
            {
                FieldInfo extraJumps = AccessTools.Field(typeof(ENT_Player), "extraJumpsRemaining");
                extraJumps.SetValue(ENT_Player.playerObject, 0);
            }
        }
    }
}
