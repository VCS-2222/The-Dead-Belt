using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [Header("Important Components")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] ZombieStateMachine stateMachine;
    [SerializeField] Transform zombieEyes;

    [Header("Variables")]
    [SerializeField] float currentSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float sight;

    private void Start()
    {
        stateMachine.PopulateAgent(agent);
        currentSpeed = walkSpeed;
        agent.speed = currentSpeed;
    }

    private void Update()
    {
        ValidateCurrentState();

        VisionPerAngle(45);
        VisionPerAngle(30);
        VisionPerAngle(15);
        VisionPerAngle(0);
        VisionPerAngle(-15);
        VisionPerAngle(-30);
        VisionPerAngle(-45);
    }

    void VisionPerAngle(float angleIn)
    {
        Vector3 start = zombieEyes.position;
        Quaternion angle = Quaternion.Euler(0, angleIn, 0);

        Physics.Raycast(start, angle * transform.forward, out RaycastHit hit, sight);

        if (hit.transform == null) return;

        Debug.DrawLine(start, hit.point, Color.red);

        if (hit.collider.tag == "Player")
        {
            stateMachine.breathingTarget = hit.transform.gameObject;
            stateMachine.ChangeState(stateMachine.chaseState);
        }
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

        if (stateMachine.ReturnCurrentState() == stateMachine.chaseState)
        {
            currentSpeed = runSpeed;
        }
    }
}