using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PmRunning : IState
{
    PlayerMovementStateMachine stateMachine;
    float normalRunningSpeed = .25f;
    float hurtRunningSpeed = .17f;
    public Controls controls;

    public void OnActivated(PlayerMovementStateMachine StateMachine)
    {
        controls = new Controls();
        controls.Enable();
        stateMachine = StateMachine;
        stateMachine.SetSpeed(normalRunningSpeed);
    }

    public void OnUpdate()
    {
        if(controls.Player.Running.WasReleasedThisFrame() && stateMachine.controllerVelocity > 0)
        {
            stateMachine.ChangeState(stateMachine.walkState);
        }

        if(controls.Player.Running.WasReleasedThisFrame() && stateMachine.controllerVelocity == 0)
        {
            stateMachine.ChangeState(stateMachine.idleState);
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