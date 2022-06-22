using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody2D rb;
    Vector2 movement;

    void Update()
    {
        Move();
    }

    // Function gets Input from Unity Function GetAxisRaw(), (Inputs are keys like: W, S, D, A or Arrow Keys)
    void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // Applies speed to input
    void FixedUpdate()
    {
        rb.velocity = new Vector3(movement.x * speed, movement.y * speed);
    }
}
