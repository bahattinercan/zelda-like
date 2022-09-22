using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidBody;
    private Animator animator;
    private Vector3 change;
    public EPlayerState currentState;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public ScenePositions scenePositions;
    public Inventory playerInventory;
    public SpriteRenderer receiveItemSprite;
    public Signal playerHit;

    // Start is called before the first frame update
    private void Start()
    {
        currentState = EPlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", 1);
        Vector2 startPos = scenePositions.GetStartPos();
        transform.position = startPos;
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentState == EPlayerState.interact)
            return;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != EPlayerState.attack && currentState != EPlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == EPlayerState.walk || currentState == EPlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = EPlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != EPlayerState.interact)
            currentState = EPlayerState.walk;
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != EPlayerState.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = EPlayerState.interact;
                receiveItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receiveItem", false);
                currentState = EPlayerState.idle;
                receiveItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    private void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    private void MoveCharacter()
    {
        change.Normalize();

        myRigidBody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.runTimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.runTimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        playerHit.Raise();
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = EPlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}