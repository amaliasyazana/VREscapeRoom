using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine;

public class winTrigger : MonoBehaviour
{
    TimeCountdown time;
    public GameObject winText; // Assign this in the Inspector

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

        winText.SetActive(false); // Ensure win text is hidden at the start
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
                StartCoroutine(LoadStartSceneAfterDelay(10f)); // Start coroutine to load start scene after 15 seconds
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
    }

    IEnumerator LoadStartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("startScene");
    }
}
