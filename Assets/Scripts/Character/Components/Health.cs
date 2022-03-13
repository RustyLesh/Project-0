using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    
    [SerializeField] private float maxHealth = 100;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        Mathf.Clamp((maxHealth -= damage), 0, maxHealth);
    }

    public void Heal(float healAmount)
    {
        Mathf.Clamp((maxHealth += healAmount), 0, maxHealth);
    }

    public float GetHealth()
    {
        return currentHealth;
    }
    public void SetMaxHealth(float maxHealthValue)
    {
        maxHealth = maxHealthValue;

        if (maxHealth < 1)
            maxHealth = 1;
    }
}
