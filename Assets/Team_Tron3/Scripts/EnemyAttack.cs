using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using FinalProject.CharacterControllers;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{


    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    public InputActionReference combat;

    public void OnTriggerEnter(Collider other)
    {
        if (player.CompareTag("Player"))
        {
            if (combat?.action != null)
            {
                if (combat.action.WasPressedThisFrame())
                {
                    combat.action.performed += OnCombatPerformed;
                }

            }
            
        }

    }
    public void OnCombatPerformed(InputAction.CallbackContext context)
    {
        Destroy(enemy);
    }
}