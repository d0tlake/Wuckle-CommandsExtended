using UnityEngine;

namespace CommandsExtended.Behaviors;

public sealed class Raycaster : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DoRaycast();
        }
    }
    private void DoRaycast()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Plugin.Logger.LogInfo($"Hit Object: {hit.collider.gameObject.name}");
        }
    }
}
