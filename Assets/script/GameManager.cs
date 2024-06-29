using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winText; // Assign this in the Inspector
    public GameObject door; // Assign this in the Inspector
    public AudioClip keyPickupSound; // Assign this in the Inspector
    public AudioClip doorOpenSound; // Assign this in the Inspector

    private bool gameWon = false;
    private int keysCollected = 0;
    private int requiredKeys = 2; // Adjust this to require more keys to win

    public void InitializeGame()
    {
        // Add initialization code here, e.g., enabling player controls
        Debug.Log("Game Started");
        winText.SetActive(false); // Ensure win text is hidden at the start
        door.SetActive(false); // Ensure door is closed at the start
    }

    public void KeyFound()
    {
        keysCollected++;
        Debug.Log("Key found! (" + keysCollected + "/" + requiredKeys + ")");
        AudioSource.PlayClipAtPoint(keyPickupSound, transform.position);

        if (keysCollected >= requiredKeys)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        gameWon = true;
        winText.SetActive(true);
        door.SetActive(true);
        AudioSource.PlayClipAtPoint(doorOpenSound, transform.position);
        Debug.Log("You Win!");
    }
}