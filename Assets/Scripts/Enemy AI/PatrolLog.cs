using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance=.1f;

    protected override void CheckDistance()
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
            if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                rb.MovePosition(temp);
            }
            else
            {
                Debug.Log("dajopýdsaf");
                ChangeGoal();
            }            
        }
    }

    void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = path.Length - 2;
        }
        else
        {
            currentPoint++;
        }
        currentGoal = path[currentPoint];
    }
}
