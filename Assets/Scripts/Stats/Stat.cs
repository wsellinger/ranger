using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public uint MaxStat;

    public event EventHandler StatChanged;

    private uint m_uiCurrentStat;
    public uint CurrentStat
    {
        get { return m_uiCurrentStat; }
		protected set
		{
            if (value != m_uiCurrentStat)
            {
                m_uiCurrentStat = value;
                RaiseStatChanged();
            }
        }
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
