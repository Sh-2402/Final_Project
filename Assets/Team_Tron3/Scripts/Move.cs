using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(platform, 0.01f);
        }
    }
    
}
