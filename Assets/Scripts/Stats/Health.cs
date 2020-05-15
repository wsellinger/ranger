using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Health : Stat
{
	public void TakeDamage(uint uiDamageAmount)
	{
		CurrentStat = (uint)Mathf.FloorToInt(Mathf.Max(CurrentStat - uiDamageAmount, 0));
	}

	public void Heal(uint uiHealAmount)
	{
		CurrentStat = (uint)Mathf.CeilToInt(Mathf.Min(CurrentStat + uiHealAmount, MaxStat));
	}
}
