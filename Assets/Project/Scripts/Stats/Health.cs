using System;
using UnityEngine;

public class Health : Stats
{
    public event Action LostHealth;

    private void Awake()
    {
        CurrentValue = maxHealthValue;
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0, maxHealthValue);

        RaiseAmountChanged();
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue - amount, 0, maxHealthValue);

        if (CurrentValue == 0)
            LostHealth?.Invoke();

        RaiseAmountChanged();
    }

    public void IncreaseHealth(int amount)
    {
        if (amount <= 0)
            return;

        maxHealthValue += amount;
        // CurrentValue += amount;
    }
}