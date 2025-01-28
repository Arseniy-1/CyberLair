using System;
using UnityEngine;

public class ExperienceStorage : Stats
{
    public event Action LevelRaised;

    public void AddExperience(int amount)
    {
        if (amount <= 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0, MaxValue); 

        if (CurrentValue >= MaxValue)
            LevelRaised?.Invoke();

        RaiseAmountChanged();
    }

    public void ResetExperience(int maxValue)
    {
        CurrentValue = 0;
        MaxValue = maxValue;
    }
}