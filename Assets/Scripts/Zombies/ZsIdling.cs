using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZsIdling : ZState
{
    ZombieStateMachine stateMachine;

    public void Activating(ZombieStateMachine StateMachine)
    {
        stateMachine = StateMachine;
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