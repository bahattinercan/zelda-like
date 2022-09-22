using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isActive;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer spriteRenderer;
    public Door thisDoor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isActive = storedValue.runTimeValue;
        Debug.Log(isActive);
        if (isActive)
        {
            ActivateSwitch();   
        }
    }
    
    public void ActivateSwitch()
    {
        isActive = true;
        storedValue.runTimeValue = isActive;
        thisDoor.Open();
        spriteRenderer.sprite = activeSprite;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }
}
