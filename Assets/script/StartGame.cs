using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameManager gameManager; // Assign this in the Inspector
    private bool isHovering = false;

    public void StartTheGame()
    {
        // Hide the Start Button
        gameObject.SetActive(false); // Hide the start button

        // Call the initialization method on the GameManager
        gameManager.InitializeGame();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isHovering)
        {
            StartTheGame();
        }
    }
}




