using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attribute : MonoBehaviour
{
    public string Name;

    // Level and Experience Points
    public int lvl;
    public float EXP;

    // Health Points
    public float maxHP;
    public float currentHP;

    // DAMAGE
    public float damage;
    public int fire_multiplier;
    public float takenDamage;

    // Friendliness Points
    public int friendliness;
    public int maxfriendliness;

    // Calculates characters Health and Damage based on it's LVL
    public void setCharacter()
    {
        float MaximumPossibleHP = 999;
        float MaximumPossibleLevel = 100;
        maxHP = Mathf.Round(maxHP + (MaximumPossibleHP - maxHP) * lvl / MaximumPossibleLevel);


        float MaximumPossibleDMG = 999;
        damage = Mathf.Round(damage + (MaximumPossibleDMG - damage) * lvl / MaximumPossibleLevel / 4);

        maxfriendliness = maxfriendliness + (2 + lvl);
        currentHP = maxHP;
    }

    // Function, player takes damage
    public bool TakeDamage(float dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Function hurts Player when touched by <Projectile>
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            TakeDamage(takenDamage);
        }
    }
}
