using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider hungerSlider;

    [Header("Variables")]
    [SerializeField] float maxHealth;
    [SerializeField] float maxHunger;
    [SerializeField] float currentHealth;
    [SerializeField] float currentHunger;

    private void Start()
    {
        currentHealth = maxHealth;
        currentHunger = maxHunger;
    }

    private void Update()
    {
        HungerDecay();
        AssignVariablesToSliders();
    }

    public void HungerDecay()
    {
        currentHunger -= .15f * Time.deltaTime;

        if(currentHealth < 0 )
        {
            TakeDamage(.2f);

            currentHealth = 0;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void GainHealth(float health)
    {
        if(currentHealth + health > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
        }
    }

    public void Eat(float food)
    {
        if (currentHunger + food > maxHunger)
        {
            currentHunger = maxHunger;
        }
        else
        {
            currentHunger += food;
        }
    }

    public void AssignVariablesToSliders()
    {
        healthSlider.value = currentHealth;
        hungerSlider.value = currentHunger;
    }
}