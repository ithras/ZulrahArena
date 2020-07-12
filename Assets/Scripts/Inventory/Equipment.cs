using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot { Ammo, Amulet, Boots, Cape, Chest, Gloves, Head, Legs, Offhand, Ring, Weapon }

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int stabAtk;
    public int slashAtk;
    public int crushAtk;
    public int magicAtk;
    public int rangeAtk;

    public int stabDef;
    public int slashDef;
    public int crushDef;
    public int magicDef;
    public int rangeDef;

    public int meleeStr;
    public int rangeStr;
    public int magicStr;
    
    public int prayerBonus;

    public float attackSpeed;

    public override void Use(bool inGame)
    {
        base.Use(inGame);
        string operation = EquipmentManager.instance.Equip(this, inGame);

        if (operation == "Remove" && inGame)
            RemoveFromInventory();
    }
}
