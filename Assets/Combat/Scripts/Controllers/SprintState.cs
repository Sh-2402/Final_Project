using UnityEngine;

public class SprintState : State
{
    float gravityValue;
    bool grounded;
    bool sprint;
    float playerSpeed;
    bool sprintJump;

    public SprintState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        sprint = true;
        sprintJump = false;

        playerSpeed = character.sprintSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;

        velocity = character.playerVelocity;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();

        Vector3 camForward = character.cameraTransform.forward;
        Vector3 camRight = character.cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        velocity = input.x * camRight + input.y * camForward;

        sprint = sprintAction.IsPressed() && input.sqrMagnitude > 0f;

        if (jumpAction.triggered)
        {
            sprintJump = true;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (sprint)
        {
            character.animator.SetFloat("speed", input.magnitude + 0.5f, character.speedDampTime, Time.deltaTime);
        }
        else
        {
            stateMachine.ChangeState(character.standing);
        }

        if (sprintJump)
        {
            stateMachine.ChangeState(character.jumping);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;

        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }

        character.controller.Move(velocity * playerSpeed * Time.deltaTime + gravityVelocity * Time.deltaTime);

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }
    }

    public override void Exit()
    {
        base.Exit();
        character.playerVelocity = velocity;
    }
}