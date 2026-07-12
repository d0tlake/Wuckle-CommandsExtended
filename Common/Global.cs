using System;

namespace CommandsExtended.Common
{
    public static class Global
    {
        public static void RefreshAllSettings()
        {
            try
            {
                SettingsManager.RefreshSettings(string.Empty);
            }
            catch (NullReferenceException)
            {
                Plugin.Logger.LogInfo("Refresh settings attempt failed, most likely not initialized yet.");
            }
        }
    }
}
