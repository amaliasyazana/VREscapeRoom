using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController2 : MonoBehaviour
{
    private BVHVisualizer bvhVisualizer;
    public TMP_Text bvhStateText;
    public TMP_Text lightStateText; // Text to display light state
    private Light[] lights; // Array to hold found Light components

    void Start()
    {
        bvhVisualizer = FindObjectOfType<BVHVisualizer>();

        if (bvhVisualizer == null)
        {
            Debug.LogError("No BVHVisualizer found in the scene.");
        }

        // Find all Light components in the scene
        lights = FindObjectsOfType<Light>();

        if (lights == null || lights.Length == 0)
        {
            Debug.LogWarning("No Light components found in the scene.");
        }
        else
        {
            // Turn off all lights at the start
            foreach (var light in lights)
            {
                light.enabled = false;
            }
        }

        UpdateBVHStateText();
        UpdateLightStateText();
    }

    public void OnToggleBVHButtonClicked()
    {
        if (bvhVisualizer != null)
        {
            bvhVisualizer.showBVH = !bvhVisualizer.showBVH;
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

    private void UpdateLightStateText()
    {
        if (lights != null && lightStateText != null)
        {
            bool anyLightOn = false;
            foreach (var light in lights)
            {
                if (light.enabled)
                {
                    anyLightOn = true;
                    break;
                }
            }
            lightStateText.text = $"Scenegraph: {(anyLightOn ? "On" : "Off")}";
        }
    }

    // Method to update light state text externally
    public void RefreshLightStateText()
    {
        UpdateLightStateText();
    }
}
