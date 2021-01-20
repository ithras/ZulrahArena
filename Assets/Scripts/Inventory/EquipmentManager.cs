using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	#region Singleton
	public static EquipmentManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More tha one instance of Equipment found!");
            return;
        }
        instance = this;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        SaveSystem.LoadEquipment();
    }
    #endregion

    public Equipment[] currentEquipment;

    public event System.Action<Equipment, Equipment> onEquipmentChanged;
    public event System.Action onEquipmentChangedUI;

    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;
    }

    public string Equip(Equipment newItem, bool inGame)
    {
        int slotIndex = (int)newItem.equipSlot;
        string operation;

        Equipment equippedItem = null;

        if (currentEquipment[slotIndex] == null)
        {
            currentEquipment[slotIndex] = newItem;
            operation = "Remove";
        }
        else
        {
            equippedItem = currentEquipment[slotIndex];
            equippedItem.assignedSlot = newItem.assignedSlot;
            currentEquipment[slotIndex] = newItem;

            if(inGame)
                inventory.Swap(equippedItem);

            operation = "Swap";
        }

        onEquipmentChanged.Invoke(newItem, equippedItem);

        if(!inGame)
            onEquipmentChangedUI.Invoke();

        return operation;
    }
}
