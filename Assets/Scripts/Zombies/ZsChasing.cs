using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZsChasing : ZState
{
    ZombieStateMachine stateMachine;
    Transform target;

    public void Activating(ZombieStateMachine StateMachine)
    {
        stateMachine = StateMachine;
        target = stateMachine.breathingTarget.transform;
        stateMachine.agent.destination = target.position;
    }

    public void Updating()
    {
        if(stateMachine.agent.remainingDistance < 1)
        {
            //stateMachine.ChangeState(stateMachine.);
        }
    }

    public void FixedUpdating()
    {

    }

    public void Deactivating()
    {

    }
}
