using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : PlayerStats
{
	#region Singleton
	public static BonusManager instance;
	void Awake()
	{
		instance = this;
	}
    #endregion

    
}
