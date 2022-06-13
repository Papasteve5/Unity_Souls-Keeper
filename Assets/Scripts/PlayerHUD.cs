using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Text playerName;
    public Text playerLVL;
    public Text playerHP;
    public RectTransform playerHP_POS;
    public Slider health;
    public RectTransform health_POS;
    public GameObject buttons;

    public void setHUD(attribute attribute)
    {

        playerName.text = attribute.Name;
        playerLVL.text = "LV " + attribute.lvl;
        playerHP.text = "HP " + attribute.currentHP.ToString() + "/" + attribute.maxHP.ToString();


        health.maxValue = attribute.maxHP;
        health.value = attribute.currentHP;
    }

    public void setMaxHP(float hp)
    {

        health.maxValue = hp;
        health.value = hp;
    }

    public void setHP(float hp)
    {

        health.value = hp;
    }
}
