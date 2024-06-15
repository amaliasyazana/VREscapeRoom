using UnityEngine;

public class BVHNode
{
    public Bounds bounds;
    public BVHNode left;
    public BVHNode right;
    public GameObject[] objects;

    public BVHNode(Bounds bounds, GameObject[] objects)
    {
        this.bounds = bounds;
        this.objects = objects;
    }
}
