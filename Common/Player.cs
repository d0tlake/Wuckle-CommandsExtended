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
        }
    }
}
