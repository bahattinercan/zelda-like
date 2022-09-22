using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isEnemy = other.CompareTag("Enemy");
        bool isPlayer = other.CompareTag("Player");
        if (other.CompareTag("Breakable") && gameObject.CompareTag("Hitbox"))
        {
            other.GetComponent<Pot>().Smash();
        }
        else if (isEnemy || isPlayer)
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 difference = rb.transform.position - transform.position;
                difference = difference.normalized * thrust;
                rb.AddForce(difference, ForceMode2D.Impulse);

                if (isEnemy && other.isTrigger)
                {
                    rb.GetComponent<Enemy>().currentState = EEnemyState.stagger;
                    rb.GetComponent<Enemy>().Knock(rb, knockTime, damage);
                }
                else if (isPlayer && other.isTrigger)
                {
                    Player player = other.GetComponent<Player>();
                    if (player.currentState != EPlayerState.stagger)
                    {
                        player.currentState = EPlayerState.stagger;
                        player.Knock(knockTime, damage);
                    }
                }
            }
        }
    }
}