using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{
    public FloatValue heartContainers;
    public FloatValue playerHealth;
    public float amountToIncrease;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")&& !other.isTrigger)
        {
            playerHealth.runTimeValue += amountToIncrease;
            if (playerHealth.initialValue > heartContainers.runTimeValue * 2)
            {
                playerHealth.initialValue = heartContainers.runTimeValue * 2;
            }
            powerUpSignal.Raise();
            Destroy(gameObject);
        }
    }
}
