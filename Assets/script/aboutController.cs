using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class aboutController : MonoBehaviour
{
    // Start is called before the first frame update
  public void AbtBtn()
{
    Debug.Log("Start button clicked");
    SceneManager.LoadScene("aboutScene");
}

}
