using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int numberOfBossKeys;

    public void AddItem(Item itemToAdd)
    {
        // is the item a key?
        if (itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else if (itemToAdd.isBossKey)
        {
            numberOfBossKeys++;
        }
        else
        {
            if (!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }
}
