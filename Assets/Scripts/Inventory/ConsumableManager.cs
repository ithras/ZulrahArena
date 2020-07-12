using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableManager : MonoBehaviour
{
	#region Singleton
	public static ConsumableManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	public PlayerStats player;
    public float timerSeconds = 60f;

    List<Potion> consumableEffects = new List<Potion>();
    bool timerIsActive;

    public void Consumable(Potion pot)
    {
        Inventory.instance.Consumable(pot);
        pot.Effect(player);
        
        if(pot.hasDuration)
        {
            if (pot.type == PotionTypes.AntiVenom)
                StartCoroutine(AntiVenom());

            else if (!consumableEffects.Contains(pot))
                consumableEffects.Add(pot);

            if (!timerIsActive)
            {
                timerIsActive = true;
                StartCoroutine(TemporaryBoostTimer(consumableEffects));
            }
        }
    }
    
    IEnumerator TemporaryBoostTimer(List<Potion> potions)
    {

        bool rangingOnUse = false;
        bool bastionOnUse = false;

        while (player.magic.potion > 0 || player.ranged.potion > 0 || player.defence.potion > 0)
        {
            List<Potion> readyToRemove = new List<Potion>();

            yield return new WaitForSeconds(timerSeconds);

            for (int i = 0; i < potions.Count; i++)
            {
                if (potions[i].type == PotionTypes.Magic)
                {
                    player.magic.potion --;
                    if (player.magic.potion <= 0)
                    {
                        player.magic.potion = 0;
                        readyToRemove.Add(potions[i]);
                    }
                }
                else if (potions[i].type == PotionTypes.Ranging)
                {
                    if (!bastionOnUse)
                    {
                        rangingOnUse = true;
                        player.ranged.potion--;
                        if(player.ranged.potion <= 0)
                        {
                            player.ranged.potion = 0;
                            readyToRemove.Add(potions[i]);
                        }
                    }
                }
                else if (potions[i].type == PotionTypes.Bastion)
                {
                    if (!rangingOnUse)
                    {
                        if(player.ranged.potion > 0)
                            player.ranged.potion--;

                        if(player.defence.potion > 0)
                            player.defence.potion--;

                        if (player.defence.potion <= 0 && player.ranged.potion <= 0)
                        {
                            player.defence.potion = 0;
                            player.ranged.potion = 0;
                            readyToRemove.Add(potions[i]);
                        }

                        bastionOnUse = true;
                    }
                    else
                    {
                        player.defence.potion--;
                        if (player.defence.potion <= 0)
                        {
                            player.defence.potion = 0;
                            readyToRemove.Add(potions[i]);
                        }
                    }
                    
                }
            }

            foreach(var potion in readyToRemove)
            {
                potions.Remove(potion);
            }
        }

        timerIsActive = false;
    }

    IEnumerator AntiVenom()
    {
        yield return new WaitForSeconds(180f);
        player.antiVenomActive = false;
    }
}
