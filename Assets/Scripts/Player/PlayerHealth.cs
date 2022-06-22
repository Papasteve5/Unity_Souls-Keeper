using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;

    public int healthCurrent;

    private int damage = 50;

    private bool isAlive = true;


    private void Start()
    {
        ResetHealth();
    }

    private void ResetHealth()
    {
        healthCurrent = maxHealth;
    }

    private void Update()
    {
        PlayerDie();
    }


    private void PlayerDie()
    {
        if (healthCurrent <= 0)
        {
            Destroy(gameObject);
            isAlive = false;
        }
    }
}
