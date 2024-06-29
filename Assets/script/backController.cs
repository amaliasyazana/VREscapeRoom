using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backController : MonoBehaviour
{
    // Start is called before the first frame update
  public void BackBtn()
{
    Debug.Log("Start button clicked");
    SceneManager.LoadScene("startScene");
}

}
