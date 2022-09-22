using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    // Start is called before the first frame update
    private void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            Image element = hearts[i];
            element.gameObject.SetActive(true);
            element.sprite = fullHeart;
        }
    }

    public void UpdateHearts()
    {
        float tempHealth = playerCurrentHealth.runTimeValue / 2;
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            Image element = hearts[i];
            // full health
            if (i <= tempHealth - 1)
            {
                element.sprite = fullHeart;
            }
            // empty health
            else if (i >= tempHealth)
            {
                element.sprite = emptyHeart;
            }
            // half health
            else
            {
                element.sprite = halfHeart;
            }
        }
    }
}