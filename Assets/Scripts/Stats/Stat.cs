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
	private float m_fCurrentMaxStat;

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

				CurrentStat = Mathf.Min(CurrentStat, m_fCurrentMaxStat);
			}
		}
	}

	public bool Full
    {
        get { return CurrentStat == CurrentMaxStat; }
	}

	public bool Empty
	{
		get { return CurrentStat == 0; }
	}

	public bool MaxFull
	{
		get { return CurrentMaxStat == MaxStat; }
	}

	public bool MaxEmpty
	{
		get { return CurrentMaxStat == 0; }
	}

	public virtual void Awake()
    {
		m_fCurrentStat = m_fCurrentMaxStat = MaxStat;		
    }
}
