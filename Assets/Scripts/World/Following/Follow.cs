using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{
    public Animator animator;

    public float speed;
    public float Distance;

    private float step;

    void following()
    {
        Vector2 target = GameObject.FindGameObjectWithTag("Player").transform.position;


        step = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target) > Distance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }

    public void Update()
    {
    }

    private void FixedUpdate()
    {
        following();
    }

    /*
    void AnimMove()
    {
        animator.SetFloat("Horizontal", transform.position.x);
        animator.SetFloat("Vertical", transform.position.y);
        animator.SetFloat("Speed", transform.position.sqrMagnitude);
    }
    */

}
