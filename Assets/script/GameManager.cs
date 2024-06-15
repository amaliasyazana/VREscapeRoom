using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalKeys = 3; // Total number of keys to be found
    private int keysFound = 0;
    public GameObject winText; // UI Text to show when the player wins

    void Start()
    {
        winText.SetActive(false); // Hide the win text at the start
    }

    public void KeyFound()
    {
        keysFound++;
        if (keysFound >= totalKeys)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        // Show the win text
        winText.SetActive(true);

        // Add any additional win logic here (e.g., stopping the game, showing a menu, etc.)
    }
}

