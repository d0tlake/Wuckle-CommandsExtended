using CommandsExtended.Behaviors;
using CommandsExtended.Common;
using MoreCommands.Common;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CommandsExtended.Commands;

public sealed class ShowGrabs : TogglableCommandBase
{
    public override string[] Aliases => ["showgrabs", "sg"];

    public override CommandTag Tag => CommandTag.World;

    public override string Description => "Shows grabbable surfaces.\nPass color like 'red', 'green' or '#RRGGBB' as argument to set custom color, or 'rgb' for rgb mode";

    public override bool CheatsOnly => true;

    public static Material HighlightMat;

    private GameObject animationObj;

    private const string rgb = "rgb";

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            if (this.Enabled)
            {
                this.SetupVisualizer(args);
            }
            else if (!this.Enabled)
            {
                this.OnExit();
            }
        };
    }

    private void SetupVisualizer(string[] args)
    {
        bool enableRgb = false;
        bool hasArg = args.Length > 0;
        if (hasArg)
        {
            enableRgb = args[0].EqualsIgnoreCase(rgb);
        }
        
        HighlightMat = new Material(Shader.Find("Hidden/Internal-Colored"));
        HighlightMat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry + 1;
        Color color = Color.green;
            
        if (hasArg && !enableRgb)
            ColorUtility.TryParseHtmlString(args[0], out color);

        HighlightMat.color = color;
        // disable weird bloom that happens in rare instances
        HighlightMat.DisableKeyword("_EMISSION");
        // show both sides
        HighlightMat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        // fix z-fighting caused by breathing textures, also makes handholds easier to see
        HighlightMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        HighlightMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        HighlightMat.SetInt("_ZWrite", 1);
        HighlightMat.SetFloat("_ZBias", -10.0f);

        if (animationObj == null && enableRgb)
        {
            this.animationObj = new GameObject("ShowGrabableAnimation");
            this.animationObj.AddComponent<HandholdRgb>();
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

