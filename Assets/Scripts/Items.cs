using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Items : MonoBehaviour
{
    public bool used;
    float HealthPotionHP = 5;

    public void HealthPotion(attribute attribute)
    {
        /*Healing example when Player gets healed to much*/
        // 17 + 5 = 22
        // 22 - 20 = 2
        // 22 - 2 = 20

        if (attribute.currentHP + HealthPotionHP >= attribute.maxHP)
        {
            attribute.currentHP += HealthPotionHP;
            float rest = attribute.currentHP - attribute.maxHP;
            attribute.currentHP -= rest;
        }
        else
        {
            attribute.currentHP += HealthPotionHP;
        }
        used = true;
    }
}
