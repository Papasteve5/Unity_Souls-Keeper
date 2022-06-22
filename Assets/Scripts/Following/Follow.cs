using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    public Animator animator;
    public Transform target;

    private Rigidbody2D rb;

    public float speed;
    public float Distance;

    private float step;

    private bool touched = false;

    void Start()
    {
    }

    private void Update()
    {
        AnimMove();
    }

    void FixedUpdate()
    {
        step = speed * Time.deltaTime;


        if (Vector3.Distance(transform.position, target.position) > Distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
    }

    void AnimMove()
    {
        animator.SetFloat("Horizontal", transform.position.x);
        animator.SetFloat("Vertical", transform.position.y);
        animator.SetFloat("Speed", transform.position.sqrMagnitude);
    }
}
