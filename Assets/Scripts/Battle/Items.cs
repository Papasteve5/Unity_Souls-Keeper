using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Items : MonoBehaviour
{
    public bool used;
    float HealthPotionHP = 5f;

    // HealthPotion Item, can be used to heal the Player
    public void HealthPotion(attribute attribute, Text battleText)
    {
        /*
        When Player gets healed to much
        Example:
        17 + 5 = 22
        22 - 20 = 2
        22 - 2 = 20
        */
        if (attribute.currentHP + HealthPotionHP >= attribute.maxHP)
        {
            attribute.currentHP += HealthPotionHP;
            float rest = attribute.currentHP - attribute.maxHP;
            attribute.currentHP -= rest;
        }
        // normal healing
        else
        {
            attribute.currentHP += HealthPotionHP;
        }
        used = true;
    }

    // Gets called when Player has max Health and doesn't need healing
    public IEnumerator errorHeal(Text battleText)
    {
        battleText.enabled = true;
        var originalBattleText = battleText.rectTransform.anchoredPosition;

        // displays message on a new position
        battleText.rectTransform.anchoredPosition = new Vector2(100, -27);
        battleText.text = "Your HP is already full!";
        yield return new WaitForSeconds(2f);

        // resets position of battleText
        battleText.enabled = false;
        battleText.rectTransform.anchoredPosition = originalBattleText;
    }
}
