using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameManager gameManager; // Assign this in the Inspector

    public void StartTheGame()
    {
        // Hide the Start Button
        gameObject.SetActive(false); // Hide the start button

        // Call the initialization method on the GameManager
        gameManager.InitializeGame();
    }
}



