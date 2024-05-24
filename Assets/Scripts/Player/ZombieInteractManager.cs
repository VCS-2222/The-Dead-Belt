using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieInteractManager : MonoBehaviour
{
    [SerializeField] ZombieStateMachine stateMachine;

    public void DamagePlayer()
    {
        stateMachine.breathingTarget.GetComponent<PlayerStats>().TakeDamage(15);
    }
}