using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PmWalking : IState
{
    PlayerMovementStateMachine stateMachine;

    [Header("Variables")]
    [SerializeField] float normalWalkSpeed = .15f;
    [SerializeField] float hurtWalkSpeed = .12f;
    public Controls controls;

    public void OnActivated(PlayerMovementStateMachine StateMachine)
    {
        controls = new Controls();
        controls.Enable();
        stateMachine = StateMachine;
        stateMachine.SetSpeed(normalWalkSpeed);
    }

    public void OnUpdate()
    {
        if(stateMachine.xAxis == 0 && stateMachine.zAxis == 0)
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }

        if (controls.Player.Crouching.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(stateMachine.crouchState);
        }

        if (controls.Player.Running.WasPressedThisFrame())
        {
            stateMachine.ChangeState(stateMachine.runState);
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