using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 0.5f;      // Радиус атаки
    public float attackCooldown = 2f; // Задержка между атаками в секундах

    private float lastAttackTime = 0f;
    private Transform player;
    private Health playerHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
    }

    void Update()
    {
        if (player == null || playerHealth == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Враг атакует! Нанесено урона: " + damage);
        }
    }
}
