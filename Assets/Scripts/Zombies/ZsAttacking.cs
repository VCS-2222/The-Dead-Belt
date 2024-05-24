using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZsAttacking : ZState
{
    ZombieStateMachine stateMachine;
    Transform target;
    float accurateRemainingDistance;

    public void Activating(ZombieStateMachine StateMachine)
    {
        stateMachine = StateMachine;
        target = stateMachine.breathingTarget.transform;
        target.GetComponent<PlayerMovementStateMachine>().SetMobility(false);
        stateMachine.animatorController.SetSpecificAnimatorBools("biting", true);
        stateMachine.agent.isStopped = true;
        stateMachine.transform.LookAt(stateMachine.breathingTarget.gameObject.transform);
    }

    public void Updating()
    {
        //accurateRemainingDistance = Vector3.Distance(stateMachine.agent.transform.position, target.transform.position);
        //Debug.Log(accurateRemainingDistance);

        //if (accurateRemainingDistance > 2f)
        //{
        //    stateMachine.ChangeState(stateMachine.chaseState);
        //}
    }

    public void FixedUpdating()
    {

    }

    public void Deactivating()
    {
        stateMachine.animatorController.SetSpecificAnimatorBools("biting", false);
    }
}