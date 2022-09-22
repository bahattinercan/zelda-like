using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EEnemyState currentState;
    public FloatValue MaxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffect;
    private float deathEffectDelay = .95f;

    private void Awake()
    {
        health = MaxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeadEffect();
            gameObject.SetActive(false);
        }
    }

    private void DeadEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect,deathEffectDelay);
        }
    }

    public void Knock(Rigidbody2D rb, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(rb, knockTime));
        TakeDamage(damage);
    }

    private IEnumerator KnockCo(Rigidbody2D rb, float knockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            currentState = EEnemyState.idle;
            rb.velocity = Vector2.zero;
        }
    }
}