using System;
using UnityEngine;

public class ExperienceStorage : BaseStat
{
    public event Action LevelRaised;

    public float MaxValue;

    public void AddExperience(int amount)
    {
        if (amount <= 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0, MaxValue);

        if (CurrentValue >= MaxValue)
            LevelRaised?.Invoke();

        OnAmountChanged(CurrentValue, MaxValue);
    }

    public void ResetExperience(int maxValue)
    {
        CurrentValue = 0;
        MaxValue = maxValue;

        OnAmountChanged(CurrentValue, MaxValue);
    }
}