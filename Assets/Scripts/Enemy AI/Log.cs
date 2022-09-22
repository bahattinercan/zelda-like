using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    protected Rigidbody2D rb;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePos;
    public Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        currentState = EEnemyState.idle;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        animator.SetBool("wakeUp", true);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CheckDistance();
    }

    protected virtual void CheckDistance()
    {
        float targetDis = Vector3.Distance(target.position, transform.position);
        // player in the chaseRadius and not in the attackRadius
        if (targetDis <= chaseRadius && targetDis > attackRadius)
        {
            if (currentState == EEnemyState.idle || currentState == EEnemyState.walk && currentState != EEnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                rb.MovePosition(temp);
                ChangeState(EEnemyState.walk);
                animator.SetBool("wakeUp", true);
            }
        }
        // player not in the chaseRadius
        else if (targetDis > chaseRadius)
        {
            animator.SetBool("wakeUp", false);
        }
    }

    public void SetAnimFloat(Vector2 setVector)
    {
        animator.SetFloat("moveX", setVector.x);
        animator.SetFloat("moveY", setVector.y);
    }

    protected void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    protected void ChangeState(EEnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}