using UnityEngine;
using UnityEngine.InputSystem;

namespace FinalProject.CharacterControllers
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")]
        public CharacterController controller;
        public GameObject player; // for animations, if needed

        [Header("Movement")]
        public float moveSpeed = 2f;              // forward speed (units / second)
        public float laneChangeSpeed = 6f;        // how quickly we move to target X (units / second)
        public float[] lanePositions = new float[] { -2f, 0f, 2f }; // predefined X positions
        public float gravity = -9.81f;
        public float multiplier = 1f;

        [Header("Input")]
        public InputActionReference move;   // should provide a Vector2 (x = horizontal)
        public InputActionReference combat;

        private int _currentLane = 1;
        private float _targetX;
        private float _verticalVelocity;
        private Vector2 _moveInput;
        

        private void Start()
        {
            controller = controller ?? GetComponent<CharacterController>();

            // Pick nearest lane on start
            if (lanePositions != null && lanePositions.Length > 0)
            {
                float minDist = float.MaxValue;
                for (int i = 0; i < lanePositions.Length; i++)
                {
                    float d = Mathf.Abs(transform.position.x - lanePositions[i]);
                    if (d < minDist)
                    {
                        minDist = d;
                        _currentLane = i;
                    }
                }

                _targetX = lanePositions[_currentLane];
            }
            else
            {
                _targetX = transform.position.x;
            }
        }

        
        public void OnEnable()
        {
            if (move?.action != null)
            {
                move.action.performed += OnMovePerformed;
                move.action.canceled += OnMoveCanceled;
            }   

            if (combat?.action != null)
                combat.action.started += Action;
        }

        private void OnDisable()
        {
            if (move?.action != null)
            {
                move.action.performed -= OnMovePerformed;
                move.action.canceled -= OnMoveCanceled;
            }

            if (combat?.action != null)
                combat.action.started -= Action;
        }

        private void OnMovePerformed(InputAction.CallbackContext ctx)
        {
            _moveInput = ctx.ReadValue<Vector3>();

            // A/D or left/right on joystick: use horizontal component.
            if (_moveInput.x < -0.5f)
                MoveLeft();
            else if (_moveInput.x > 0.5f)
                MoveRight();
        }

        private void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            _moveInput = Vector2.zero;
        }

        private void Update()
        {
            
            if (controller == null)
                return;

            // Apply gravity
            if (controller.isGrounded && _verticalVelocity < 0f)
                _verticalVelocity = -1f; // small downward force to keep grounded

            _verticalVelocity += gravity * Time.deltaTime;

            // Smoothly move X toward target lane
            float newX = Mathf.MoveTowards(transform.position.x, _targetX, laneChangeSpeed * Time.deltaTime);
            float deltaX = newX - transform.position.x;

            // Constant forward movement
            moveSpeed += 0.05f;
            float forwardDelta = (moveSpeed * multiplier) * Time.deltaTime;

            // Vertical movement from gravity
            float verticalDelta = _verticalVelocity * Time.deltaTime ;

            // Build total displacement
            Vector3 displacement = new Vector3(deltaX, verticalDelta);

            // Move the CharacterController once per frame
            controller.Move(displacement);
            controller.Move(Vector3.forward * forwardDelta);
        }

        private void MoveLeft()
        {
            if (lanePositions == null || lanePositions.Length == 0)
                return;
             // snap to current lane before changing
            _currentLane = Mathf.Clamp(_currentLane - 1, 0, lanePositions.Length - 1);
            _targetX = lanePositions[_currentLane];
        }

        private void MoveRight()
        {
            if (lanePositions == null || lanePositions.Length == 0)
                return;

            _currentLane = Mathf.Clamp(_currentLane + 1, 0, lanePositions.Length - 1);
            _targetX = lanePositions[_currentLane];
        }

        public void Action(InputAction.CallbackContext obj)
        {
            Debug.Log("Action complete");
            player.gameObject.GetComponent<Animator>().Play("MeleeAttack_OneHanded");
            
        }
    }
}

