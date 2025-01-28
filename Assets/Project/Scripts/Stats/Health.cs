using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class Health : Stats
{
    public event Action LostHealth;
    
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

    public void SetHealth(int amount)
    {
        if (amount <= 0)
            return;

        MaxValue += amount;
    }

    public Health Copy()
    {
        Health copy = gameObject.AddComponent<Health>();
        
        return copy;
    }

    [Button]
    public void ResetHealth()
    {
        RaiseAmountChanged();
        CurrentValue = MaxValue;
    }
}