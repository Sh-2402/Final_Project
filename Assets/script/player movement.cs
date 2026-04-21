using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public float gravity = -20f;

    [Header("Jump")]
    public float jumpHeight = 1.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private CharacterController _controller;
    private Camera _cam;
    private Vector3 _velocity;
    private bool _isGrounded;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _cam = Camera.main;
    }

    void Update()
    {
        GroundCheck();
        Move();
        Jump();
        ApplyGravity();
    }

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(
            groundCheck.position, groundCheckRadius, groundLayer);

        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -2f;
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 camForward = _cam.transform.forward;
        Vector3 camRight   = _cam.transform.right;
        camForward.y = 0;
        camRight.y   = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = (camForward * v + camRight * h).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

            _controller.Move(moveDir * moveSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        
        // Only jump when grounded and Space is pressed
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            // Formula: velocity = sqrt(jumpHeight * -2 * gravity)
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ApplyGravity()
    {
        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}