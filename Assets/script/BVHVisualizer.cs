using System.Collections.Generic;
using UnityEngine;

public class BVHVisualizer : MonoBehaviour
{
    public GameObject[] objects;
    public Color nodeColor = Color.green;
    public Color leafColor = Color.red;
    public bool showBVH = false;
    private BVHNode rootNode;
    private List<GameObject> lineRenderers = new List<GameObject>();

    void Start()
    {
        // Initial BVH build
        BuildBVH();
    }

    void Update()
    {
        if (showBVH)
        {
            // Clear existing bounding boxes
            ClearBVH();

            // Rebuild BVH and draw the bounding boxes
            BuildBVH();
            DrawBVH();
        }
        else
        {
            ClearBVH();
        }
    }

    private void BuildBVH()
    {
        rootNode = BVHBuilder.BuildBVH(objects);
        if (rootNode == null)
        {
            Debug.LogError("BVH construction failed: no objects to include in the BVH.");
        }
    }

    private void DrawBVH()
    {
        DrawBVHNode(rootNode);
    }

    private void DrawBVHNode(BVHNode node)
    {
        if (node == null) return;

        CreateBoundingBox(node.bounds, node.left == null && node.right == null ? leafColor : nodeColor);

        DrawBVHNode(node.left);
        DrawBVHNode(node.right);
    }

    private void CreateBoundingBox(Bounds bounds, Color color)
    {
        GameObject lineObject = new GameObject("BoundingBox");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = 16;
        lineRenderer.useWorldSpace = true;

        Vector3[] points = new Vector3[16];
        bounds.GetCornerPoints(points);
        lineRenderer.SetPositions(points);
        lineRenderer.loop = true;

        lineRenderers.Add(lineObject);
    }

    void ClearBVH()
    {
        foreach (var lr in lineRenderers)
        {
            if (lr != null)
            {
                Destroy(lr);
            }
        }
        lineRenderers.Clear();
    }

    public void ToggleBVH()
    {
        showBVH = !showBVH;
        Debug.Log($"BVH Visualization toggled to: {showBVH}");
    }
}

public static class BoundsExtensions
{
    public static void GetCornerPoints(this Bounds bounds, Vector3[] points)
    {
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        points[0] = new Vector3(min.x, min.y, min.z);
        points[1] = new Vector3(max.x, min.y, min.z);
        points[2] = new Vector3(max.x, min.y, max.z);
        points[3] = new Vector3(min.x, min.y, max.z);
        points[4] = new Vector3(min.x, max.y, min.z);
        points[5] = new Vector3(max.x, max.y, min.z);
        points[6] = new Vector3(max.x, max.y, max.z);
        points[7] = new Vector3(min.x, max.y, max.z);

        // Connecting the corners to form a bounding box
        points[8] = points[0];
        points[9] = points[4];
        points[10] = points[1];
        points[11] = points[5];
        points[12] = points[2];
        points[13] = points[6];
        points[14] = points[3];
        points[15] = points[7];
    }
}
