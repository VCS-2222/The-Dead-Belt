using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] float startHealth;
    [SerializeField] float currentHealth;

    private void Start()
    {
        currentHealth = startHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
}