using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneralStats : ScriptableObject
{
    public Skill defence;
    public Skill magic;
    public Skill ranged;

    public int maxHitpoints;
    public int currentHitpoints;

    public Stat stabAtk;
    public Stat slashAtk;
    public Stat crushAtk;
    public Stat magicAtk;
    public Stat rangeAtk;

    public Stat stabDef;
    public Stat slashDef;
    public Stat crushDef;
    public Stat magicDef;
    public Stat rangeDef;

    public Stat meleeStr;
    public Stat rangeStr;
    public Stat magicStr;

    public int RangedAtkRoll;
    public int RangedMaxHit;
    public int MagicAtkRoll;
    public int MagicMaxHit;
    public int StabDefenceRoll;
    public int SlashDefenceRoll;
    public int CrushDefenceRoll;
    public int MagicDefenceRoll;
    public int RangedDefenceRoll;

    virtual protected void Awake()
    {
        currentHitpoints = maxHitpoints;
    }

    virtual protected void Start()
    {
        OnBonusChanged();
    }

    protected virtual void OnBonusChanged()
    {
        RangedAtkRoll = (ranged.effectiveValue(true) + 8) * (rangeAtk.GetValue() + 64);
        RangedMaxHit = Mathf.FloorToInt(1.3f + (ranged.effectiveValue(false) / 10f) + (rangeStr.GetValue() / 80f) + ((ranged.effectiveValue(true) * rangeStr.GetValue()) / 640f));
        MagicAtkRoll = magic.effectiveValue(true) * (magicAtk.GetValue() + 64);

        StabDefenceRoll = defence.effectiveValue(true) * (stabDef.GetValue() + 64);
        SlashDefenceRoll = defence.effectiveValue(true) * (slashDef.GetValue() + 64);
        CrushDefenceRoll = defence.effectiveValue(true) * (crushDef.GetValue() + 64);

        RangedDefenceRoll = defence.effectiveValue(true) * (rangeDef.GetValue() + 64);
        MagicDefenceRoll = Mathf.FloorToInt((magic.effectiveValue(true) * 0.7f) + (defence.effectiveValue(true) * 0.3f) + 8) * (magicDef.GetValue() + 64);
    }

    public void TakeDamage(int damage)
    {
        currentHitpoints -= damage;
        currentHitpoints = Mathf.Clamp(currentHitpoints, 0, maxHitpoints);

        if(currentHitpoints <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        //Destroy(gameObject);
    }
}
