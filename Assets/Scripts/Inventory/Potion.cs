using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionTypes { AntiVenom, Bastion, Magic, Prayer, Ranging }

[CreateAssetMenu(fileName ="Potion", menuName ="Inventory/Potion")]
public class Potion : Item
{
    public Potion(Potion defaultPotion)
    {
        name = defaultPotion.name;
        icon = defaultPotion.icon;


        dose4 = defaultPotion.dose4;
        dose3 = defaultPotion.dose3;
        dose2 = defaultPotion.dose2;
        dose1 = defaultPotion.dose1;

        hasDuration = defaultPotion.hasDuration;
        type = defaultPotion.type;
    }

    public Sprite dose4;
    public Sprite dose3;
    public Sprite dose2;
    public Sprite dose1;
    public bool hasDuration;
    public PotionTypes type;

    public int doses = 4;

    public override void Use(bool inGame)
    {
        base.Use(inGame);
        doses--;
        if(doses <= 0)
        {
            Inventory.instance.Remove(this);
            doses = 4;
            icon = dose4;
        }
        else if(doses == 3)
        {
            icon = dose3;
        }
        else if (doses == 2)
        {
            icon = dose2;
        }
        else if (doses == 1)
        {
            icon = dose1;
        }
        ConsumableManager.instance.Consumable(this);
    }

    public void Effect(PlayerStats player)
    {
        switch (type)
        {
            case PotionTypes.Ranging:
                player.ranged.potion = 4 + Mathf.FloorToInt(player.ranged.baseValue * 0.1f);
                break;

            case PotionTypes.Magic:
                player.magic.potion = 4;
                break;

            case PotionTypes.Bastion:
                player.ranged.potion = 4 + Mathf.FloorToInt(player.ranged.baseValue * 0.1f);
                player.defence.potion = 5 + Mathf.FloorToInt(player.defence.baseValue * 0.15f);
                break;

            case PotionTypes.Prayer:
                int points = 7 + Mathf.FloorToInt(player.maxPrayerPoints / 4f);
                player.rechargePrayer(points);
                break;

            case PotionTypes.AntiVenom:
                player.antiVenomActive = true;
                break;
        }
    }
}
