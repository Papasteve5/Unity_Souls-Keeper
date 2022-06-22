using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 pos;
    private GameObject player;

    void Start()
    {
        offset = transform.position;
    }


    void Update()
    {
    }

    void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            transform.position = player.transform.position + offset;
        }
    }
}
