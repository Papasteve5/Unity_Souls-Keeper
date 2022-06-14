using UnityEngine;

public class projectile_left : MonoBehaviour
{
    public float speed = 3.0f;

    // Moves Object left
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
