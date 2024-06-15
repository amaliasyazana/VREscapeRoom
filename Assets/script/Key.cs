using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand")) // Assuming the hand or controller has the tag "Hand"
        {
            gameManager.KeyFound();
            Destroy(gameObject); // Remove the key from the scene
        }
    }
}

