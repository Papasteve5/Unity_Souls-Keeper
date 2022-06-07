using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Items : MonoBehaviour
{
    float HealthPotionHP = 5;

    public void HealthPotion(attribute attribute)
    {
        attribute.currentHP += HealthPotionHP;
    }
}
