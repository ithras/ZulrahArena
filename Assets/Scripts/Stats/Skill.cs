using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
	public int baseValue;

	public int potion = 0;

	public float prayer = 1f;

	public float other = 1f;

	public int effectiveValue(bool isRoll)
	{
		if (isRoll)
			return Mathf.FloorToInt((((baseValue + potion) * prayer) + 8) * other);

		else
			return Mathf.FloorToInt((baseValue + potion) * prayer * other);

	}

	public int effectiveValue(float bonus) => Mathf.FloorToInt((baseValue + potion) * bonus * other) + 8;
}
