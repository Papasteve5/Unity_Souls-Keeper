using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody2D rb;
    Vector2 movement;

    void Start()
    {
    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        /*Hitbox Geschwindigkeit = Neue Bewegung in Richtung x & y mit der Geschwindigkeit*/
        rb.velocity = new Vector3(movement.x * speed, movement.y * speed);
    }
}
