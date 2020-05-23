using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitality : Stat
{
	public float DecayPerSec;
	public float VitalityPercent
	{
		get
		{
			return CurrentStat / (float)MaxStat;
		}
	}

	public void Replenish(float fValue)
	{
		CurrentStat += Mathf.Abs(fValue);
	}

	private void Update()
	{
		CurrentStat -= DecayPerSec * Time.deltaTime;
	}
}
