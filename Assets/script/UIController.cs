using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public BoundingBoxVisualizer boundingBoxVisualizer;
    public TMP_Text boundingBoxStateText;

    public void OnToggleBoundingBoxButtonClicked()
    {
        boundingBoxVisualizer.ToggleBoundingBox();
        UpdateBoundingBoxStateText();
    }

    private void UpdateBoundingBoxStateText()
    {
        if (boundingBoxVisualizer != null)
        {
            bool isShowing = boundingBoxVisualizer.IsBoundingBoxVisible();
            boundingBoxStateText.text = $"Bounding Box: {(isShowing ? "On" : "Off")}";
        }
    }
}
