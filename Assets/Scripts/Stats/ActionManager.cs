using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
	#region Singleton
	public static ActionManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	[Header("Minions")]
	public GameObject minionPrefab;
	public List<Transform> minionLocations = new List<Transform>();

	[Header("Venom")]
	public GameObject venomPrefab;
	public List<Transform> venomLocations = new List<Transform>();
	
	void Start()
	{
		Projectile.OnProjectileHit += OnProjectileHit;

	}

	void OnProjectileHit(PlayerStats aggresorStats, PlayerStats targetStats, Projectile proj)
	{
		float accuracy = -1;
		int point = -1;
		switch (proj.Type)
		{
			case ProjectileType.Ranged:
				if (aggresorStats.RangedAtkRoll > targetStats.RangedDefenceRoll)
					accuracy = 1 - ((targetStats.RangedDefenceRoll + 2) / (2 * (aggresorStats.RangedAtkRoll + 1)));

				else if (targetStats.RangedDefenceRoll > aggresorStats.RangedAtkRoll)
					accuracy = aggresorStats.RangedAtkRoll / (2 * (targetStats.RangedDefenceRoll + 1));

				break;

			case ProjectileType.Magic:
				if (aggresorStats.MagicAtkRoll > targetStats.MagicDefenceRoll)
					accuracy = 1 - ((targetStats.MagicDefenceRoll + 2) / (2 * (aggresorStats.MagicAtkRoll + 1)));

				else if (targetStats.MagicDefenceRoll > aggresorStats.MagicAtkRoll)
					accuracy = aggresorStats.MagicAtkRoll / (2 * (targetStats.MagicDefenceRoll + 1));

				break;

			case ProjectileType.Minion:
				point = Random.Range(0, minionLocations.Count);
				Instantiate(minionPrefab, minionLocations[point].position, minionLocations[point].rotation);
				break;

			case ProjectileType.Venom:
				point = Random.Range(0, venomLocations.Count);
				Instantiate(venomPrefab, venomLocations[point].position, venomLocations[point].rotation);
				break;
		}
		

		if(proj.Type == ProjectileType.Magic || proj.Type == ProjectileType.Ranged)
		{
			float random = Random.Range(0f, 1f);

			if (accuracy >= random)
				targetStats.TakeDamage(proj.damage);
		}
	}

}
