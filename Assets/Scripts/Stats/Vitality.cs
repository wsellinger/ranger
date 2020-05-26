using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitality : Stat
{
	[Tooltip("Amount of vitality lost every second")]
	public float DecayPerSec = 1f;

	[Tooltip("Amount of regeneration of max stats per second while resting")]
	public float RestRegenPerSec = 15f;

	[Tooltip("Percent of Rest Regen Per Sec lost as vitality while resting")]
	public float RestCostPercent = 0.5f;

	private Health m_health;
	private Stamina m_stamina;

	public float VitalityPercent
	{
		get
		{
			return CurrentStat / (float)MaxStat;
		}
	}
	public override void Awake()
	{
		base.Awake();

		m_health = GetComponent<Health>();
		m_stamina = GetComponent<Stamina>();
	}

	public void Replenish(float fValue)
	{
		CurrentStat += Mathf.Abs(fValue);
	}

	private void Update()
	{
		CurrentStat -= DecayPerSec * Time.deltaTime;

		if (Input.GetButton("Rest"))
		{
			bool bStatsToReplinish = !m_health.MaxFull || !m_stamina.MaxFull;
			if (bStatsToReplinish && !MaxEmpty)
			{
				float fRest = RestRegenPerSec * Time.deltaTime;
				m_health.RegenerateStat(fRest);
				m_stamina.RegenerateStat(fRest);
				CurrentMaxStat -= fRest * RestCostPercent;
			}
		}
	}
}
