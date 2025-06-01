using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"Player HP: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
            Destroy(gameObject);
        }
    }

    void Die()
    {
        Debug.Log("Игрок умер");
        
    }
}
