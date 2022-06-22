using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deploy : MonoBehaviour
{
    public GameObject Igris;
    public Transform playerPos;

    private GameObject playerSpawn;
    private bool isSpawned;

    public void Update()
    {
        deployIgris();
    }

    public void deployIgris()
    {
        if (Input.GetMouseButtonDown(1) && isSpawned == false)
        {
            Debug.Log("Summon Igris");
            playerSpawn = Instantiate(Igris, playerPos);
            isSpawned = true;
        }
        else if (Input.GetMouseButtonDown(0) && isSpawned == true)
        {
            Debug.Log("Despawn Igris");
            Destroy(playerSpawn);
            isSpawned = false;
        }
    }
}
