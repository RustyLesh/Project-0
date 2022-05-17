using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour, ISaveable
{
    [SerializeField] private float currentHealth;
    
    [SerializeField] private float maxHealth = 100;

    public delegate void HealthChanged();
    public static event HealthChanged OnHealthChanged;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        OnHealthChanged.Invoke();

        if(this.currentHealth <= 0){
            CSS_GameManager.Instance.SetIsPlayerDead(true);
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Clamp((currentHealth + healAmount), 0, maxHealth);
        OnHealthChanged.Invoke();
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

    public object SaveState() {
        Debug.Log(currentHealth);
        Debug.Log(maxHealth);
        return new SaveData() {
            currentHealth = this.currentHealth,
            maxHealth = this.maxHealth
        };
    }

    public void LoadState(object state) {
        var saveData = (SaveData)state;
        currentHealth = saveData.currentHealth;
        maxHealth = saveData.maxHealth;
    }

    [Serializable]
    private struct SaveData {
        public float currentHealth;
        public float maxHealth;
    }
}
