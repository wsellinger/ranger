using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBarUI : MonoBehaviour
{
	public Health TargetHealth;
	public RectTransform HealthBar;
	public RectTransform MaxHealthBar;

	private float m_fMaxHealthBarLength;

	private void Awake()
	{
		m_fMaxHealthBarLength = HealthBar.sizeDelta.x;//TODO this is bad placeholder
	}

	private void OnEnable()
	{
		TargetHealth.HealthChanged += ConsumeHealthChanged;
	}

	private void OnDisable()
	{
		TargetHealth.HealthChanged -= ConsumeHealthChanged;
	}

	private void ConsumeHealthChanged(object sender, EventArgs e)
	{
		float fHealthPercentage = (float)TargetHealth.CurrentHealth / (float)TargetHealth.MaxHealth;
		float fHealthBarWidth = m_fMaxHealthBarLength * fHealthPercentage;
		HealthBar.sizeDelta = new Vector2(fHealthBarWidth, HealthBar.sizeDelta.y);
	}
}
