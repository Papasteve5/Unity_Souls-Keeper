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

    // Objects used for Functions showHUD() & hideHUD() in the "battlesystem" script
    public RectTransform playerHP_POS;
    public RectTransform health_POS;
    public GameObject buttons;

    // Fills HUD with proper information
    public void setHUD(attribute attribute)
    {
        playerName.text = attribute.Name;
        playerLVL.text = "LV " + attribute.lvl;
        playerHP.text = "HP " + attribute.currentHP.ToString() + "/" + attribute.maxHP.ToString();

        health.maxValue = attribute.maxHP;
        health.value = attribute.currentHP;
    }

    // Defines Max HP on the player Healthbar
    public void setMaxHP(float hp)
    {
        health.maxValue = hp;
        health.value = hp;
    }

    // Defines HP on player Healthbar
    public void setHP(float hp)
    {
        health.value = hp;
    }
}
