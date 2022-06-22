using UnityEngine;

public class GoblinHealth : MonoBehaviour
{
    public int maxHealth;
    public int healthCurrent;

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
