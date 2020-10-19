using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownSlot : MonoBehaviour
{
    public TMP_Dropdown assignedDropdown;
    public string key;

    public void AddToInventory()
    {
        if (assignedDropdown.captionText.text != "Select . . .")
            Inventory.instance.Add(DropdownManager.instance.InventoryMap[key][assignedDropdown.captionText.text]);
    }
}
