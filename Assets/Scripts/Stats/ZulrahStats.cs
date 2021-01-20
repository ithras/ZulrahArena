using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class ZulrahStats : GeneralStats
{
    [SerializeField]
    public ProjectileType atkType;

    public void Initiate()
    {
        base.Start();
        maxHitpoints = 500;
        defence.baseValue = 300;
        ranged.baseValue = 300;
        magicAtk.AddModifier(50, 0);
        magicStr.AddModifier(20, 0);
        rangeAtk.AddModifier(50, 0);
        rangeStr.AddModifier(20, 0);
    }

    public void OnZulrahBonusChanged()
    {
        OnBonusChanged();
    }
}
