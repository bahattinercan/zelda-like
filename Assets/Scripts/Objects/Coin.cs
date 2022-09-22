using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    public Inventory playerInventory;

    private void Start()
    {
        powerUpSignal.Raise();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.coins += 1;
            powerUpSignal.Raise();
            Destroy(gameObject);
        }
    }
}
