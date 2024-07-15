using UnityEngine;

public class SceneGraphManager : MonoBehaviour
{
    public GameObject specificParent; // Assign the specific parent GameObject in the Inspector
    public GameObject[] specificChildren; // Assign the specific child GameObjects in the Inspector

    void Update()
    {
        if (specificParent == null || specificChildren == null || specificChildren.Length == 0)
        {
            Debug.LogError("Specific parent or children not assigned!");
            return;
        }

        // Get the parent's mesh bounds
        Bounds parentBounds = GetMeshColliderBounds(specificParent);

        foreach (GameObject child in specificChildren)
        {
            if (child == null)
            {
                Debug.LogError("One of the specific children is not assigned!");
                continue;
            }

            // Check if the child's position is inside the parent's bounding box
            if (parentBounds.Contains(child.transform.position))
            {
                // If the child is not already a child of the parent, make it a child
                if (child.transform.parent != specificParent.transform)
                {
                    child.transform.SetParent(specificParent.transform);
                    Debug.Log($"Child '{child.name}' is now parented to the parent.");
                }
            }
            else
            {
                // If the child is currently a child of the parent, unparent it
                if (child.transform.parent == specificParent.transform)
                {
                    child.transform.SetParent(null);
                    Debug.Log($"Child '{child.name}' is now unparented from the parent.");
                }
            }
        }
    }

    // Function to get the mesh bounds of the object with a MeshCollider
    private Bounds GetMeshColliderBounds(GameObject obj)
    {
        MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
        if (meshCollider != null && meshCollider.sharedMesh != null)
        {
            Bounds bounds = meshCollider.bounds;
            return bounds;
        }
        else
        {
            Debug.LogError("Object must have a MeshCollider component with a valid mesh to calculate bounds.");
            // Fallback to transform position with zero size bounds
            return new Bounds(obj.transform.position, Vector3.zero);
        }
    }
}
