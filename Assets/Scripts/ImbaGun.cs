using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImbaGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public Text counterText;        // Текст, где хранится число
    public Transform teleportTarget; // Куда телепортироваться при достижении 30

    private bool teleported = false;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        CheckValueAndTeleport();
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void CheckValueAndTeleport()
    {
        if (teleported || counterText == null || teleportTarget == null)
            return;

        // Попытка распарсить число из текста
        if (int.TryParse(counterText.text, out int currentValue))
        {
            if (currentValue >= 30)
            {
                transform.position = teleportTarget.position;
                teleported = true;
                Debug.Log("Объект телепортирован, значение достигло " + currentValue);
            }
        }
    }
}
