using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText; // Assign this in the Inspector
    public GameObject winText; // Assign this in the Inspector
    public GameObject loseText; // Assign this in the Inspector
    public float gameTime = 60f; // Total time in seconds

    private bool gameRunning = false;
    private float currentTime;

    void Start()
    {
        InitializeGame();
    }

    public void InitializeGame()
    {
        currentTime = gameTime;
        winText.SetActive(false);
        loseText.SetActive(false);
        gameRunning = true;
        StartCoroutine(GameTimer());
    }

    IEnumerator GameTimer()
    {
        while (gameRunning && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
            yield return null;
        }

        if (currentTime <= 0)
        {
            LoseGame();
        }
    }

    void UpdateTimerText()
{
    Debug.Log($"Updating Timer: {currentTime:F2}");
    timerText.text = $"Time: {currentTime:F2}";
}

public void FindKey()
{
    Debug.Log("Key found!");
    if (gameRunning)
    {
        WinGame();
    }
}


    void WinGame()
    {
        gameRunning = false;
        winText.SetActive(true);
    }

    void LoseGame()
    {
        gameRunning = false;
        loseText.SetActive(true);
    }
}
