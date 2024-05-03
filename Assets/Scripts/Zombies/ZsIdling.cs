using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZsIdling : ZState
{
    ZombieStateMachine stateMachine;
    float idlingTimer;

    public void Activating(ZombieStateMachine StateMachine)
    {
        stateMachine = StateMachine;
        idlingTimer = Random.Range(1, 5);
    }

    public void Updating()
    {
        idlingTimer -= Time.deltaTime;

        if(idlingTimer < 0)
        {
            stateMachine.ChangeState(stateMachine.roamState);
        }
    }

    public void FixedUpdating()
    {

    }

    public void Deactivating() 
    {
        idlingTimer = 0;
    }
}