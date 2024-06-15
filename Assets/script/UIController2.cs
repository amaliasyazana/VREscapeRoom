using UnityEngine;
using TMPro;

public class UIController2 : MonoBehaviour
{
    private BVHVisualizer bvhVisualizer;
    public TMP_Text bvhStateText;

    void Start()
    {
        bvhVisualizer = FindObjectOfType<BVHVisualizer>();
        if (bvhVisualizer == null)
        {
            Debug.LogError("No BVHVisualizer found in the scene.");
        }
        UpdateBVHStateText();
    }

    public void OnToggleBVHButtonClicked()
    {
        if (bvhVisualizer != null)
        {
            bvhVisualizer.ToggleBVH();
            UpdateBVHStateText();
        }
    }

    private void UpdateBVHStateText()
    {
        if (bvhVisualizer != null)
        {
            bvhStateText.text = $"BVH Visualization: {(bvhVisualizer.showBVH ? "On" : "Off")}";
        }
    }
}
