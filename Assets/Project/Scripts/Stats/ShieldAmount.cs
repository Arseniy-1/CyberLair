using System;
using UnityEngine;

[Serializable]
public class ShieldAmount : BaseStat
{
    public float MaxShield => CalculateValue();

    public void ReduceShield(float amount)
    {
        if (amount <= 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue - amount, 0f, MaxShield);
    }

    public void RepairShield(float repairAmount)
    {
        if (repairAmount <= 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue + repairAmount, 0f, MaxShield);
    }

    public void SetMaxShield(float amount)
    {
        if (amount <= 0)
            return;

        BaseValue = amount;
    }
}