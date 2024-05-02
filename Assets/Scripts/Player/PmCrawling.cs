using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PmCrawling : IState
{
    PlayerMovementStateMachine stateMachine;
    float crawlingSpeed = .05f;
    public Controls controls;

    float cameraOriginal = 0.7f;
    float controllerHeightOriginal = 1.7f;

    float newCameraPosition = 0f;
    float newControllerHeight = 0.5f;

    public void OnActivated(PlayerMovementStateMachine StateMachine)
    {
        controls = new Controls();
        controls.Enable();
        stateMachine = StateMachine;
        stateMachine.SetSpeed(crawlingSpeed);
        stateMachine.ChangeControllerAndCamera(newCameraPosition, newControllerHeight);
    }

    public void OnUpdate()
    {
        RaycastHit hit;

        Physics.Raycast(stateMachine.transform.position, Vector3.up, out hit, 1.5f);

        if (hit.collider != null) return;

        if (controls.Player.Proning.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(stateMachine.walkState);
        }

        if (controls.Player.Crouching.WasPerformedThisFrame())
        {
            stateMachine.ChangeState(stateMachine.crouchState);
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