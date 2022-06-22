using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;

    public Animator animator;

    public GameObject Igris;

    public float speed;

    Vector2 movement;


    private void Start()
    {
        Igris.GetComponent<Renderer>().enabled = true;
    }

    public void Update()
    {
        PlayerMove();


        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Despawn Igris");
            Igris.GetComponent<Renderer>().enabled = false;

        }
        
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Summon Igris");
            Igris.GetComponent<Renderer>().enabled = true;
        }

    }

    void PlayerMove()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(movement.x * speed, movement.y * speed);
    }
}
