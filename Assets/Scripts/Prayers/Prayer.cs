using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public enum PrayerTypes { None, StatAugment, Protect, Ultimate}
public enum PrayerAugmentTypes { Defence, Strenght, Attack, Range, Magic }
public enum PrayerProtectTypes { None, Magic, Range, Melee }
public enum PrayerUltimateTypes { Chivalry, Piety, Rigour, Augury }

public class Prayer : MonoBehaviour
{
    public Sprite icon;
    public Image image;
    public Image background;
    public Button button;
    public int lvlUnlock;
    public float drain;
    public PrayerTypes type;
    public bool isAugment;
    public bool isProtect;
    public bool isUltimate;

    public bool isActive;
    public bool isUnlocked;

    [ShowIf("isAugment")]
    public PrayerAugmentTypes augmentType;
    [ShowIf("isAugment")]
    public int percentage;

    [ShowIf("isProtect")]
    public PrayerProtectTypes protectType;

    [ShowIf("isUltimate")]
    public PrayerUltimateTypes ultimateType;

    public void Use()
    {
        PrayerManager.instance.ManagePrayer(this);
    }
}
