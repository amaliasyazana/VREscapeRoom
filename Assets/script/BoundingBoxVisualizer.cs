using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBoxVisualizer : MonoBehaviour
{
    public Color boxColor = Color.green;
    public Color combinedBoxColor = Color.red;
    private bool showBoundingBox = false;
    private LineRenderer lineRenderer;
    private LineRenderer combinedLineRenderer;
    public bool isParent;

    void Start()
    {
        // Add LineRenderer component if it doesn't exist
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Setup LineRenderer properties for individual bounding box
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 16; // Number of positions needed to draw the box
        lineRenderer.loop = false;
        lineRenderer.enabled = false; // Start with the LineRenderer disabled

        // Add a second LineRenderer for combined bounding box if this is the parent
        if (isParent)
        {
            combinedLineRenderer = gameObject.AddComponent<LineRenderer>();
            combinedLineRenderer.startWidth = 0.05f;
            combinedLineRenderer.endWidth = 0.05f;
            combinedLineRenderer.positionCount = 16;
            combinedLineRenderer.loop = false;
            combinedLineRenderer.enabled = false;
        }
    }

    void Update()
    {
        if (showBoundingBox)
        {
            DrawBoundingBox();
            if (isParent)
            {
                DrawCombinedBoundingBox();
            }
        }
        else
        {
            lineRenderer.enabled = false;
            if (isParent && combinedLineRenderer != null)
            {
                combinedLineRenderer.enabled = false;
            }
        }
    }

    public void ToggleBoundingBox()
    {
        showBoundingBox = !showBoundingBox;
        lineRenderer.enabled = showBoundingBox;
        if (isParent && combinedLineRenderer != null)
        {
            combinedLineRenderer.enabled = showBoundingBox;
        }
    }

    private void DrawBoundingBox()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Bounds bounds = renderer.bounds;

            Vector3[] corners = GetBoundingBoxCorners(bounds);

            lineRenderer.positionCount = 16;

            // Set positions to draw the bounding box lines
            lineRenderer.SetPositions(GetBoundingBoxLines(corners));

            lineRenderer.startColor = boxColor;
            lineRenderer.endColor = boxColor;
        }
    }

    private void DrawCombinedBoundingBox()
    {
        Bounds combinedBounds = CalculateCombinedBounds();

        Vector3[] corners = GetBoundingBoxCorners(combinedBounds);

        combinedLineRenderer.positionCount = 16;

        // Set positions to draw the bounding box lines
        combinedLineRenderer.SetPositions(GetBoundingBoxLines(corners));

        combinedLineRenderer.startColor = combinedBoxColor;
        combinedLineRenderer.endColor = combinedBoxColor;
    }

    private Bounds CalculateCombinedBounds()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return new Bounds();

        Bounds combinedBounds = renderers[0].bounds;
        foreach (Renderer renderer in renderers)
        {
            combinedBounds.Encapsulate(renderer.bounds);
        }

        return combinedBounds;
    }

    private Vector3[] GetBoundingBoxCorners(Bounds bounds)
    {
        Vector3[] corners = new Vector3[8];
        corners[0] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
        corners[1] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
        corners[2] = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z);
        corners[3] = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z);

        corners[4] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z);
        corners[5] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z);
        corners[6] = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);
        corners[7] = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z);

        return corners;
    }

    private Vector3[] GetBoundingBoxLines(Vector3[] corners)
    {
        return new Vector3[]
        {
            corners[0], corners[1], corners[5], corners[4], corners[0], // Bottom face
            corners[3], corners[2], corners[6], corners[7], corners[3], // Top face
            corners[4], corners[7], // Vertical lines
            corners[5], corners[6],
            corners[1], corners[2]
        };
    }

    public bool IsBoundingBoxVisible()
    {
        return showBoundingBox;
    }
}
