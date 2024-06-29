using UnityEngine;

public class winTrigger : MonoBehaviour
{
    TimeCountdown time;
    public GameObject winText; // Assign this in the Inspector

    void Start()
    {
        time = GameObject.Find("GameManager").GetComponent<TimeCountdown>();
        winText.SetActive(false); // Ensure win text is hidden at the start
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            if (time.currentTime > 0)
            {
                PlayerPrefs.SetInt("timetaken", (int)time.duration - (int)time.currentTime);
                Cursor.lockState = CursorLockMode.None;
                ShowWinText();
            }
        }
    }

    void ShowWinText()
    {
        winText.SetActive(true);
    }
}
