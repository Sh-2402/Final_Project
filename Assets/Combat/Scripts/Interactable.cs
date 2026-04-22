using UnityEngine;

public class Interactable : MonoBehaviour
{
    Transform player;
    public float radius = 3f;

     protected virtual void Start()
    {
        player = PlayerManager.instance.player.transform;
    }

    public virtual void Interact()
    {
        Debug.Log("INTERACT");
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= radius && Input.GetMouseButtonDown(0))
        {
            Debug.Log(gameObject.name + " is in range");
            Interact();

        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
