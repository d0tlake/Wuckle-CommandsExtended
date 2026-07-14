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

    public override string Description =>
        "Shows grabbable surfaces\n" +
        "Pass color like 'red', 'green' or '#RRGGBB' as argument to set custom color, or 'rgb' for rgb mode\n" +
        "Pass a number after 'rgb' to set the rgb speed.";

    public override bool EnablesCheatsOnUse => true;

    public static Material HighlightMat;

    private static readonly int Cull = Shader.PropertyToID("_Cull");
    private static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");
    private static readonly int DstBlend = Shader.PropertyToID("_DstBlend");
    private static readonly int ZWrite = Shader.PropertyToID("_ZWrite");
    private static readonly int ZBias = Shader.PropertyToID("_ZBias");

    private GameObject animationObj;

    private const string RGB = "rgb";

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
            enableRgb = args[0].EqualsIgnoreCase(RGB);
        }

        HighlightMat = new Material(Shader.Find("Hidden/Internal-Colored"))
        {
            renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry + 1,
        };
        Color color = Color.green;

        if (hasArg && !enableRgb)
            ColorUtility.TryParseHtmlString(args[0], out color);

        HighlightMat.color = color;
        // disable weird bloom that happens in rare instances
        HighlightMat.DisableKeyword("_EMISSION");
        // show both sides
        HighlightMat.SetInt(Cull, (int)UnityEngine.Rendering.CullMode.Off);
        // fix z-fighting caused by breathing textures, also makes handholds easier to see
        HighlightMat.SetInt(SrcBlend, (int)UnityEngine.Rendering.BlendMode.One);
        HighlightMat.SetInt(DstBlend, (int)UnityEngine.Rendering.BlendMode.Zero);
        HighlightMat.SetInt(ZWrite, 1);
        HighlightMat.SetFloat(ZBias, -10.0f);

        if (this.animationObj == null && enableRgb)
        {
            hasArg = args.Length > 1;
            HandholdRgb.Speed = HandholdRgb.DefaultSpeed;
            if (hasArg && float.TryParse(args[1], out float speed))
                HandholdRgb.Speed = speed;
            this.animationObj = new GameObject("ShowGrabbableAnimation");
            this.animationObj.AddComponent<HandholdRgb>();
        }

        CL_Handhold[] allHandholds = Resources.FindObjectsOfTypeAll<CL_Handhold>();

        foreach (CL_Handhold handhold in allHandholds)
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

        foreach (CL_Handhold handhold in allHandholds)
        {
            GameObject go = handhold.gameObject;

            bool hasVisualizer = go.TryGetComponent(out HandholdVisualizer visualizer);
            if (!hasVisualizer)
            {
                continue;
            }

            visualizer.HideHandholds();
            Object.Destroy(visualizer);
        }

        this.Enabled = false;
    }
}

