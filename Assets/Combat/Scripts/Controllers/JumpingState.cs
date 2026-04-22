using UnityEngine;

public class JumpingState : State
{
    bool grounded;

    float gravityValue;
    float jumpHeight;
    float playerSpeed;

    Vector3 airVelocity;

    public JumpingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        grounded = false;
        gravityValue = character.gravityValue;
        jumpHeight = character.jumpHeight;
        playerSpeed = character.playerSpeed;
        gravityVelocity.y = 0;

        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");
        Jump();
    }
    public override void HandleInput()
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (grounded)
        {
            stateMachine.ChangeState(character.landing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        Vector3 camForward = character.cameraTransform.forward;
        Vector3 camRight = character.cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 inputDirection = input.x * camRight + input.y * camForward;
        Vector3 horizontalVelocity = Vector3.Lerp(character.playerVelocity, inputDirection, character.airControl);

        gravityVelocity.y += gravityValue * Time.deltaTime;

        character.playerVelocity = horizontalVelocity;

        character.controller.Move((horizontalVelocity * playerSpeed + gravityVelocity) * Time.deltaTime);

        if (inputDirection.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(
                character.transform.rotation,
                Quaternion.LookRotation(inputDirection),
                character.rotationSpeed * Time.deltaTime
            );
        }

        grounded = character.controller.isGrounded;

        if (grounded)
        {
            stateMachine.ChangeState(character.landing);
        }
    }

    public override void Exit()
    {
        base.Exit();

        Vector3 horizontalVelocity = character.playerVelocity;
        horizontalVelocity.y = 0f;
        character.playerVelocity = horizontalVelocity;
    }

    void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }
}