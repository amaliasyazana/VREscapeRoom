using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;
using TMPro;

public class winTrigger : MonoBehaviour
{
    TimeCountdown time;
    public GameObject winText; // Assign this in the Inspector
    public GameObject restartButton; // Assign this in the Inspector

    void Start()
    {
        time = GameObject.Find("GameManager").GetComponent<TimeCountdown>();
        if (time == null)
        {
            Debug.LogError("TimeCountdown script not found on GameManager.");
        }

        if (winText == null)
        {
            Debug.LogError("Win text not assigned in the Inspector.");
        }

        if (restartButton == null)
        {
            Debug.LogError("Restart button not assigned in the Inspector.");
        }

        winText.SetActive(false); // Ensure win text is hidden at the start
        restartButton.SetActive(false); // Ensure restart button is hidden at the start
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);

        if (other.CompareTag("key"))
        {
            Debug.Log("Player detected.");

            if (time.currentTime > 0)
            {
                Debug.Log("Time remaining: " + time.currentTime);
                PlayerPrefs.SetInt("timetaken", (int)time.duration - (int)time.currentTime);
                Cursor.lockState = CursorLockMode.None;
                ShowWinText();
            }
            else
            {
                Debug.Log("Time is up.");
            }
        }
    }

    void ShowWinText()
    {
        Debug.Log("Showing win text.");
        winText.SetActive(true);
        restartButton.SetActive(true); // Show restart button simultaneously with win text
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("startScene");
    }
}
