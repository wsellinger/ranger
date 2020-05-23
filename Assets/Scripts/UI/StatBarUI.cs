using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatBarUI : MonoBehaviour
{
	public GameObject Target;

	protected Stat m_sTarget;

	protected RectTransform m_rtStatBar;
	protected RectTransform m_rtMaxStatBar;
	protected RectTransform m_rtEmptyStatBar;

	private float m_fMaxStatBarMaxLength;
	private float m_fStatBarMaxLength;

	private const string STAT_BAR_NAME = "Current Bar";
	private const string MAX_STAT_BAR_NAME = "Max Bar";
	private const string EMPTY_STAT_BAR_NAME = "Empty Bar";

	virtual public void Awake()
	{
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

	virtual public void OnEnable()
	{
		m_sTarget.CurrentStatChanged += ConsumeCurrentStatChanged;
		m_sTarget.CurrentMaxStatChanged += ConsumeCurrentMaxStatChanged;
	}

	virtual public void OnDisable()
	{
		m_sTarget.CurrentStatChanged -= ConsumeCurrentStatChanged;
		m_sTarget.CurrentMaxStatChanged -= ConsumeCurrentMaxStatChanged;
	}

	private void ConsumeCurrentStatChanged(object sender, EventArgs e)
	{
		SetStatBarWidth(m_sTarget.CurrentStat, m_sTarget.MaxStat, m_fStatBarMaxLength, m_rtStatBar);
	}
	private void ConsumeCurrentMaxStatChanged(object sender, EventArgs e)
	{
		SetStatBarWidth(m_sTarget.CurrentMaxStat, m_sTarget.MaxStat, m_fMaxStatBarMaxLength, m_rtMaxStatBar);
	}

	private void SetStatBarWidth(float fCurrent, float fMaxStat, float fMaxBar, RectTransform rtBar)
	{
		float fPercentage = fCurrent / fMaxStat;
		float fBarWidth = fMaxBar * fPercentage;
		rtBar.sizeDelta = new Vector2(fBarWidth, rtBar.sizeDelta.y);
	}
}
