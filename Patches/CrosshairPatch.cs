using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace CommandsExtended.Patches
{
    [HarmonyPatch(typeof(UI_CrosshairController))]
    public static class CrosshairPatch
    {
        public static ConfigEntry<bool> Enabled;
        public static ConfigEntry<Color> CrosshairColor;

        public static ConfigEntry<float> Length;
        public static ConfigEntry<float> Thickness;
        public static ConfigEntry<float> Gap;
        public static ConfigEntry<float> DotScale;
        public static ConfigEntry<bool> InvertBehind;

        private static GameObject customContainer;
        private static Material originalInvertMat;

        private static Image centerDotInvert;
        private static Image lineLeftInvert;
        private static Image lineRightInvert;
        private static Image lineTopInvert;
        private static Image lineBottomInvert;

        private static Image centerDotOverlay;
        private static Image lineLeftOverlay;
        private static Image lineRightOverlay;
        private static Image lineTopOverlay;
        private static Image lineBottomOverlay;

        [HarmonyPatch("Refresh")]
        [HarmonyPostfix]
        public static void PostfixRefresh(UI_CrosshairController __instance)
        {
            if (__instance == null) return;

            if (Enabled == null || !Enabled.Value)
            {
                __instance.crosshairRenderer?.enabled = true;
                customContainer?.SetActive(false);
                return;
            }

            if (__instance.crosshairRenderer != null)
            {
                if (originalInvertMat == null && __instance.crosshairRenderer.material != null)
                {
                    originalInvertMat = __instance.crosshairRenderer.material;
                }
                __instance.crosshairRenderer.enabled = false;
            }

            if (__instance.crosshair != null)
            {
                if (customContainer == null || customContainer.gameObject == null)
                {
                    BuildCrosshair(__instance.crosshair);
                }

                UpdateCrosshair();
            }
        }

        private static void UpdateCrosshair()
        {
            customContainer.SetActive(true);

            float gameScale = (float)SettingsManager.settings.crosshairScale * 0.04f;

            Color color = CrosshairColor?.Value ?? Color.green;
            float maxC = Mathf.Max(color.r, color.g, color.b);
            Color colorInvert = new(maxC, maxC, maxC, 1f);

            float len = (Length?.Value ?? 1f) * gameScale;
            float thick = (Thickness?.Value ?? 1f) * gameScale;
            float gapOffset = (Gap?.Value ?? 1f) * gameScale;

            float dotScale = DotScale?.Value ?? 1f;
            float dotSize = dotScale * gameScale;

            bool showLines = len > 0f && thick > 0f;
            bool showDot = dotScale > 0f;
            bool useInversion = InvertBehind != null && InvertBehind.Value;

            bool showInversionLines = showLines && useInversion;
            bool showInversionDot = showDot && useInversion;

            Material baseMaterial = useInversion ? originalInvertMat : null;

            Vector2 dotSizeVector = new(dotSize, dotSize);

            Vector2 horizontalSize = new(len, thick);
            Vector2 verticalSize = new(thick, len);

            Vector2 leftPos = new(-gapOffset - (len / 2f), 0f);
            Vector2 rightPos = new(gapOffset + (len / 2f), 0f);
            Vector2 topPos = new(0f, gapOffset + (len / 2f));
            Vector2 bottomPos = new(0f, -gapOffset - (len / 2f));

            UpdateElement(centerDotInvert, showInversionDot, colorInvert, baseMaterial, dotSizeVector, Vector2.zero);
            UpdateElement(lineLeftInvert, showInversionLines, colorInvert, baseMaterial, horizontalSize, leftPos);
            UpdateElement(lineRightInvert, showInversionLines, colorInvert, baseMaterial, horizontalSize, rightPos);
            UpdateElement(lineTopInvert, showInversionLines, colorInvert, baseMaterial, verticalSize, topPos);
            UpdateElement(lineBottomInvert, showInversionLines, colorInvert, baseMaterial, verticalSize, bottomPos);

            UpdateElement(centerDotOverlay, showDot, color, null, dotSizeVector, Vector2.zero);
            UpdateElement(lineLeftOverlay, showLines, color, null, horizontalSize, leftPos);
            UpdateElement(lineRightOverlay, showLines, color, null, horizontalSize, rightPos);
            UpdateElement(lineTopOverlay, showLines, color, null, verticalSize, topPos);
            UpdateElement(lineBottomOverlay, showLines, color, null, verticalSize, bottomPos);
        }

        private static void UpdateElement(Image img, bool show, Color color, Material mat, Vector2 size, Vector2 pos)
        {
            img.gameObject.SetActive(show);
            if (!show) return;

            img.color = color;
            img.material = mat;
            img.rectTransform.sizeDelta = size;
            img.rectTransform.anchoredPosition = pos;
        }

        private static void BuildCrosshair(Transform parent)
        {
            customContainer = new GameObject("CustomCrosshairContainer", typeof(RectTransform));
            customContainer.transform.SetParent(parent, false);
            customContainer.GetComponent<RectTransform>().localScale = Vector3.one;

            centerDotInvert = CreateLineElement("XhairInvertCenterDot", customContainer.transform);
            lineLeftInvert = CreateLineElement("XhairInvertLeft", customContainer.transform);
            lineRightInvert = CreateLineElement("XhairInvertRight", customContainer.transform);
            lineTopInvert = CreateLineElement("XhairInvertTop", customContainer.transform);
            lineBottomInvert = CreateLineElement("XhairInvertBottom", customContainer.transform);

            centerDotOverlay = CreateLineElement("XhairCenterDotOverlay", customContainer.transform);
            lineLeftOverlay = CreateLineElement("XhairLeftOverlay", customContainer.transform);
            lineRightOverlay = CreateLineElement("XhairRightOverlay", customContainer.transform);
            lineTopOverlay = CreateLineElement("XhairTopOverlay", customContainer.transform);
            lineBottomOverlay = CreateLineElement("XhairBottomOverlay", customContainer.transform);
        }

        private static Image CreateLineElement(string name, Transform parent)
        {
            GameObject obj = new(name, typeof(RectTransform));
            obj.transform.SetParent(parent, false);

            Image img = obj.AddComponent<Image>();
            img.raycastTarget = false;

            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);

            return img;
        }
    }
}