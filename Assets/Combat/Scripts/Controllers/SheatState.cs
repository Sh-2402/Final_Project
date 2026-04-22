using UnityEngine;

public class SheathState : State
{
    float timePassed;
    float clipLength;
    float clipSpeed;

    public SheathState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        timePassed = 0f;

        // Keep combat layer ON so the sheath animation can be seen
        character.animator.SetLayerWeight(character.combatLayerIndex, 1f);

        character.animator.SetTrigger("sheathWeapon");
        character.animator.SetFloat("speed", 0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        timePassed += Time.deltaTime;

        AnimatorClipInfo[] clips = character.animator.GetCurrentAnimatorClipInfo(character.combatLayerIndex);

        if (clips.Length > 0)
        {
            clipLength = clips[0].clip.length;
            clipSpeed = character.animator.GetCurrentAnimatorStateInfo(character.combatLayerIndex).speed;

            if (clipSpeed <= 0f)
            {
                clipSpeed = 1f;
            }

            if (timePassed >= clipLength / clipSpeed)
            {
                // Now that the sheath animation has finished,
                // turn the combat layer off and return to standing
                character.animator.SetLayerWeight(character.combatLayerIndex, 0f);
                stateMachine.ChangeState(character.standing);
            }
        }
    }
}