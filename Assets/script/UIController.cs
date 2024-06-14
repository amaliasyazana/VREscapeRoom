using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public BoundingShapeVisualizer boundingShapeVisualizer;
    public TMP_Text boundingShapeStateText;

    public void OnToggleBoundingShapeButtonClicked()
    {
        boundingShapeVisualizer.ToggleBoundingShape();
        UpdateBoundingShapeStateText();
    }

    private void UpdateBoundingShapeStateText()
    {
        if (boundingShapeVisualizer != null)
        {
            bool isShowing = boundingShapeVisualizer.IsBoundingShapeVisible();
            boundingShapeStateText.text = $"Bounding Shape: {(isShowing ? "On" : "Off")}";
        }
    }
}
