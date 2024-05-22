using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZsChasing : ZState
{
    ZombieStateMachine stateMachine;
    Transform target;
    float accurateRemainingDistance;

    public void Activating(ZombieStateMachine StateMachine)
    {
        stateMachine = StateMachine;
        target = stateMachine.breathingTarget.transform;
        stateMachine.agent.destination = target.position;
    }

    public void Updating()
    {
        stateMachine.agent.destination = target.position;

        accurateRemainingDistance = Vector3.Distance(stateMachine.agent.transform.position, target.transform.position);
        //Debug.Log(accurateRemainingDistance);

        if (accurateRemainingDistance < 1.35f)
        {
            stateMachine.ChangeState(stateMachine.attackState);
        }
    }

    public void FixedUpdating()
    {

    }

    public void Deactivating()
    {

    }
}
