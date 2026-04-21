using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FinalProject.CharacterControllers
{
    
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float MovementBlendSpeed = 0.02f;
        private PlayerMovement _playerMovement;

        private static int inputXHash = Animator.StringToHash("inputX");
       
       private Vector3 _currentBlendInput = Vector3.zero;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            UpdateAnimationState();
        }

       

     
        private void UpdateAnimationState()
        {
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, _playerMovement.move.action.ReadValue<Vector3>(), MovementBlendSpeed * Time.deltaTime);
            _animator.SetFloat(inputXHash, _playerMovement.move.action.ReadValue<Vector3>().x);
            
        }
    }
}
    
