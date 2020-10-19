using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipmentMenuUI : MonoBehaviour
{
    public PlayerStats statsBonuses;

    public TMP_Text stabAtk;
    public TMP_Text slashAtk;
    public TMP_Text crushAtk;
    public TMP_Text magicAtk;
    public TMP_Text rangeAtk;

    public TMP_Text stabDef;
    public TMP_Text slashDef;
    public TMP_Text crushDef;
    public TMP_Text magicDef;
    public TMP_Text rangeDef;

    public TMP_Text meleeStr;
    public TMP_Text rangeStr;
    public TMP_Text magicStr;

    public TMP_Text prayerBonus;

    EquipmentManager equipmentManager;

    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChangedUI += onEquipmentChanged;
    }

    void onEquipmentChanged()
    {
        stabAtk.text = valueToText(statsBonuses.stabAtk.GetValue());
        slashAtk.text = valueToText(statsBonuses.slashAtk.GetValue());
        crushAtk.text = valueToText(statsBonuses.crushAtk.GetValue());
        magicAtk.text = valueToText(statsBonuses.magicAtk.GetValue());
        rangeAtk.text = valueToText(statsBonuses.rangeAtk.GetValue());

        stabDef.text = valueToText(statsBonuses.stabDef.GetValue());
        slashDef.text = valueToText(statsBonuses.slashDef.GetValue());
        crushDef.text = valueToText(statsBonuses.crushDef.GetValue());
        magicDef.text = valueToText(statsBonuses.magicDef.GetValue());
        rangeDef.text = valueToText(statsBonuses.rangeDef.GetValue());

        meleeStr.text = valueToText(statsBonuses.meleeStr.GetValue());
        rangeStr.text = valueToText(statsBonuses.rangeStr.GetValue());
        magicStr.text = valueToText(statsBonuses.magicStr.GetValue());

        prayerBonus.text = valueToText(statsBonuses.prayerBonus.GetValue());
    }

    string valueToText(int value)
    {
        if (value >= 0)
            return "+" + value.ToString();

        else
            return value.ToString();
    }

    public void SaveEquipment()
    {
        SaveSystem.SaveEquipment();

    }

    public void LoadEquipment()
    {
        EquipmentData data = SaveSystem.LoadEquipment();

        for (int i = 0; i < data.equipment.Length; i++)
        {
            if (data.equipment[i] != null)
            {
                equipmentManager.Equip(Resources.Load(data.equipmentSlot[i] + "/" + data.equipment[i]) as Equipment, false);
                DropdownManager.instance.LoadEquipment(data.equipment[i], data.equipmentSlot[i]);
            }
        }

        
    }
}
