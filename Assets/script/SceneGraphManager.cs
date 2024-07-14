using UnityEngine;
using System.Collections.Generic;

public class SceneGraphManager : MonoBehaviour
{
    public GameObject[] parents;  // Array of parent objects to manage

    private Dictionary<Transform, List<Transform>> parentChildMap;

    void Start()
    {
        parentChildMap = new Dictionary<Transform, List<Transform>>();

        foreach (var parent in parents)
        {
            InitializeParent(parent.transform);
        }
    }

    void InitializeParent(Transform parent)
    {
        if (!parentChildMap.ContainsKey(parent))
        {
            parentChildMap[parent] = new List<Transform>();
        }

        foreach (Transform child in parent)
        {
            if (!parentChildMap[parent].Contains(child))
            {
                parentChildMap[parent].Add(child);
            }
        }
    }

    void Update()
    {
        foreach (var parent in parents)
        {
            Transform parentTransform = parent.transform;
            Bounds parentBounds = GetBounds(parent);

            // Check for new children added to the parent
            foreach (Transform child in parentTransform)
            {
                if (!parentChildMap[parentTransform].Contains(child))
                {
                    parentChildMap[parentTransform].Add(child);
                    Debug.Log($"{child.name} added to {parent.name}.");
                }
            }

            // Check if any child moved out of the parent's bounds
            List<Transform> childrenToRemove = new List<Transform>();
            foreach (var child in parentChildMap[parentTransform])
            {
                if (child != null && !parentBounds.Contains(child.position))
                {
                    HandleOutOfBounds(child, parent);
                    childrenToRemove.Add(child);
                }
            }

            // Remove children that have been reparented
            foreach (var child in childrenToRemove)
            {
                parentChildMap[parentTransform].Remove(child);
            }
        }
    }

    Bounds GetBounds(GameObject obj)
    {
        Collider collider = obj.GetComponent<Collider>();
        if (collider != null)
        {
            return collider.bounds;
        }
        return new Bounds(obj.transform.position, Vector3.zero);
    }

    void HandleOutOfBounds(Transform child, GameObject currentParent)
    {
        // Detach the child from the current parent
        Debug.Log($"{child.name} moved out of {currentParent.name}'s bounds.");
        child.SetParent(null);

        // Optionally, find a new parent for the child
        FindNewParent(child);
    }

    void FindNewParent(Transform child)
    {
        foreach (var parent in parents)
        {
            Bounds parentBounds = GetBounds(parent);
            if (parentBounds.Contains(child.position))
            {
                // Set the child to the new parent
                child.SetParent(parent.transform);

                // Adjust the local position to ensure it remains within bounds
                child.localPosition = parent.transform.InverseTransformPoint(child.position);

                // Update the parent-child map
                parentChildMap[parent.transform].Add(child);

                Debug.Log($"{child.name} is now a child of {parent.name}.");
                return;
            }
        }

        Debug.Log($"{child.name} has no suitable new parent and remains unparented.");
    }
}
