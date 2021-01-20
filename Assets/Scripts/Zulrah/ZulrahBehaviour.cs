using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZulrahPhase { Range, Magic, Melee }
public class ZulrahBehaviour : MonoBehaviour
{
    ZulrahStats stats;
    public ZulrahPhase phase;

    void Start()
    {
        stats = (ZulrahStats) ScriptableObject.CreateInstance(typeof(ZulrahStats));
        stats.Initiate();
    }

    public void RangeStats()
    {
        phase = ZulrahPhase.Range;
        stats.magicDef.ResetModifiers();
        stats.rangeDef.ResetModifiers();
        stats.magicDef.AddModifier(-45, 0);
        stats.rangeDef.AddModifier(50, 0);
        stats.OnZulrahBonusChanged();
    }

    public void MagicStats()
    {
        phase = ZulrahPhase.Magic;
        stats.magicDef.ResetModifiers();
        stats.rangeDef.ResetModifiers();
        stats.magicDef.AddModifier(300, 1);
        stats.OnZulrahBonusChanged();
    }

    public void MeleeStats()
    {
        phase = ZulrahPhase.Melee;
        stats.magicDef.ResetModifiers();
        stats.rangeDef.ResetModifiers();
        stats.rangeDef.AddModifier(300, 2);
        stats.OnZulrahBonusChanged();
    }
}
