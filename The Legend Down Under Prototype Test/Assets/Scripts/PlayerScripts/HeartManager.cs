using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        // since UpdateHearts() method calls the InitHearts() method,
        // we want to call the UpdateHearts() method in the Start() method
        // in order show the correct heart sprite when transitioning from scene
        // to scene
        UpdateHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            if (playerCurrentHealth.RuntimeValue == playerCurrentHealth.initialValue)
            {
                hearts[i].sprite = fullHeart;
            }
        }
    }

    public void UpdateHearts()
    {
        InitHearts();
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            if (i <= tempHealth - 1)
            {
                // full heart
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                // empty heart
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                // half-emtpy
                hearts[i].sprite = halfFullHeart;
            }
        }
    }
}
