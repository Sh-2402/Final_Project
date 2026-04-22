using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalLoadScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           SceneManager.LoadScene("IslandTutorial");
        }
    }
}
