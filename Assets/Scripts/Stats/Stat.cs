using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public uint MaxStat;

    public event EventHandler CurrentStatChanged;
	public event EventHandler CurrentMaxStatChanged;

	private float m_fCurrentStat;
    public float CurrentStat
    {
        get { return m_fCurrentStat; }
		protected set
		{
            value = Math.Min(value, m_fCurrentMaxStat);
            value = Math.Max(value, 0);

            if (value != m_fCurrentStat)
            {
                m_fCurrentStat = value;
                CurrentStatChanged.Raise(this);
            }
        }
	}

	private float m_fCurrentMaxStat;
	public float CurrentMaxStat
	{
		get { return m_fCurrentMaxStat; }
		protected set
		{
			value = Math.Min(value, MaxStat);
			value = Math.Max(value, 0);

			if (value != m_fCurrentMaxStat)
			{
				m_fCurrentMaxStat = value;
				CurrentMaxStatChanged.Raise(this);
			}
		}
	}

	public bool Full
    {
        get { return CurrentStat == CurrentMaxStat; }
    }

    public virtual void Awake()
    {
		m_fCurrentStat = m_fCurrentMaxStat = MaxStat;		
    }
}
