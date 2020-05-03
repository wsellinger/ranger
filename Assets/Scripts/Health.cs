using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public uint MaxHealth;

    private uint uiCurrentHealth;

    public uint CurrentHealth
    {
        get
        {
            return uiCurrentHealth;
        }
    }

    void Awake()
    {
        uiCurrentHealth = MaxHealth;
    }

    public void TakeDamage(uint uiDamageAmount)
    {
        uiCurrentHealth = (uint)Mathf.FloorToInt(Mathf.Max(CurrentHealth - uiDamageAmount, 0));
    }

    public void Heal(uint uiHealAmount)
    {
        uiCurrentHealth = (uint)Mathf.CeilToInt(Mathf.Min(CurrentHealth + uiHealAmount, MaxHealth));
    }
}
