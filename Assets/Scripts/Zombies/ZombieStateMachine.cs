using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public string showStatus;
    [SerializeField] ZState currentState;
    [SerializeField] ZState lastState;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public GameObject breathingTarget;

    public ZsIdling idleState = new ZsIdling();
    public ZsRoaming roamState = new ZsRoaming();
    public ZsChasing chaseState = new ZsChasing();

    private void Start()
    {
        lastState = null;
        ChangeState(idleState);
    }

    public void PopulateAgent(NavMeshAgent addAgent)
    {
        agent = addAgent;
    }

    private void Update()
    {
        showStatus = currentState.ToString();

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