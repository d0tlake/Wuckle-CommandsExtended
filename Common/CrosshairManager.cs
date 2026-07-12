using BepInEx.Configuration;
using CommandsExtended.Patches;
using UnityEngine;

namespace CommandsExtended.Common
{
    public static class CrosshairManager
    {
        public static bool IsCustomEnabled(string[] aliases)
        {
            if (CrosshairPatch.Enabled != null && CrosshairPatch.Enabled.Value)
            {
                return true;
            }

            ConsoleExt.EchoWithCommand(aliases, "Crosshair is disabled");

            return false;
        }

        public static void ToggleState(string[] aliases)
        {
            CrosshairPatch.Enabled.Value = !CrosshairPatch.Enabled.Value;
            CrosshairPatch.Enabled.ConfigFile.Save();

            ConsoleExt.EchoWithCommand(aliases, "Crosshair toggled");

            Global.RefreshAllSettings();
        }

        public static void ShowSettings(string[] aliases)
        {
            if (!CrosshairPatch.Enabled.Value)
            {
                ConsoleExt.EchoWithCommand(aliases, "Crosshair is disabled");

                return;
            }

            float len = CrosshairPatch.Length.Value;
            float thick = CrosshairPatch.Thickness.Value;
            float gap = CrosshairPatch.Gap.Value;
            float dot = CrosshairPatch.DotScale.Value;
            int invert = CrosshairPatch.InvertBehind.Value ? 1 : 0;
            string color = ColorToString(CrosshairPatch.CrosshairColor.Value);

            ConsoleExt.EchoWithCommand(aliases, $"customcrosshair {len} {thick} {gap} {dot} {color} {invert}");
        }

        public static bool TryUpdateFullConfig(string[] args, string[] aliases)
        {
            if (args.Length == 9 &&
                float.TryParse(args[0], out float length) &&
                float.TryParse(args[1], out float thickness) &&
                float.TryParse(args[2], out float gap) &&
                float.TryParse(args[3], out float dotScale) &&
                int.TryParse(args[4], out int r) &&
                int.TryParse(args[5], out int g) &&
                int.TryParse(args[6], out int b) &&
                int.TryParse(args[7], out int a) &&
                int.TryParse(args[8], out int invertVal))
            {
                CrosshairPatch.Length.Value = length;
                CrosshairPatch.Thickness.Value = thickness;
                CrosshairPatch.Gap.Value = gap;
                CrosshairPatch.DotScale.Value = dotScale;
                CrosshairPatch.InvertBehind.Value = (invertVal == 1);

                SetColorConfig(r, g, b, a);

                CrosshairPatch.Enabled.Value = true;
                CrosshairPatch.Enabled.ConfigFile.Save();

                ConsoleExt.EchoWithCommand(aliases, "Crosshair updated");

                Global.RefreshAllSettings();

                return true;
            }

            return false;
        }

        public static bool TryUpdateSingleFloat(string[] args, ConfigEntry<float> configEntry, string[] aliases, string valueName)
        {
            if (args == null || args.Length == 0)
            {
                ConsoleExt.EchoWithCommand(aliases, $"{configEntry.Value}");
                return true;
            }

            if (args.Length == 1 && float.TryParse(args[0], out float val))
            {
                configEntry.Value = val;
                CrosshairPatch.Enabled.ConfigFile.Save();

                ConsoleExt.EchoWithCommand(aliases, $"Crosshair {valueName} updated to {val}.");

                Global.RefreshAllSettings();

                return true;
            }

            return false;
        }

        public static bool TryUpdateColor(string[] args, string[] aliases)
        {
            if (args == null || args.Length == 0)
            {
                ConsoleExt.EchoWithCommand(aliases, $"{ColorToString(CrosshairPatch.CrosshairColor.Value)}");

                return true;
            }

            if (args.Length == 4 &&
                int.TryParse(args[0], out int r) &&
                int.TryParse(args[1], out int g) &&
                int.TryParse(args[2], out int b) &&
                int.TryParse(args[3], out int a))
            {
                SetColorConfig(r, g, b, a);
                CrosshairPatch.Enabled.ConfigFile.Save();

                ConsoleExt.EchoWithCommand(aliases, "Crosshair color updated");

                Global.RefreshAllSettings();

                return true;
            }

            return false;
        }

        public static bool TryUpdateInversion(string[] args, string[] aliases)
        {
            if (args == null || args.Length == 0)
            {
                ConsoleExt.EchoWithCommand(aliases, $"{(CrosshairPatch.InvertBehind.Value ? 1 : 0)}");
                return true;
            }

            if (args.Length == 1 && int.TryParse(args[0], out int val))
            {
                bool invertActive = (val == 1);
                CrosshairPatch.InvertBehind.Value = invertActive;
                CrosshairPatch.Enabled.ConfigFile.Save();

                ConsoleExt.EchoWithCommand(aliases, $"Crosshair inversion toggled");

                Global.RefreshAllSettings();
                return true;
            }
            return false;
        }

        private static void SetColorConfig(int r, int g, int b, int a)
        {
            CrosshairPatch.CrosshairColor.Value = new Color(
                Mathf.Clamp01(r / 255f),
                Mathf.Clamp01(g / 255f),
                Mathf.Clamp01(b / 255f),
                Mathf.Clamp01(a / 255f)
            );
        }

        private static string ColorToString(Color color)
        {
            int r = Mathf.RoundToInt(color.r * 255f);
            int g = Mathf.RoundToInt(color.g * 255f);
            int b = Mathf.RoundToInt(color.b * 255f);
            int a = Mathf.RoundToInt(color.a * 255f);

            return $"{r} {g} {b} {a}";
        }
    }
}