using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attribute : MonoBehaviour
{
    public string Name;
    public int lvl;

    public int damage;

    public int maxHP;
    public int currentHP;

    public bool TakeDamage(int dmg) {

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
            TakeDamage(1);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }
}