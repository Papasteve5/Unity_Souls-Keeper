using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    GameObject Player;

    attribute playerAttribute;


    // Start is called before the first frame update
    void Start()
    {
        Player = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
        playerAttribute = Player.GetComponent<attribute>();
    }

    void Update()
    {
    }
}
