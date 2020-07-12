using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class PrayerManager : MonoBehaviour
{
	#region Singleton
	public static PrayerManager instance;

	void Awake()
	{
		instance = this;
	}

	#endregion
	public PlayerStats player;
	public event Action OnPrayerChanged;
	public event Action OnPrayerChangedRigour;
	public Prayer dummy;

	List<Prayer> activePrayers = new List<Prayer>();
	Dictionary<float, bool> activePrayersDrainRates = new Dictionary<float, bool>();
	Dictionary<float, int> pointsToDrain;

	public void ManagePrayer(Prayer prayerToActivate)
	{
		if (player.CurrentPrayerPoints > 0)
		{
			prayerToActivate.background.enabled = true;
			prayerToActivate.isActive = !prayerToActivate.isActive;
			List<Prayer> prayersToDesactivate = new List<Prayer>();

			if (activePrayers.Count == 0)
				activePrayers.Add(dummy);

			for (int i = 0; i < activePrayers.Count; i++)
			{
				if (prayerToActivate.isAugment)
				{
					if (prayerToActivate == activePrayers[i])
					{
						statAugment(activePrayers[i].augmentType, 0);
						prayersToDesactivate.Add(activePrayers[i]);
					}
					PrayerAugmentTypes augment = prayerToActivate.augmentType;
					if (activePrayers[i].isAugment || activePrayers[i].isUltimate)
					{
						if (activePrayers[i].isAugment)
						{

							if (augment == activePrayers[i].augmentType)
								prayersToDesactivate.Add(activePrayers[i]);

							else
							{
								if (augment == PrayerAugmentTypes.Attack || augment == PrayerAugmentTypes.Strenght)
								{
									if (activePrayers[i].augmentType == PrayerAugmentTypes.Magic || activePrayers[i].augmentType == PrayerAugmentTypes.Range)
									{
										prayersToDesactivate.Add(activePrayers[i]);
										statAugment(activePrayers[i].augmentType, 0);
									}
								}
								else if (augment == PrayerAugmentTypes.Magic || augment == PrayerAugmentTypes.Range)
								{
									if (activePrayers[i].augmentType != PrayerAugmentTypes.Defence)
									{
										prayersToDesactivate.Add(activePrayers[i]);
										statAugment(activePrayers[i].augmentType, 0);
									}
								}
							}
						}
						else
						{
							if (augment == PrayerAugmentTypes.Defence)
							{
								disableUltimate(activePrayers[i].ultimateType);
								prayersToDesactivate.Add(activePrayers[i]);
							}
							else if (augment == PrayerAugmentTypes.Attack || augment == PrayerAugmentTypes.Strenght)
							{
								if (activePrayers[i].ultimateType == PrayerUltimateTypes.Chivalry || activePrayers[i].ultimateType == PrayerUltimateTypes.Piety)
								{
									disableUltimate(activePrayers[i].ultimateType);
									prayersToDesactivate.Add(activePrayers[i]);
								}
							}
							else if (augment == PrayerAugmentTypes.Magic)
							{
								if (activePrayers[i].ultimateType == PrayerUltimateTypes.Augury)
								{
									disableUltimate(activePrayers[i].ultimateType);
									prayersToDesactivate.Add(activePrayers[i]);
								}
							}
							else
							{
								if (activePrayers[i].ultimateType == PrayerUltimateTypes.Rigour)
								{
									disableUltimate(activePrayers[i].ultimateType);
									prayersToDesactivate.Add(activePrayers[i]);
								}
							}
						}
					}
					if (prayerToActivate.isActive)
						statAugment(prayerToActivate.augmentType, prayerToActivate.percentage);
				}
				else if (prayerToActivate.isProtect)
				{
					if (prayerToActivate == activePrayers[i])
					{
						protect(PrayerProtectTypes.None);
						prayersToDesactivate.Add(activePrayers[i]);
					}
					else if (activePrayers[i].isProtect)
					{
						protect(prayerToActivate.protectType);
						prayersToDesactivate.Add(activePrayers[i]);
					}
				}
				else if (prayerToActivate.isUltimate)
				{
					if (prayerToActivate == activePrayers[i])
					{
						disableUltimate(activePrayers[i].ultimateType);
						prayersToDesactivate.Add(activePrayers[i]);
					}
					else if (activePrayers[i].isUltimate || activePrayers[i].isAugment)
					{
						if (activePrayers[i].isAugment)
						{
							statAugment(activePrayers[i].augmentType, 0);
							prayersToDesactivate.Add(activePrayers[i]);
						}
						else
						{
							disableUltimate(activePrayers[i].ultimateType);
							prayersToDesactivate.Add(activePrayers[i]);
						}
					}
					if (prayerToActivate.isActive)
					{
						ultimate(prayerToActivate.ultimateType);
					}
				}
			}

			for (int i = 0; i < prayersToDesactivate.Count; i++)
			{
				prayersToDesactivate[i].background.enabled = false;
				prayersToDesactivate[i].isActive = false;
				activePrayers.Remove(prayersToDesactivate[i]);
			}

			if (prayerToActivate.isActive)
			{
				//Debug.Log(prayerToActivate.name + " is active?: " + prayerToActivate.isActive);
				activePrayers.Add(prayerToActivate);
			}

			drainHandler();
		}
		else
		{
			foreach(var prayer in activePrayers)
			{
				prayer.isActive = false;
				prayer.background.enabled = false;
			}
			activePrayers = new List<Prayer>();
			activePrayersDrainRates = new Dictionary<float, bool>();
		}

	}

	void drainHandler()
	{
		pointsToDrain = new Dictionary<float, int>();

		if (activePrayers.Count > 0)
		{
			foreach (var prayer in activePrayers)
			{
				if(prayer.drain > 0)
				{
					if(pointsToDrain.TryGetValue(prayer.drain, out int points))
						pointsToDrain[prayer.drain] = points + 1;

					else
						pointsToDrain.Add(prayer.drain, 1);

					if (!activePrayersDrainRates.TryGetValue(prayer.drain, out bool active))
						activePrayersDrainRates.Add(prayer.drain, false);
				}
			}
		}

		foreach (var drainRate in pointsToDrain)
		{   
			if(activePrayersDrainRates.TryGetValue(drainRate.Key, out bool active))
				if (!active)
				{
					activePrayersDrainRates[drainRate.Key] = true;
					StartCoroutine(drainPrayerPoints(drainRate.Key));
				}
		}
	}

	IEnumerator drainPrayerPoints(float drainRate)
	{
		float modifiedDrainRate = drainRate * (1 + (player.prayerBonus.GetValue() / 30f));
		//Debug.Log("modified drain rate:" + modifiedDrainRate);

		while(player.CurrentPrayerPoints > 0 && activePrayersDrainRates[drainRate])
		{
			yield return new WaitForSeconds(modifiedDrainRate);

			if (pointsToDrain.Count > 0)
				player.drainPrayer(pointsToDrain[drainRate]);

			else
				activePrayersDrainRates[drainRate] = false;
		}
		activePrayersDrainRates.Remove(drainRate);
	}

	void statAugment(PrayerAugmentTypes type, int percentage)
	{
		switch(type)
		{
			case PrayerAugmentTypes.Defence:
				player.defence.prayer = (percentage / 100f) +1;
				break;

			case PrayerAugmentTypes.Magic:
				player.magic.prayer = (percentage / 100f) + 1;
				break;

			case PrayerAugmentTypes.Range:
				player.ranged.prayer = (percentage / 100f) + 1;
				break;
		}

		OnPrayerChanged.Invoke();
	}

	void protect(PrayerProtectTypes type)
	{
		player.activeProtect = type;
	}

	void ultimate(PrayerUltimateTypes type)
	{
		switch (type)
		{
			case PrayerUltimateTypes.Chivalry:
				player.defence.prayer = 1.15f;
				break;

			case PrayerUltimateTypes.Piety:
				player.defence.prayer = 1.25f;
				break;

			case PrayerUltimateTypes.Rigour:
				player.rangedAtkBonus = 1.20f;
				player.rangedStrBonus = 1.23f;
				player.defence.prayer = 1.25f;
				break;

			case PrayerUltimateTypes.Augury:
				player.magic.prayer = 1.25f;
				player.defence.prayer = 1.25f;
				break;
		}

		if(type == PrayerUltimateTypes.Rigour)
			OnPrayerChangedRigour.Invoke();
		else
			OnPrayerChanged.Invoke();
	}

	void disableUltimate(PrayerUltimateTypes type)
	{
		switch (type)
		{
			case PrayerUltimateTypes.Chivalry:
				player.defence.prayer = 1;
				break;

			case PrayerUltimateTypes.Piety:
				player.defence.prayer = 1;
				break;

			case PrayerUltimateTypes.Rigour:
				player.rangedAtkBonus = 1;
				player.rangedStrBonus = 1;
				player.defence.prayer = 1;
				break;

			case PrayerUltimateTypes.Augury:
				player.magic.prayer = 1;
				player.defence.prayer = 1;
				break;
		}

		OnPrayerChanged.Invoke();
	}

}


/*
							if (activePrayers[i].augmentType == PrayerAugmentTypes.Defence)
							{
								statAugment(activePrayers[i].augmentType, 0);
								prayersToDesactivate.Add(activePrayers[i]);
							}
							else if (prayerToActivate.ultimateType == PrayerUltimateTypes.Chivalry || prayerToActivate.ultimateType == PrayerUltimateTypes.Piety)
							{
								if (activePrayers[i].augmentType == PrayerAugmentTypes.Attack || activePrayers[i].augmentType == PrayerAugmentTypes.Strenght)
								{
									statAugment(activePrayers[i].augmentType, 0);
									prayersToDesactivate.Add(activePrayers[i]);
								}
							}
							else if (prayerToActivate.ultimateType == PrayerUltimateTypes.Rigour)
							{
								if (activePrayers[i].augmentType == PrayerAugmentTypes.Range)
								{
									statAugment(activePrayers[i].augmentType, 0);
									prayersToDesactivate.Add(activePrayers[i]);
								}
							}
							else
							{
								if (activePrayers[i].augmentType == PrayerAugmentTypes.Magic)
								{
									statAugment(activePrayers[i].augmentType, 0);
									prayersToDesactivate.Add(activePrayers[i]);
								}
							}
							*/
