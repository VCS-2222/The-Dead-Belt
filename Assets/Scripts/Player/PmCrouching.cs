using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PmCrouching : IState
{
    PlayerMovementStateMachine stateMachine;
    float crouchSpeed = .1f;
    public Controls controls;

    float cameraOriginal = 0.7f;
    float controllerHeightOriginal = 1.7f;

    float newCameraPosition = 0.3f;
    float newControllerHeight = 1f;

    public void OnActivated(PlayerMovementStateMachine StateMachine)
    {
        controls = new Controls();
        controls.Enable();
        stateMachine = StateMachine;
        stateMachine.SetSpeed(crouchSpeed);
        stateMachine.ChangeControllerAndCamera(newCameraPosition, newControllerHeight);
    }

    public void OnUpdate()
    {
        RaycastHit hit;

        Physics.Raycast(stateMachine.transform.position, Vector3.up, out hit, 1);

        if (controls.Player.Proning.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(stateMachine.crawlingState);
        }

        if (hit.collider != null) return;

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
        stateMachine.ChangeControllerAndCamera(cameraOriginal, controllerHeightOriginal);
        controls.Disable();
    }
}