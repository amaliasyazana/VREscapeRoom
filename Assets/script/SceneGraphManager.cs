using UnityEngine;

public class SceneGraphManager : MonoBehaviour
{
    public GameObject specificParent; // Assign the specific parent GameObject in the Inspector (e.g., shelf)
    public GameObject[] specificChildren; // Assign the specific child GameObjects in the Inspector (e.g., candle)
    public Light[] pointLights; // Assign the point lights in the Inspector
    private UIController2 uiController;

    void Start()
    {
        uiController = FindObjectOfType<UIController2>();
        
        if (pointLights != null)
        {
            foreach (var light in pointLights)
            {
                light.enabled = false; // Ensure all lights are off at the start
            }
        }
    }

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

                    // Turn on the lights if the candle becomes a child of the shelf
                    if (child.name == "Candle" && pointLights != null)
                    {
                        SetLightsState(true);
                        Debug.Log("Candle lights enabled.");
                    }
                }
            }
            else
            {
                // If the child is currently a child of the parent, unparent it
                if (child.transform.parent == specificParent.transform)
                {
                    child.transform.SetParent(null);
                    Debug.Log($"Child '{child.name}' is now unparented from the parent.");

                    // Turn off the lights if the candle is unparented from the shelf
                    if (child.name == "Candle" && pointLights != null)
                    {
                        SetLightsState(false);
                        Debug.Log("Candle lights disabled.");
                    }
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

    // Function to set the state of all point lights
    private void SetLightsState(bool state)
    {
        foreach (var light in pointLights)
        {
            light.enabled = state;
        }

        // Update the light state text in the UI
        if (uiController != null)
        {
            uiController.RefreshLightStateText();
        }
    }
}
