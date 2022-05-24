using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text playerName;
    public Text playerLVL;
    public Text playerHP;
    public Slider health;

    public void setHUD(attribute attribute) {

        playerName.text = attribute.Name;
        playerLVL.text = "LV " + attribute.lvl;
        playerHP.text = "HP " + attribute.currentHP.ToString() + "/" + attribute.maxHP.ToString();


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
