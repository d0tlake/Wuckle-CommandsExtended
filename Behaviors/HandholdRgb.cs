using CommandsExtended.Commands;
using UnityEngine;

namespace CommandsExtended.Behaviors;

public class HandholdRgb : MonoBehaviour
{
    private float speed = 0.1f;

    public void Update()
    {
        if (ShowGrabs.HighlightMat != null)
        {
            float hue = (Time.time * speed) % 1f;

            Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);
            ShowGrabs.HighlightMat.SetColor("_Color", rainbowColor);
        }
    }
}