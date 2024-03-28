using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ZState
{
    public void Activating(ZombieStateMachine StateMachine);
    public void Updating();
    public void FixedUpdating();
    public void Deactivating();
}

public class ZombieStateMachine : MonoBehaviour
{
    [Header("State Status")]
    [SerializeField] ZState currentState;
    [SerializeField] ZState lastState;

    public ZsIdling idleState = new ZsIdling();
    public ZsRoaming roamState = new ZsRoaming();
    //public PmRunning runState = new PmRunning();

    private void Start()
    {
        lastState = null;
        ChangeState(idleState);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Updating();
        }
    }
    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixedUpdating();
        }
    }

    public ZState ReturnCurrentState()
    {
        return currentState;
    }

    public void ChangeState(ZState newState)
    {
        if (currentState != null)
        {
            currentState.Deactivating();
        }

        lastState = currentState;
        currentState = newState;
        currentState.Activating(this);
    }
}