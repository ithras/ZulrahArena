using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuUI : MonoBehaviour
{
    public void SaveInventory()
    {
        SaveSystem.SaveInventory();
    }

    public void LoadInventory()
    {
        InventoryData data = SaveSystem.LoadInventory();

        for (int i = 0; i < data.inventory.Length; i++)
        {
            if (data.inventory[i] != null)
            {
                Inventory.instance.Add(Resources.Load(data.inventory[i]) as Item);
            }
        }
    }
}
