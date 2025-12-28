using CommandsExtended.Behaviors;
using MoreCommands.Common;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CommandsExtended.Commands;

public sealed class Raycast : TogglableCommandBase
{
    public override string[] Aliases => ["raycast"];

    public override CommandTag Tag => CommandTag.World;

    public override string Description => "For dev purposes, determines name of game object at crosshair when pressing left click";

    public override bool CheatsOnly => true;

    public override Action<string[]> GetLogicCallback()
    {
        return args =>
        {
            GameObject listenerObj = new GameObject("RaycastListener");
            if (this.Enabled)
            {
                listenerObj.AddComponent<Raycaster>();
            }
            else
            {
                Object.Destroy(listenerObj);
            }
        };
    }
}

