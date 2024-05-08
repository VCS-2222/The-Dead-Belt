using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAnimatorController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;

    [Header("Values")]
    [SerializeField] float oneDimentionAnimatorBlend;

    private void Start()
    {
        oneDimentionAnimatorBlend = 0;
    }

    private void Update()
    {
        SetBlendToVelotity();
    }

    public void SetBlendToVelotity()
    {
        oneDimentionAnimatorBlend = agent.velocity.magnitude;
        animator.SetFloat("blend", oneDimentionAnimatorBlend);
    }
}