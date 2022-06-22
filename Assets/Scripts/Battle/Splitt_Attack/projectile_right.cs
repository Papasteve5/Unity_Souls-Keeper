using UnityEngine;

public class projectile_right : MonoBehaviour
{
    public float speed = 3.0f;

    // Moves Object right
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
