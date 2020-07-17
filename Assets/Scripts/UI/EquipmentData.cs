using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentData
{
    public string[] equipment = new string[11];
    public string[] equipmentSlot = new string[11];

    public EquipmentData()
    {
        for (int i = 0; i < EquipmentManager.instance.currentEquipment.Length; i++)
        {
            if(EquipmentManager.instance.currentEquipment[i] != null)
            {
                equipment[i] = EquipmentManager.instance.currentEquipment[i].name;
                equipmentSlot[i] = EquipmentManager.instance.currentEquipment[i].equipSlot.ToString();
                //Debug.Log(equipmentSlot[i]);
            }
        }
    }
}
