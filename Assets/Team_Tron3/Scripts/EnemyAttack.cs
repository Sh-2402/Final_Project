using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using FinalProject.CharacterControllers;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{


    [SerializeField] GameObject player;
    [SerializeField] Animator Anim;
    [SerializeField] GameObject fadeOut;
    [SerializeField] GameObject enemy;
    public InputActionReference combat;

    private void OnTriggerEnter(Collider other)
    {
        if (player.CompareTag("Player"))
        {
            if (combat?.action != null)
            {
                if (combat.action.triggered)
                {
                    Destroy(enemy);
                }
                else
                {
                    StartCoroutine(CollisionEnd());
                }
            }
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