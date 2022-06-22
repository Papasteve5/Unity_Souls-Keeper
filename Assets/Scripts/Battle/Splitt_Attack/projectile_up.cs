using UnityEngine;

public class projectile_up : MonoBehaviour
{
    public float speed = 3.0f;

    // Moves Object up
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
