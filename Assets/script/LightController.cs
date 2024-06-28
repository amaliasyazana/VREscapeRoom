using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light[] lights; // Array to hold found Light components

    void Start()
    {
        // Find all Light components in the scene
        lights = FindObjectsOfType<Light>();

        if (lights == null || lights.Length == 0)
        {
            Debug.LogWarning("No Light components found in the scene.");
        }
    }

    void Update()
    {
        // Example: Toggle all lights on/off using a key press (e.g., spacebar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLights();
        }
    }

    void ToggleLights()
    {
        // Toggle the enabled state of all found Light components
        foreach (var light in lights)
        {
            light.enabled = !light.enabled;
        }

        // Log the current state to the console
        Debug.Log("Lights toggled");
    }
}
