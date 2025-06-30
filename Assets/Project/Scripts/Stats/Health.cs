using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Health : BaseStat
{
    public event Action LostHealth;
    public event Action<float> DamageTaken;

    public float MaxHealth => CalculateValue();
    public ShieldAmount ShieldAmount { get; private set; }

    public void Initialize(ShieldAmount shieldAmount)
    {
        ShieldAmount = shieldAmount;
    }

    [Button]
    public void Heal(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0f, MaxHealth);
        OnAmountChanged(CurrentValue, MaxHealth);
    }

    [Button]
    public void TakeDamage(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (ShieldAmount != null)
        {
            float shieldDamage = Mathf.Min(ShieldAmount.CurrentValue, amount);
            ShieldAmount.ReduceShield(shieldDamage);
            amount -= shieldDamage;
        }

        if (amount > 0)
            CurrentValue = Mathf.Clamp(CurrentValue - amount, 0f, MaxHealth);

        OnAmountChanged(CurrentValue, MaxHealth);

        if (CurrentValue <= 0)
            HandleDeath();
        
        DamageTaken?.Invoke(amount);
    }

    [Button]
    public void SetMaxHealth(float amount)
    {
        if (amount <= 0)
            return;

        OnAmountChanged(CurrentValue, MaxHealth);
    }

    private void HandleDeath()
    {
        LostHealth?.Invoke();
    }
}