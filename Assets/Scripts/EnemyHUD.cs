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

    public void setHUD(attribute attribute) {

        enemyName.text = attribute.Name;

        health.maxValue = attribute.maxHP;
        health.value = attribute.currentHP;
    }

    public void setMaxHP(int hp) {

        health.maxValue = hp;
        health.value = hp;
    }

    public void setHP(int hp) {

        health.value = hp;
    }
}
