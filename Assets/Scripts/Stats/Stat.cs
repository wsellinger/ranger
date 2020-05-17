using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public uint MaxStat;

    public event EventHandler StatChanged;

    private float m_fCurrentStat;
    public float CurrentStat
    {
        get { return m_fCurrentStat; }
		protected set
		{
            value = Math.Min(value, MaxStat);
            value = Math.Max(value, 0);

            if (value != m_fCurrentStat)
            {
                m_fCurrentStat = value;
                RaiseStatChanged();
            }
        }
    }

    public bool Full
    {
        get { return CurrentStat == MaxStat; }
    }

    public virtual void Awake()
    {
        CurrentStat = MaxStat;
    }

    protected virtual void RaiseStatChanged()
    {
        EventHandler handler = StatChanged;
        handler?.Invoke(this, EventArgs.Empty);
    }
}
