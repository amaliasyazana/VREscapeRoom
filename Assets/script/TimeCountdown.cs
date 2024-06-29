using System.Collections;
using UnityEngine;
using TMPro;

public class TimeCountdown : MonoBehaviour
{
    public TextMeshProUGUI timeText; // Assign this in the Inspector
    public GameObject loseText; // Assign this in the Inspector
    public float duration, currentTime;
    private string unit = "seconds";
    private bool gameRunning = true; // Flag to control the game state

    void Start()
    {
        currentTime = duration;
        loseText.SetActive(false); // Hide the lose text at the start
        UpdateTimeText();
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while (currentTime > 0 && gameRunning)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1;

            if (currentTime == 1)
                unit = "second";

            if (currentTime <= 10)
                timeText.color = new Color(1, 0, 0, 1f);

            UpdateTimeText();
        }

        // Time out -> show lose text
        if (currentTime <= 0 && gameRunning)
        {
            ShowLoseText();
        }
    }

    void UpdateTimeText()
    {
        timeText.text = "Time remaining: " + currentTime.ToString() + " " + unit;
    }

    void ShowLoseText()
    {
        loseText.SetActive(true);
    }

    public void StopTimer()
    {
        gameRunning = false;
    }
}
