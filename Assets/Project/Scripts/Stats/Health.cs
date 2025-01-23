using System;
using UnityEngine;

public class Health : Stats
{
    public event Action LostHealth;

    // private void Awake()
    // {
    //     ResetHealth();
    // }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0, MaxValue);

        RaiseAmountChanged();
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue - amount, 0, MaxValue);

        if (CurrentValue == 0)
            LostHealth?.Invoke();

        RaiseAmountChanged();
    }

    public void IncreaseHealth(int amount)
    {
        if (amount <= 0)
            return;

        MaxValue += amount;
        // CurrentValue += amount;
    }

    public Health Copy()
    {
        Health copy = new Health
        {
            MaxValue = this.MaxValue,
            CurrentValue = this.CurrentValue
        };
        
        return copy;
    }

    public void ResetHealth()
    {
        RaiseAmountChanged();
        CurrentValue = MaxValue;
    }
}