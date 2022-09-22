using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [Header("Door Variables")]
    public EDoor eDoor;
    public bool isOpen;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerInRange && eDoor== EDoor.key)
            {
                if (playerInventory.keys > 0)
                {
                    playerInventory.keys--;
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        doorSprite.enabled = false;
        isOpen = true;
        physicsCollider.enabled = false;

    }

    public void Close()
    {

    }
}
