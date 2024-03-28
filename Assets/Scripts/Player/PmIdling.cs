using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
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
        if(controls.Player.Crouching.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(stateMachine.crouchState);
        }

        if(stateMachine.zAxis > 0.2f || stateMachine.xAxis > 0.2f || stateMachine.zAxis < -0.2f || stateMachine.xAxis < -0.2f)
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