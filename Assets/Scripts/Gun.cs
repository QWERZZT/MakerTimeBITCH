using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public int maxAmmo = 10;          // Максимум пуль
    private int currentAmmo;
    public float reloadDelay = 10f;   // Время паузы (перезарядки) в секундах

    public Text killTextUI;  // В инспекторе назначь UI Text

private int killCount = 0;

public void AddKill()
{
    killCount++;
    UpdateKillText();
}

    public int killsToUpgrade = 30;

    [Header("Апгрейд")]
    public GameObject newGunPrefab;   // Объект новой пушки, который будет ставиться после апгрейда

    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateKillText();
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        currentAmmo--;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Пауза на перезарядку...");
        yield return new WaitForSeconds(reloadDelay);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void UpdateKillText()
    {
        if (killTextUI != null)
        {
            killTextUI.text = "Kills: " + killCount;
        }
    }

    void UpgradeGun()
    {
        Debug.Log("Апгрейд пушки!");

        if (newGunPrefab != null)
        {
            GameObject newGun = Instantiate(newGunPrefab, transform.position, transform.rotation);
            // Можно перенести некоторые настройки или данные (например UI) — зависит от ситуации

            Destroy(gameObject); // Удаляем текущую пушку
        }
        else
        {
            Debug.LogWarning("Не назначен объект новой пушки!");
        }
    }
}
