using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxVisualizer : MonoBehaviour
{
    public Color boxColor = Color.green;
    private bool showBoundingBox = false;
    private LineRenderer lineRenderer;

    void Start()
    {
        // Add LineRenderer component if it doesn't exist
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Setup LineRenderer properties
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 16; // Number of positions needed to draw the box
        lineRenderer.loop = false;
        lineRenderer.enabled = false; // Start with the LineRenderer disabled
    }

    void Update()
    {
        if (showBoundingBox)
        {
            DrawBoundingBox();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public void ToggleBoundingBox()
    {
        showBoundingBox = !showBoundingBox;
        lineRenderer.enabled = showBoundingBox;
    }

    private void DrawBoundingBox()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Bounds bounds = renderer.bounds;

            Vector3[] corners = new Vector3[8];
            corners[0] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
            corners[1] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
            corners[2] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z);
            corners[3] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z);

            corners[4] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z);
            corners[5] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z);
            corners[6] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);
            corners[7] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z);

            lineRenderer.positionCount = 16;

            // Set positions to draw the bounding box lines
            lineRenderer.SetPositions(new Vector3[]
            {
                corners[0], corners[1], corners[5], corners[4], corners[0], // Bottom face
                corners[3], corners[2], corners[6], corners[7], corners[3], // Top face
                corners[4], corners[7], // Vertical lines
                corners[5], corners[6],
                corners[1], corners[2]
            });

            lineRenderer.startColor = boxColor;
            lineRenderer.endColor = boxColor;
        }
    }

    public bool IsBoundingBoxVisible()
    {
        return showBoundingBox;
    }
}
