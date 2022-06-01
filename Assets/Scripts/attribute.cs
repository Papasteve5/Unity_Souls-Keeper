using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attribute : MonoBehaviour
{
    public string Name;

    public int lvl;
    public float EXP;

    public float maxHP;
    public float currentHP;

    public float damage;
    public float takenDamage;
    public int fire_multiplier;


    public int friendliness;
    public int maxfriendliness;

    public bool TakeDamage(float dmg) {

        currentHP -= dmg;

        if(currentHP <= 0) {
            return true;
        } else {
            return false;
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            TakeDamage(takenDamage);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    public void setCharacter() {
        //CurrentMaxHp = BaseHP + (MaximumPossibleHP - BaseHP) * CurrentLevel / MaximumPossibleLevel

        float MaximumPossibleHP = 999;
        float MaximumPossibleLevel = 100;

        maxHP = Mathf.Round(maxHP + (MaximumPossibleHP - maxHP) * lvl / MaximumPossibleLevel);

        //maxHP = maxHP + lvl * 5;
        //damage = damage + lvl + 5;

        float MaximumPossibleDMG = 999;

        damage = Mathf.Round(damage + (MaximumPossibleDMG - damage) * lvl / MaximumPossibleLevel / 4);


        maxfriendliness = maxfriendliness + (2 + lvl);
        currentHP = maxHP;

        //(level / x) * y
    }
}
