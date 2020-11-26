using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public int maxPrayerPoints;
    public int CurrentPrayerPoints { get; private set; }

    public float rangedAtkBonus;
    public float rangedStrBonus;

    public int RangedAtkRoll { get; private set; }
    public int RangedMaxHit { get; private set; }
    public int MagicAtkRoll { get; private set; }
    public int MagicMaxHit { get; private set; }
    public int StabDefenceRoll { get; private set; }
    public int SlashDefenceRoll { get; private set; }
    public int CrushDefenceRoll { get; private set; }
    public int MagicDefenceRoll { get; private set; }
    public int RangedDefenceRoll { get; private set; }

    public PrayerProtectTypes activeProtect;

    public bool antiVenomActive = false;

    public ProjectileType atkType;

    void Awake()
    {
        currentHitpoints = maxHitpoints;
        CurrentPrayerPoints = maxPrayerPoints;
    }

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        if(PrayerManager.instance != null)
        {
            PrayerManager.instance.OnPrayerChanged += OnBonusChanged;
            PrayerManager.instance.OnPrayerChangedRigour += OnBonusChangedRigour;
        }
        
        activeProtect = PrayerProtectTypes.None;

        OnBonusChanged();
    }

    void OnBonusChanged()
    {
        Debug.Log("Bonuses");
        string weaponName = "";
        if (EquipmentManager.instance.currentEquipment[(int)EquipmentSlot.Weapon] != null)
            weaponName = EquipmentManager.instance.currentEquipment[(int)EquipmentSlot.Weapon].name;

        RangedAtkRoll = (ranged.effectiveValue(true) + 8) * ( rangeAtk.GetValue() + 64 );
        RangedMaxHit = Mathf.FloorToInt(1.3f + (ranged.effectiveValue(false) / 10f) + (rangeStr.GetValue() / 80f) + ((ranged.effectiveValue(true) * rangeStr.GetValue()) / 640f));
        MagicAtkRoll = magic.effectiveValue(true) * (magicAtk.GetValue() + 64);

        if (weaponName == "Trident of the Seas")
            MagicMaxHit = Mathf.FloorToInt(((magic.baseValue / 3f) - 5) * (1 + (magicStr.GetValue() / 100f)));

        else if (weaponName == "Trident of the Swamp")
            MagicMaxHit = Mathf.FloorToInt(((magic.baseValue / 3f) - 2) * (1 + (magicStr.GetValue() /100f)));

        else
            MagicMaxHit = 0;

        StabDefenceRoll = defence.effectiveValue(true) * (stabDef.GetValue() + 64);
        SlashDefenceRoll = defence.effectiveValue(true) * (slashDef.GetValue() + 64);
        CrushDefenceRoll = defence.effectiveValue(true) * (crushDef.GetValue() + 64);

        RangedDefenceRoll = defence.effectiveValue(true) * (rangeDef.GetValue() + 64);
        MagicDefenceRoll = Mathf.FloorToInt((magic.effectiveValue(true) * 0.7f) + (defence.effectiveValue(true) * 0.3f) + 8) * (magicDef.GetValue() + 64);
    }

    void OnBonusChangedRigour()
    {
        RangedAtkRoll = ranged.effectiveValue(rangedAtkBonus) * (rangeAtk.GetValue() + 64);
        RangedMaxHit = Mathf.FloorToInt(1.3f + (ranged.effectiveValue(rangedStrBonus) / 10) + (rangeStr.GetValue() / 80) + ((ranged.effectiveValue(rangedStrBonus) * rangeStr.GetValue()) / 640));
        MagicAtkRoll = magic.effectiveValue(true) * (magicAtk.GetValue() + 64);

        StabDefenceRoll = defence.effectiveValue(true) * (stabDef.GetValue() + 64);
        SlashDefenceRoll = defence.effectiveValue(true) * (slashDef.GetValue() + 64);
        CrushDefenceRoll = defence.effectiveValue(true) * (crushDef.GetValue() + 64);
        RangedDefenceRoll = defence.effectiveValue(true) * (rangeDef.GetValue() + 64);
        MagicDefenceRoll = Mathf.FloorToInt((magic.effectiveValue(true) * 0.7f) + (defence.effectiveValue(true) * 0.3f) + 8) * (magicDef.GetValue() + 64);
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (oldItem != null)
        {
            int oldItemEquipSlot = (int)newItem.equipSlot;

            stabDef.RemoveModifier(oldItemEquipSlot);
            slashDef.RemoveModifier(oldItemEquipSlot);
            crushDef.RemoveModifier(oldItemEquipSlot);
            magicDef.RemoveModifier(oldItemEquipSlot);
            rangeDef.RemoveModifier(oldItemEquipSlot);

            stabAtk.RemoveModifier(oldItemEquipSlot);
            slashAtk.RemoveModifier(oldItemEquipSlot);
            crushAtk.RemoveModifier(oldItemEquipSlot);
            magicAtk.RemoveModifier(oldItemEquipSlot);
            rangeAtk.RemoveModifier(oldItemEquipSlot);

            meleeStr.RemoveModifier(oldItemEquipSlot);
            rangeStr.RemoveModifier(oldItemEquipSlot);
            magicStr.RemoveModifier(oldItemEquipSlot);

            prayerBonus.RemoveModifier(oldItemEquipSlot);
        }

        if (newItem != null)
        {
            if (newItem.equipSlot == EquipmentSlot.Weapon)
            {
                if (newItem.magicAtk > newItem.rangeAtk)
                    atkType = ProjectileType.Magic;

                else if (newItem.rangeAtk > newItem.magicAtk)
                    atkType = ProjectileType.Ranged;
            }

            int newItemEquipSlot = (int)newItem.equipSlot;
            
            stabDef.AddModifier(newItem.stabDef, newItemEquipSlot);
            slashDef.AddModifier(newItem.slashDef, newItemEquipSlot);
            crushDef.AddModifier(newItem.crushDef, newItemEquipSlot);
            magicDef.AddModifier(newItem.magicDef, newItemEquipSlot);
            rangeDef.AddModifier(newItem.rangeDef, newItemEquipSlot);

            stabAtk.AddModifier(newItem.stabAtk, newItemEquipSlot);
            slashAtk.AddModifier(newItem.slashAtk, newItemEquipSlot);
            crushAtk.AddModifier(newItem.crushAtk, newItemEquipSlot);
            magicAtk.AddModifier(newItem.magicAtk, newItemEquipSlot);
            rangeAtk.AddModifier(newItem.rangeAtk, newItemEquipSlot);

            meleeStr.AddModifier(newItem.meleeStr, newItemEquipSlot);
            rangeStr.AddModifier(newItem.rangeStr, newItemEquipSlot);
            magicStr.AddModifier(newItem.magicStr, newItemEquipSlot);

            prayerBonus.AddModifier(newItem.prayerBonus, newItemEquipSlot);
        }

        OnBonusChanged();
    }

    public void rechargePrayer(int rechargePoints)
    {
        CurrentPrayerPoints = Mathf.Min(CurrentPrayerPoints + rechargePoints, maxPrayerPoints);
    }

    public void drainPrayer(int pointsToDrain)
    {
        CurrentPrayerPoints = Mathf.Max(CurrentPrayerPoints - pointsToDrain, 0);
    }

    public void healPlayer(int pointsToHeal)
    {
        currentHitpoints = Mathf.Min(currentHitpoints + pointsToHeal, maxHitpoints);
    }
}
