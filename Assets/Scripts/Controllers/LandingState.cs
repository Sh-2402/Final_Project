using UnityEngine;

public class LandingState : State
{
    float timePassed;
    float landingTime;

    public LandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        timePassed += Time.deltaTime;

        if (timePassed > landingTime)
        {
            stateMachine.ChangeState(character.standing);
        }
    }
}