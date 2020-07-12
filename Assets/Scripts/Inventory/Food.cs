using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Food", menuName ="Inventory/Food")]
public class Food : Item
{
    public int healPoints;

    public override void Use(bool inGame)
    {
        base.Use(inGame);
        ConsumableManager.instance.player.healPlayer(healPoints);
    }
}
