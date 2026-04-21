using UnityEngine;

public class MerchantInteraction : MonoBehaviour
{
    public GameObject interactPrompt;
    public GameObject shopScreen;

    private bool playerInRange = false;

    void Start()
    {
        if (interactPrompt != null)
            interactPrompt.SetActive(false);

        if (shopScreen != null)
            shopScreen.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (shopScreen != null)
                shopScreen.SetActive(true);

            if (interactPrompt != null)
                interactPrompt.SetActive(false);
        }

        if (shopScreen != null && shopScreen.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            shopScreen.SetActive(false);

            if (playerInRange && interactPrompt != null)
                interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (interactPrompt != null)
                interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (interactPrompt != null)
                interactPrompt.SetActive(false);
        }
    }
}
