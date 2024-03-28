using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PmCrawling : IState
{
    PlayerMovementStateMachine stateMachine;

    public void OnActivated(PlayerMovementStateMachine StateMachine)
    {
        stateMachine = StateMachine;
    }

    public void OnUpdate()
    {

    }

    public void OnFixedUpdate()
    {

    }

    public void OnDeactivated()
    {

    }
}