using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue = 0;

    private int[] modifiers = new int[11];

    public void ResetModifiers() => modifiers = new int[11];

    public int GetValue()
    {
        int finalValue = baseValue;

        for (int i = 0; i < modifiers.Length; i++)
        {
            finalValue += modifiers[i];
        }

        return finalValue;
    }

    public void AddModifier(int modifier, int equipSlot)
    {
        modifiers[equipSlot] = modifier;
    }

    public void RemoveModifier(int equipSlot)
    {
        modifiers[equipSlot] = 0;
    }
}
