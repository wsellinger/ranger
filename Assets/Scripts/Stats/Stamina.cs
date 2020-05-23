using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : RegeneratingStat
{

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

	override protected void Update()
	{
		if (m_fRegenDelay == 0)
		{
			if (CurrentStat == 0)
			{
				Depleted = true;
			}

			Regen();
		}

		base.Update();
	}

	protected override void Regen()
	{
		base.Regen();

		//Stay depleted until fully restored
		if (Depleted && Full)
		{
			Depleted = false;
		}
	}
}
