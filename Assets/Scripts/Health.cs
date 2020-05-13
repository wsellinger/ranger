using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public uint MaxHealth;

    public event EventHandler HealthChanged;

    private uint m_uiCurrentHealth;
    public uint CurrentHealth
    {
        get { return m_uiCurrentHealth; }
		private set
		{
            if (value != m_uiCurrentHealth)
            {
                m_uiCurrentHealth = value;
                RaiseHealthChanged();
            }
        }
    }

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(uint uiDamageAmount)
    {
        CurrentHealth = (uint)Mathf.FloorToInt(Mathf.Max(CurrentHealth - uiDamageAmount, 0));
    }

    public void Heal(uint uiHealAmount)
    {
        CurrentHealth = (uint)Mathf.CeilToInt(Mathf.Min(CurrentHealth + uiHealAmount, MaxHealth));
    }

    protected virtual void RaiseHealthChanged()
    {
        EventHandler handler = HealthChanged;
        handler?.Invoke(this, EventArgs.Empty);
    }
}
