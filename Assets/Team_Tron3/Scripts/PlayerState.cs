using System.Collections;
using System.Collections.Generic;  
using UnityEngine;

namespace FinalProject.CharacterControllers
{
    public class PlayerState : MonoBehaviour
    {
        [field: SerializeField] public PlayerMovementState CurrentPlayerMovementState { get; protected set; } = PlayerMovementState.Idle;
        
    }
    public enum PlayerMovementState
    {
        Idle = 0,
        Walking = 1,
        Running = 2,
        Sprinting = 3,
        Jumping = 4,
        Falling = 5,
        Strafing = 6,
    }

}
