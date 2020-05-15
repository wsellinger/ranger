using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatBarUI : MonoBehaviour
{
	public GameObject Target;
	public EStatType StatType;

	public enum EStatType
	{
		HEALTH, STAMINA, VITALITY
	}

	private Stat m_sTarget;

	private RectTransform m_rtStatBar;
	private RectTransform m_rtMaxStatBar;
	private RectTransform m_rtEmptyStatBar;

	private float m_fMaxStatBarMaxLength;
	private float m_fStatBarMaxLength;

	private const string STAT_BAR_NAME = "Current Bar";
	private const string MAX_STAT_BAR_NAME = "Max Bar";
	private const string EMPTY_STAT_BAR_NAME = "Empty Bar";

	void Awake()
	{
		//Get References
		Stat sTarget;
		switch (StatType)
		{
			case EStatType.HEALTH:
				sTarget = Target.GetComponent<Health>();
				break;
			case EStatType.STAMINA:
				sTarget = Target.GetComponent<Stamina>();
				break;
			case EStatType.VITALITY:
				sTarget = Target.GetComponent<Vitality>();
				break;
			default:
				sTarget = Target.GetComponent<Stat>();
				break;
		}

		m_sTarget = sTarget;

		m_rtStatBar = transform.Find(STAT_BAR_NAME).GetComponent<RectTransform>();
		m_rtMaxStatBar = transform.Find(MAX_STAT_BAR_NAME).GetComponent<RectTransform>();
		m_rtEmptyStatBar = transform.Find(EMPTY_STAT_BAR_NAME).GetComponent<RectTransform>();

		//Get Initial Values
		m_fMaxStatBarMaxLength = m_rtEmptyStatBar.sizeDelta.x - .5f; //TODO replace .5 with value derived from initial positions
		m_fStatBarMaxLength = m_rtEmptyStatBar.sizeDelta.x - .5f; //TODO replace .5 with value derived from initial positions

		//Set Initial Widths
		m_rtStatBar.sizeDelta = new Vector2(m_fStatBarMaxLength, m_rtStatBar.sizeDelta.y);
		m_rtMaxStatBar.sizeDelta = new Vector2(m_fMaxStatBarMaxLength, m_rtStatBar.sizeDelta.y);
	}

	void OnEnable()
	{
		m_sTarget.StatChanged += ConsumeStatChanged;
	}

	void OnDisable()
	{
		m_sTarget.StatChanged -= ConsumeStatChanged;
	}

	private void ConsumeStatChanged(object sender, EventArgs e)
	{
		float fPercentage = (float)m_sTarget.CurrentStat / (float)m_sTarget.MaxStat;
		float fBarWidth = m_fStatBarMaxLength * fPercentage;
		m_rtStatBar.sizeDelta = new Vector2(fBarWidth, m_rtStatBar.sizeDelta.y);
	}
}
