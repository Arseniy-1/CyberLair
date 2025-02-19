using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Health : BaseStat
{
    private ShieldAmount _shieldAmount;

    public event Action LostHealth;
    public event Action<float> DamageTaken;

    public float MaxHealth => CalculateValue();

    public void Initialize(ShieldAmount shieldAmount)
    {
        _shieldAmount = shieldAmount;
    }

    [Button]
    public void Heal(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        OnAmountChanged();
        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0f, MaxHealth);
    }

    [Button]
    public void TakeDamage(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (_shieldAmount != null)
        {
            float shieldDamage = Mathf.Min(_shieldAmount.CurrentValue, amount);
            _shieldAmount.ReduceShield(shieldDamage);
            amount -= shieldDamage;
        }

        if (amount > 0)
            CurrentValue -= amount;

        DamageTaken?.Invoke(amount);

        if (CurrentValue <= 0)
            HandleDeath();
    }

    [Button]
    public void SetMaxHealth(float amount)
    {
        if(amount <= 0)
            return;
     
        BaseValue = amount;
    }

    private void HandleDeath()
    {
        LostHealth?.Invoke();
    }
}