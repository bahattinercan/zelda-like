using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Smash()
    {
        animator.SetBool("smash", true);
        StartCoroutine(BreakCo());
    }

    private IEnumerator BreakCo()
    {
        yield return new WaitForSeconds(.3f);
        gameObject.SetActive(false);
    }
}