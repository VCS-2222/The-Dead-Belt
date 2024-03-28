using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [Header("Important Components")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] ZombieStateMachine stateMachine;

    [Header("Variables")]
    [SerializeField] float currentSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    private void Start()
    {
        currentSpeed = walkSpeed;
        agent.speed = currentSpeed;
    }

    private void Update()
    {
        ValidateCurrentState();

        
    }

    void ValidateCurrentState()
    { 
        if (stateMachine.ReturnCurrentState() == stateMachine.idleState)
        {
            currentSpeed = 0;
        }

        if (stateMachine.ReturnCurrentState() == stateMachine.roamState)
        {
            currentSpeed = walkSpeed;
        }
    }
}