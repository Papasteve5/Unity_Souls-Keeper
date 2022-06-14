using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveSet { HORIZONTAL, VERTICAL, ALL, NOTHING }

public class Attack : MonoBehaviour
{
    public GameObject Arrow_down;
    private GameObject newArrow_down;
    public GameObject Arrow_up;
    private GameObject newArrow_up;
    public GameObject Arrow_left;
    private GameObject newArrow_left;
    public GameObject Arrow_right;
    private GameObject newArrow_right;
    private bool isCreated;


    // Always updates Attack Cleanup
    void Update()
    {
        clearAttack();
    }

    // Spawns Arrows from all directions at random Positions
    public void allAttack()
    {
        isCreated = false;

        if (!isCreated)
        {
            for (int i = 0; i < 3; i++)
            {
                newArrow_down = Instantiate(Arrow_down, new Vector2(Random.Range(-1.8f, 1.8f), Random.Range(8.0f, 12.0f)), Quaternion.identity);
            }
            for (int i = 0; i < 6; i++)
            {
                newArrow_up = Instantiate(Arrow_up, new Vector2(Random.Range(-1.8f, 1.8f), Random.Range(-8.0f, -12.0f)), Quaternion.identity);
            }
            for (int i = 0; i < 2; i++)
            {
                newArrow_left = Instantiate(Arrow_left, new Vector2(Random.Range(14.0f, 18.0f), Random.Range(-0.5f, -2.6f)), Quaternion.identity);
            }
            for (int i = 0; i < 2; i++)
            {
                newArrow_right = Instantiate(Arrow_right, new Vector2(Random.Range(-14.0f, -18.0f), Random.Range(-0.5f, -2.6f)), Quaternion.identity);
            }
        }
        isCreated = true;
    }

    // Deletes previous Arrows after a couple seconds
    public void clearAttack()
    {
        GameObject[] arrows;

        arrows = GameObject.FindGameObjectsWithTag("Projectile");

        foreach (GameObject arrow in arrows)
        {
            Destroy(arrow, 5);
        }
    }
}
