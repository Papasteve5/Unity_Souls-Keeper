using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Heart : MonoBehaviour
{
    public int MaxHP = 100;
    public int currentHP;

    public healthbar healthbar;

    private int EnemyDMG = 20;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;
        healthbar.setMaxHealth(MaxHP);
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthbar.setHealth(currentHP);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Projectile")
        {
            TakeDamage(EnemyDMG);
        }
    }

    void Die()
    {
        if (currentHP == 0) {
            Destroy(gameObject);
            SceneManager.LoadScene(sceneName:"Deathscreen");
        }
    }
}
