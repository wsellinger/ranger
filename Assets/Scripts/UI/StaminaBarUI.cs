using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBarUI : StatBarUI
{
	override public void Awake()
	{
		m_sTarget = Target.GetComponent<Stamina>();

		base.Awake();
	}
}
