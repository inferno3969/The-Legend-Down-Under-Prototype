using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    public Slider magicSlider;
    public Inventory playerInventory;

    void Start()
    {
        magicSlider.maxValue = playerInventory.currentMagic;
        magicSlider.value = playerInventory.currentMagic;
    }

    public void AddMagic()
    {
        magicSlider.value = playerInventory.currentMagic;
        if (magicSlider.value > magicSlider.maxValue)
        {
            magicSlider.value = magicSlider.maxValue;
            playerInventory.currentMagic = playerInventory.maxMagic;
        }
    }

    public void DecreaseMagic()
    {
        magicSlider.value = playerInventory.currentMagic;
        if(magicSlider.value < 0)
        {
            magicSlider.value = 0;
            playerInventory.currentMagic = 0;
        }
    }
}
