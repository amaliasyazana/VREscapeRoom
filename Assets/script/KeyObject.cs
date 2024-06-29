using UnityEngine;

public class KeyObject : MonoBehaviour
{
void OnTriggerEnter(Collider other)
{
    Debug.Log("Trigger entered");
    if (other.CompareTag("Player"))
    {
        Debug.Log("Player found key");
        FindObjectOfType<GameManager>().FindKey();
        gameObject.SetActive(false); // Hide the key once found
    }
}

}
