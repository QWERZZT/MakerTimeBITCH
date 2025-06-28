using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Healthtext : MonoBehaviour
{
    public Health targetHealth;   // Перетяни сюда объект с компонентом Health
    public Text Text;           // Сюда перетяни UI Text (Legacy UI)

    void Update()
    {
        if (targetHealth != null && Text != null)
        {
            Text.text = "HP: " + Mathf.CeilToInt(targetHealth.currentHP) + " / " + Mathf.CeilToInt(targetHealth.maxHP);
        }
    }
}
