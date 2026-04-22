using UnityEngine;
using UnityEngine.SceneManagement;
using FinalProject.CharacterControllers;
using System.Collections;

public class PlayerEnemyCollsion : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Animator Anim;
    [SerializeField] GameObject fadeOut;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(CollisionEnd());
        }
    }
    IEnumerator CollisionEnd()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        Anim.GetComponent<Animator>().Play("Death");
        yield return new WaitForSeconds(2f);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
