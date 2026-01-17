using CommandsExtended.Commands;
using UnityEngine;

namespace CommandsExtended.Behaviors;

public sealed class HandholdVisualizer : MonoBehaviour
{
    Material[] existingMaterials;
    Renderer currentRenderer;
    bool wasEnabled = false;

    public void Start()
    {
        bool hasHandhold = TryGetComponent(out CL_Handhold handhold);

        if (hasHandhold)
        {
            RevealHandholds(handhold);
        }
    }

    public void HideHandholds()
    {
        if (currentRenderer != null)
        {
            this.currentRenderer.enabled = this.wasEnabled;
            this.currentRenderer.sharedMaterials = [.. this.existingMaterials];
        }
    }

    private void RevealHandholds(CL_Handhold handhold)
    {
        // not all handholds are treated equal...
        if (handhold.handholdRenderer != null)
        {
            this.RevealRenderer(handhold);
        }
        else if (handhold.TryGetComponent(out MeshFilter filter))
        {
            this.RevealExistingMesh(handhold, filter);
        }
        else if (handhold.TryGetComponent(out MeshCollider collider))
        {
            this.MatchMeshRendererToCollider(handhold, collider);
        }
        else if (handhold.TryGetComponent(out BoxCollider box))
        {
            this.VisualizeBoxCollider(box);
        }
        else if (handhold.TryGetComponent(out SphereCollider sphere))
        {
            this.VisualizeSphereCollider(sphere);
        }
        else
        {
            Plugin.Logger.LogInfo($"Unable to draw handhold: {handhold.name}");
        }
    }

    private void RevealRenderer(CL_Handhold handhold)
    {
        this.TrackExisting(handhold.handholdRenderer, handhold.handholdRenderer.enabled);
        handhold.handholdRenderer.enabled = true;
        handhold.handholdRenderer.sharedMaterials = [ShowGrabs.HighlightMat];
    }

    private void RevealExistingMesh(CL_Handhold handhold, MeshFilter filter)
    {
        GameObject obj = handhold.gameObject;

        if (filter != null && filter.sharedMesh != null)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            bool wasEnabled = false;
            if (renderer == null)
            {
                renderer = obj.AddComponent<MeshRenderer>();
            }
            else
            {
                wasEnabled = renderer.enabled;
            }

            this.TrackExisting(renderer, wasEnabled);
            renderer.sharedMaterials = [ShowGrabs.HighlightMat];
            renderer.enabled = wasEnabled;
        }
    }

    private void MatchMeshRendererToCollider(CL_Handhold handhold, MeshCollider meshCol)
    {
        GameObject obj = handhold.gameObject;

        MeshFilter filter = obj.GetComponent<MeshFilter>();
        if (filter == null) filter = obj.AddComponent<MeshFilter>();

        filter.sharedMesh = meshCol.sharedMesh;

        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        bool wasEnabled = false;
        if (renderer == null)
        {
            renderer = obj.AddComponent<MeshRenderer>();
        }
        else
        {
            wasEnabled = renderer.enabled;
        }

        this.TrackExisting(renderer, wasEnabled);
        renderer.sharedMaterials = [ShowGrabs.HighlightMat];
        renderer.enabled = true;
    }

    private void VisualizeSphereCollider(SphereCollider sphere)
    {
        GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        Object.Destroy(visual.GetComponent<SphereCollider>());

        visual.transform.SetParent(sphere.transform);
        visual.transform.localPosition = sphere.center;
        visual.transform.localRotation = Quaternion.identity;

        float diameter = sphere.radius * 2f;
        visual.transform.localScale = new Vector3(diameter, diameter, diameter);

        var renderer = visual.GetComponent<MeshRenderer>();
        this.TrackExisting(renderer);
        renderer.sharedMaterials = [ShowGrabs.HighlightMat];
    }

    private void VisualizeBoxCollider(BoxCollider box)
    {
        GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Destroy(visual.GetComponent<Collider>());

        visual.transform.SetParent(box.transform);
        visual.transform.localPosition = box.center;
        visual.transform.localScale = box.size;
        visual.transform.localRotation = Quaternion.identity;

        var renderer = visual.GetComponent<Renderer>();
        this.TrackExisting(renderer);
        renderer.sharedMaterials = [ShowGrabs.HighlightMat];
    }

    private void TrackExisting(Renderer renderer, bool wasEnabled = false)
    {
        this.wasEnabled = wasEnabled;
        this.currentRenderer ??= renderer;
        this.existingMaterials ??= [.. renderer.sharedMaterials];
    }
}
