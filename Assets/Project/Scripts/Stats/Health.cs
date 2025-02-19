using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class Health : BaseStat
{
    private ShieldAmount _shieldAmount;
    private bool _isActive;

    public event Action LostHealth;
    public event Action<float> DamageTaken;

    public float MaxHealth => CalculateValue();

    public void Initialize(ShieldAmount shieldAmount)
    {
        _shieldAmount = shieldAmount;
        _isActive = true;
    }

    [Button]
    public void Heal(float amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

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

    private void HandleDeath()
    {
        _isActive = false;

        LostHealth?.Invoke();
    }
}

[Serializable]
public class HealthRegenerateAmount : BaseStat
{
}

[Serializable]
public class ShieldAmount : BaseStat
{
    public float MaxShield => CalculateValue();

    public void ReduceShield(float amount)
    {
        if (amount < 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue - amount, 0f, MaxShield);
    }

    public void RepairShield(float repairAmount)
    {
        if (repairAmount < 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue + repairAmount, 0f, MaxShield);
    }
}

[Serializable]
public class JumpDistance : BaseStat
{
}

[Serializable]
public class JumpTime : BaseStat
{
}

[Serializable]
public class JumpReloadTime : BaseStat
{
}

[Serializable]
public class WeaponSpread : BaseStat
{
}

[Serializable]
public class WeaponDamage : BaseStat
{
}

[Serializable]
public class BulletPerShootCount : BaseStat
{
}

[Serializable]
public class WeaponBulletReloadTime : BaseStat
{
}

[Serializable]
public class WeaponRechargingTime : BaseStat
{
}

[Serializable]
public class WeaponMagazineSize : BaseStat
{
}

[Serializable]
public class MagnetRange : BaseStat
{
}

[Serializable]
public class MagnetForce : BaseStat
{
}