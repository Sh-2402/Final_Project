using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] GameObject Object;
    [SerializeField] GameObject rock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Object.GetComponent<MeshRenderer>().enabled = true;
            Object.GetComponent<Animator>().enabled = true;
            
        }
    }
}
