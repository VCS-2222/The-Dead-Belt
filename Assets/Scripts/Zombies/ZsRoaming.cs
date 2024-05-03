using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZsRoaming : ZState
{
    ZombieStateMachine stateMachine;
    float roamArea;
    NavMeshHit hit;

    public void Activating(ZombieStateMachine StateMachine)
    {
        stateMachine = StateMachine;
        roamArea = Random.Range(2, 5);
        GenerateRandomDestination();
    }

    public void Updating()
    {
        if(stateMachine.agent.remainingDistance < 2)
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }

    public void GenerateRandomDestination()
    {
        Vector3 randomCoords = stateMachine.transform.position + Random.insideUnitSphere * roamArea;

        if (NavMesh.SamplePosition(randomCoords, out hit, 1, NavMesh.AllAreas))
        {
            Debug.Log("Path found");
            Vector3 coords = hit.position;
            stateMachine.agent.ResetPath();
            stateMachine.agent.SetDestination(coords);
        }
        else
        {
            Debug.Log("No path found");
            return;
        }
    }

    public void FixedUpdating()
    {

    }

    public void Deactivating()
    {

    }
}