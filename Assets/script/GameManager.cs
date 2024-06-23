using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winText; // Assign this in the Inspector
    private bool gameWon = false;

    public void InitializeGame()
    {
        // Add initialization code here, e.g., enabling player controls
        Debug.Log("Game Started");
        winText.SetActive(false); // Ensure win text is hidden at the start
    }

    public void KeyFound()
    {
        if (!gameWon)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        gameWon = true;
        winText.SetActive(true);
        Debug.Log("You Win!");
    }
}

