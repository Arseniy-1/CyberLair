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
        Debug.Log(CurrentValue);

        if (CurrentValue >= MaxValue)
            LevelRaised?.Invoke();

        RaiseAmountChanged();
    }

    public bool TrySpendExperience(int amount)
    {
        if (amount <= 0)
            return false;

        if (CurrentValue - amount < 0)
            return false;

        CurrentValue -= amount;
        RaiseAmountChanged();

        return true;
    }
}