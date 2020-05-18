using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : StatBarUI
{
	public Color DepletedColor;

	private Stamina m_sStamina;
	private Image m_iStaminaBar;
	private Color m_cInitialColor;

	override public void Awake()
	{
		base.Awake();

		m_sStamina = Target.GetComponent<Stamina>();
		m_sTarget = m_sStamina;

		m_iStaminaBar = m_rtStatBar.GetComponent<Image>();
		m_cInitialColor = m_iStaminaBar.color;
	}

	override public void OnEnable()
	{
		base.OnEnable();

		m_sStamina.StaminaDepleted += ConsumeStaminaDepleted;
		m_sStamina.StaminaRestored += ConsumeStaminaRestored;
	}

	override public void OnDisable()
	{
		base.OnDisable();

		m_sStamina.StaminaDepleted -= ConsumeStaminaDepleted;
		m_sStamina.StaminaRestored -= ConsumeStaminaRestored;
	}

	private void ConsumeStaminaDepleted(object sender, EventArgs e)
	{
		m_iStaminaBar.color = DepletedColor;
	}
	private void ConsumeStaminaRestored(object sender, EventArgs e)
	{
		m_iStaminaBar.color = m_cInitialColor;
	}
}
