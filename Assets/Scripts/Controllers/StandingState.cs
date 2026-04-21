using UnityEngine;

public class StandingState : State
{
    float gravityValue;
    bool jump;
    Vector3 currentVelocity;
    bool grounded;
    bool sprint;
    float playerSpeed;
    bool drawWeapon;

    Vector3 cVelocity;

    public StandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        jump = false;
        sprint = false;
        drawWeapon = false;

        gravityVelocity.y = 0;

        velocity = character.playerVelocity;
        currentVelocity = velocity;

        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (jumpAction.triggered)
        {
            jump = true;
        }
        if (sprintAction.IsPressed())
        {
            sprint = true;
        }
        if (drawWeaponAction != null && drawWeaponAction.triggered)
        {
            drawWeapon = true;
        }

        input = moveAction.ReadValue<Vector2>();

        Vector3 camForward = character.cameraTransform.forward;
        Vector3 camRight = character.cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        velocity = input.x * camRight + input.y * camForward;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.animator.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);

        if (sprint)
        {
            stateMachine.ChangeState(character.sprinting);
        }
        if (jump)
        {
            stateMachine.ChangeState(character.jumping);
        }
        if (drawWeapon)
        {
            stateMachine.ChangeState(character.combatting);
            character.animator.SetTrigger("drawWeapon");
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

        gravityVelocity.y = 0f;
        character.playerVelocity = velocity;

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}