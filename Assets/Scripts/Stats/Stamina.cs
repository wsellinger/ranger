using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : Stat
{
	public float MaxRegenPerSec;
	public float RegenDelay;

	private bool m_bDepleted;
	virtual public bool Depleted
	{
		get { return m_bDepleted; }
		protected set
		{
			if (m_bDepleted != value)
			{
				m_bDepleted = value;

				if (value) StaminaDepleted.Raise(this);
				else StaminaRestored.Raise(this);
			}
		}
	}

	public event EventHandler StaminaDepleted;
	public event EventHandler StaminaRestored;

	private Vitality m_vitality;
	private float m_fRegenDelay;

	override public void Awake()
	{
		base.Awake();

		m_vitality = GetComponent<Vitality>();
	}

	void Update()
	{
		if (m_fRegenDelay == 0)
		{
			Regen();
		}
		else
		{
			m_fRegenDelay = Mathf.Max(m_fRegenDelay - Time.deltaTime, 0);
		}
	}

	public void Drain(float fValue)
	{
		CurrentStat -= Mathf.Abs(fValue);

		if (CurrentStat == 0)
		{
			Depleted = true;
		}
		else
		{
			m_fRegenDelay = RegenDelay;
		}
	}    

	private void Regen()
	{
		if (!Full)
		{
			CurrentStat += MaxRegenPerSec * Time.deltaTime * m_vitality.VitalityPercent;
		}

		//Stay depleted until fully restored
		if (Depleted && Full)
		{
			Depleted = false;
		}
	}
}
