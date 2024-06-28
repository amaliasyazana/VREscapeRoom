using UnityEngine;

public class ScenegraphVisualizer : MonoBehaviour {
    public Transform RootNode; // The root node of the hierarchy to visualize
    public bool showScenegraph = true;

    private void OnDrawGizmos() {
        if (RootNode != null && showScenegraph) {
            DrawScenegraphNode(RootNode);
        }
    }

    private void DrawScenegraphNode(Transform node) {
        Gizmos.color = Color.blue; // Color for scenegraph nodes
        Gizmos.DrawSphere(node.position, 0.1f); // Draw a sphere at the node position

        foreach (Transform child in node) {
            Gizmos.DrawLine(node.position, child.position); // Draw a line to each child
            DrawScenegraphNode(child); // Recursively draw each child node
        }
    }
}
