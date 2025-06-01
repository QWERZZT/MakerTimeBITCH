using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifetime = 2f;

    public Gun gun; // Ссылка на скрипт пушки, чтобы вызывать AddKill()

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    vvoid OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Enemy"))
    {
        Target target = other.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(damage);

            if (target.health <= 0)
            {
                if (gun != null)
                    gun.AddKill();  // +1 к киллам и обновление UI
            }
        }
        Destroy(gameObject);
    }
    else
    {
        Destroy(gameObject);
    }
}
}
