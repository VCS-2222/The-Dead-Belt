using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PmCrouching : IState
{
    PlayerMovementStateMachine stateMachine;
    float crouchSpeed = .1f;
    public Controls controls;

    float controllerCenterOriginal = 0;
    float controllerHeightOriginal = 1.7f;

    float newControllerCenter = .4f;
    float newControllerHeight = 1f;

    public void OnActivated(PlayerMovementStateMachine StateMachine)
    {
        controls = new Controls();
        controls.Enable();
        stateMachine = StateMachine;
        stateMachine.SetSpeed(crouchSpeed);
        stateMachine.StartCoroutine(stateMachine.ChangeControllerGradually(newControllerCenter, newControllerHeight, 1.2f));
        //stateMachine.transform.GetComponent<GravityApplier>().ChangeGroundCheckYLevel(1);
    }

    public void OnUpdate()
    {
        if (controls.Player.Running.WasPerformedThisFrame() || controls.Player.Crouching.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(stateMachine.walkState);
        }
    }

    public void OnFixedUpdate()
    {

    }

    public void OnDeactivated()
    {
        stateMachine.StartCoroutine(stateMachine.ChangeControllerGradually(controllerCenterOriginal, controllerHeightOriginal, 1.2f));
        //stateMachine.transform.GetComponent<GravityApplier>().ChangeGroundCheckYLevel(0);
        controls.Disable();
    }
}