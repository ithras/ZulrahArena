using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
	public string[] inventory = new string[28];

	public InventoryData()
	{
		for (int i = 0; i < Inventory.instance.items.Length; i++)
		{
			inventory[i] = Inventory.instance.items[i].name;
		}
	}
}
