using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartTheGame()
    {
        // Hide the Start Button
        gameObject.SetActive(false);

        // Call any initialization code here
        // For example, enabling the player's movement, starting a timer, etc.
    }
}



