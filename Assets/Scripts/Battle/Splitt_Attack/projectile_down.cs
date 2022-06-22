using UnityEngine;

public class projectile_down : MonoBehaviour
{
    public float speed = 3.0f;

    // Moves Object down
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
