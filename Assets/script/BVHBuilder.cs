using UnityEngine;
using System.Linq;

public static class BVHBuilder
{
    public static BVHNode BuildBVH(GameObject[] objects)
    {
        if (objects == null || objects.Length == 0)
            return null;

        // Compute the bounds for all objects
        Bounds bounds = ComputeBounds(objects);

        // Create a new BVH node
        BVHNode node = new BVHNode(bounds, objects);

        // If there's only one object, this is a leaf node
        if (objects.Length == 1)
            return node;

        // Split objects into two groups
        GameObject[] leftObjects;
        GameObject[] rightObjects;
        SplitObjects(objects, out leftObjects, out rightObjects);

        // Recursively build the left and right child nodes
        node.left = BuildBVH(leftObjects);
        node.right = BuildBVH(rightObjects);

        return node;
    }

    private static Bounds ComputeBounds(GameObject[] objects)
    {
        Bounds bounds = new Bounds(objects[0].transform.position, Vector3.zero);
        foreach (GameObject obj in objects)
        {
            bounds.Encapsulate(obj.GetComponent<Renderer>().bounds);
        }
        return bounds;
    }

    private static void SplitObjects(GameObject[] objects, out GameObject[] leftObjects, out GameObject[] rightObjects)
    {
        // Sort objects along the x-axis
        objects = objects.OrderBy(obj => obj.transform.position.x).ToArray();
        int mid = objects.Length / 2;

        leftObjects = objects.Take(mid).ToArray();
        rightObjects = objects.Skip(mid).ToArray();
    }
}
