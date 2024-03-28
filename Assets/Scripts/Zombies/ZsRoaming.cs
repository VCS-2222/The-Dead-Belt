using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZsRoaming : ZState
{
    ZombieStateMachine stateMachine;
    float roamArea;

    public void Activating(ZombieStateMachine StateMachine)
    {
        stateMachine = StateMachine;
        roamArea = Random.Range(2, 50);
    }

    public void Updating()
    {

    }

    public void FixedUpdating()
    {

    }

    public void Deactivating()
    {

    }
}