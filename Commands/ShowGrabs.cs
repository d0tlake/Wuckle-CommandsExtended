using CommandsExtended.Behaviors;
using MoreCommands.Common;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CommandsExtended.Commands;

public sealed class ShowGrabs : TogglableCommandBase
{
    public override string[] Aliases => ["ShowGrabs", "sg"];

    public override CommandTag Tag => CommandTag.World;

    public override string Description => "temp";

    public override bool CheatsOnly => true;


    public static Material HighlightMat;


    private GameObject animationObj;

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            if (this.Enabled)
            {
                this.SetupVisualizer();
            }
            else if (!this.Enabled)
            {
                this.OnExit();
            }
        };
    }

    private void SetupVisualizer()
    {
        if (HighlightMat == null)
        {
            HighlightMat = new Material(Shader.Find("Hidden/Internal-Colored"));
            HighlightMat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry + 1;
            HighlightMat.color = new Color(0, 0.8f, 0, 0.5f); // Transparent green
            HighlightMat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off); // Show both sides
            HighlightMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            HighlightMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            HighlightMat.DisableKeyword("_EMISSION");
            HighlightMat.SetInt("_ZWrite", 1);
            // fix z-fighting caused by breathing textures, also makes handholds easier to see
            HighlightMat.SetFloat("_ZBias", -10.0f);
        }

        if (animationObj == null)
        {
            this.animationObj = new GameObject("ShowGrabsbleAnimation");
            this.animationObj.AddComponent<HandholdRgb>();
            Object.DontDestroyOnLoad(this.animationObj);
        }

        CL_Handhold[] allHandholds = Resources.FindObjectsOfTypeAll<CL_Handhold>();

        foreach (var handhold in allHandholds)
        {
            GameObject go = handhold.gameObject;
            go.AddComponent<HandholdVisualizer>();
        }
    }

    public override void OnExit()
    {
        CL_Handhold[] allHandholds = Resources.FindObjectsOfTypeAll<CL_Handhold>();

        if (this.animationObj != null)
        {
            Object.Destroy(this.animationObj);
            this.animationObj = null;
        }

        foreach (var handhold in allHandholds)
        {
            GameObject go = handhold.gameObject;

            bool hasVisualizer = go.TryGetComponent(out HandholdVisualizer visualizer);
            if (hasVisualizer)
            {
                visualizer.HideHandholds();
                Object.Destroy(visualizer);
            }
        }

        this.Enabled = false;
    }
}

