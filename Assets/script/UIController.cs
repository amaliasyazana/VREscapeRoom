using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private BoundingBoxVisualizer[] boundingBoxVisualizers;
    public TMP_Text boundingBoxStateText;

    void Start()
    {
        // Find all objects with BoundingBoxVisualizer
        boundingBoxVisualizers = FindObjectsOfType<BoundingBoxVisualizer>();
        UpdateBoundingBoxStateText();
    }

    public void OnToggleBoundingBoxButtonClicked()
    {
        foreach (var visualizer in boundingBoxVisualizers)
        {
            visualizer.ToggleBoundingBox();
        }
        UpdateBoundingBoxStateText();
    }

    private void UpdateBoundingBoxStateText()
    {
        if (boundingBoxVisualizers.Length > 0)
        {
            bool isShowing = boundingBoxVisualizers[0].IsBoundingBoxVisible();
            boundingBoxStateText.text = $"Bounding Box: {(isShowing ? "On" : "Off")}";
        }
    }
}
