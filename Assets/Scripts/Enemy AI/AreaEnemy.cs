using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Log
{
    public Collider2D boundary;

    protected override void CheckDistance()
    {
        float targetDis = Vector3.Distance(target.position, transform.position);
        // player in the chaseRadius and not in the attackRadius
        if (targetDis <= chaseRadius && targetDis > attackRadius && boundary.bounds.Contains(target.transform.position))
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
        else if (targetDis > chaseRadius || !boundary.bounds.Contains(target.transform.position))
        {
            animator.SetBool("wakeUp", false);
        }
    }
}
