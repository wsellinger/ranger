using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalDamage : MonoBehaviour 
{
    public uint DamagePerInteval;
    public uint Interval;

    float fTimer;

    void Awake()
    {
        fTimer = 0;
    }

    void Update()
    {
        if (fTimer < Interval)
        {
            fTimer = Mathf.Min(fTimer + Time.deltaTime, Interval);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == Tags.PLAYER && fTimer == (Interval))
        {
            other.gameObject.GetComponent<Health>().DamageStat(DamagePerInteval);
            fTimer = 0;
        }
    }
}