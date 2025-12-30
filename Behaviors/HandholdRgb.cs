using CommandsExtended.Commands;
using UnityEngine;

namespace CommandsExtended.Behaviors;

public sealed class HandholdRgb : MonoBehaviour
{
    public static float Speed;

    public static float DefaultSpeed = 0.1f;

    public void Update()
    {
        if (ShowGrabs.HighlightMat != null)
        {
            float hue = (Time.time * Speed) % 1f;

            Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
            ShowGrabs.HighlightMat.SetColor("_Color", rainbowColor);
        }
    }
}