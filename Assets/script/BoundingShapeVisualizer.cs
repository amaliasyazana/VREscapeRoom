using System.Collections.Generic;
using UnityEngine;

public class BoundingShapeVisualizer : MonoBehaviour
{
    public Color lineColor = Color.green;
    private bool showBoundingShape = false;
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
        lineRenderer.loop = false;
        lineRenderer.enabled = false; // Start with the LineRenderer disabled
    }

    void Update()
    {
        if (showBoundingShape)
        {
            DrawBoundingShape();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public void ToggleBoundingShape()
    {
        showBoundingShape = !showBoundingShape;
        lineRenderer.enabled = showBoundingShape;
    }

    private void DrawBoundingShape()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.mesh;
            if (mesh == null)
                return;

            List<Vector3> vertices = new List<Vector3>();
            List<int> edges = new List<int>();
            Dictionary<string, int> edgeDict = new Dictionary<string, int>();

            // Collect all edges in the mesh
            for (int subMeshIndex = 0; subMeshIndex < mesh.subMeshCount; subMeshIndex++)
            {
                int[] indices = mesh.GetIndices(subMeshIndex);
                for (int i = 0; i < indices.Length; i += 3)
                {
                    int[] triangle = new int[] { indices[i], indices[i + 1], indices[i + 2] };
                    for (int edgeIndex = 0; edgeIndex < 3; edgeIndex++)
                    {
                        int vertex1 = triangle[edgeIndex];
                        int vertex2 = triangle[(edgeIndex + 1) % 3];
                        string edgeKey = vertex1 < vertex2 ? $"{vertex1}-{vertex2}" : $"{vertex2}-{vertex1}";

                        if (edgeDict.ContainsKey(edgeKey))
                        {
                            // Edge is already in dictionary
                            edgeDict[edgeKey]++;
                        }
                        else
                        {
                            // New edge
                            edgeDict[edgeKey] = 1;
                        }
                    }
                }
            }

            // Filter out internal edges
            foreach (var kvp in edgeDict)
            {
                if (kvp.Value == 1)
                {
                    string[] verticesIndex = kvp.Key.Split('-');
                    int v1 = int.Parse(verticesIndex[0]);
                    int v2 = int.Parse(verticesIndex[1]);
                    edges.Add(v1);
                    edges.Add(v2);
                }
            }

            // Convert vertices to world space
            Vector3[] meshVertices = mesh.vertices;
            for (int i = 0; i < meshVertices.Length; i++)
            {
                vertices.Add(transform.TransformPoint(meshVertices[i]));
            }

            // Set LineRenderer positions
            lineRenderer.positionCount = edges.Count;
            Vector3[] linePositions = new Vector3[edges.Count];
            for (int i = 0; i < edges.Count; i++)
            {
                linePositions[i] = vertices[edges[i]];
            }
            lineRenderer.SetPositions(linePositions);

            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;
        }
    }

    public bool IsBoundingShapeVisible()
    {
        return showBoundingShape;
    }
}
