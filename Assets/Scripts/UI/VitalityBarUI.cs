using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitalityBarUI : StatBarUI
{
	override public void Awake()
	{
		m_sTarget = Target.GetComponent<Vitality>();

		base.Awake();
	}
}
