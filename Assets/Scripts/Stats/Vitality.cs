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

	private void Update()
	{
		CurrentStat -= DecayPerSec * Time.deltaTime;
	}
}
