using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : StatBarUI
{
	override public void Awake()
	{
		m_sTarget = Target.GetComponent<Health>();

		base.Awake();
	}
}
