using System;
using UnityEngine;

public class Health : Stats
{
    public event Action LostHealth;

    private void Awake()
    {
        CurrentValue = MaxValue;
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0, MaxValue);

        RaiseAmountChanged();
    }

    public float TakeDamage(int amount)
    {
        if (amount <= 0)
            return 0;

        CurrentValue = Mathf.Clamp(CurrentValue - amount, 0, MaxValue);

        if (CurrentValue == 0)
            LostHealth?.Invoke();

        RaiseAmountChanged();

        if (CurrentValue < amount)
            return CurrentValue;
        else
            return amount;
    }
}
