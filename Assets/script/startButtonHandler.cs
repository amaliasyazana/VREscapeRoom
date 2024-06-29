using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButtonHandler : MonoBehaviour
{
    public int gameSceneIndex = 1; // Index of your game scene in the Build Settings

    public void StartGame()
    {
        // Use the SceneTransitionManager to handle the scene transition
        SceneTransitionManager.singleton.GoToScene(gameSceneIndex);
    }
}
