using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHUD : MonoBehaviour
{
    public Text enemyName;
    public Text enemeyLVL;
    public Text enemyHP;
    public Slider health;

    // Fills HUD with proper information
    public void setHUD(attribute attribute)
    {
        enemyName.text = attribute.Name;

        health.maxValue = attribute.maxHP;
        health.value = attribute.currentHP;
    }

    // Defines Max HP on the enemy Healthbar
    public void setMaxHP(float hp)
    {
        health.maxValue = hp;
        health.value = hp;
    }

    // Defines HP on enemy Healthbar
    public void setHP(float hp)
    {
        health.value = hp;
    }
}
