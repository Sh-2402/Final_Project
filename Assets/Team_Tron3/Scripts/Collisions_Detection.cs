using System.Collections;
using FinalProject.CharacterControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FinalProject.CharacterControllers
{
    public class Collisions_Detection : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] Animator Anim;
        [SerializeField] GameObject fadeOut;
        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(CollisionEnd());
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

}
