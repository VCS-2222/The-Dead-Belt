using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PmIdling : IState
{
    PlayerMovementStateMachine stateMachine;
    public Controls controls;

    public void OnActivated(PlayerMovementStateMachine StateMachine)
    {
        controls = new Controls();
        controls.Enable();
        stateMachine = StateMachine;
        stateMachine.SetSpeed(0.05f);
    }

    public void OnUpdate()
    {
        if(controls.Player.Crouching.WasPerformedThisFrame() && stateMachine.ReturnMovementAllowance())
        {
            stateMachine.ChangeState(stateMachine.crouchState);
        }

        if (controls.Player.Proning.WasPerformedThisFrame() && stateMachine.ReturnMovementAllowance())
        {
            stateMachine.ChangeState(stateMachine.crawlingState);
        }

        if (stateMachine.zAxis > 0.2f || stateMachine.xAxis > 0.2f || stateMachine.zAxis < -0.2f || stateMachine.xAxis < -0.2f)
        {
            stateMachine.ChangeState(stateMachine.walkState);
        }
    }

    public void OnFixedUpdate()
    {

    }

    public void OnDeactivated()
    {
        controls.Disable();
    }
}