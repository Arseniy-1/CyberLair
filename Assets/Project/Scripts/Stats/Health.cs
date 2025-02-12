using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Health : BaseStat
{
    [SerializeField] private float _regenerationTime;
    [field: SerializeField] public RegenerateAmount RegenerateAmount { get; private set; }
    
    private bool _isActive;
    private CancellationTokenSource _cancellationToken;
    
    public event Action LostHealth;
    public event Action<float> DamageTaken;

    private float MaxHealth => CalculateValue();

    public void Initialize()
    {
        RegenerateAmount.CalculateCurrentValue();
        _isActive = true;
        _cancellationToken = new CancellationTokenSource();
        
        Regenerating().Forget();
    }

    public void Heal(float amount)
    {
        if(amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        
        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0f, MaxHealth);
    }

    public void TakeDamage(float amount)
    {
        if(amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        
        CurrentValue -= amount;
        DamageTaken?.Invoke(amount);
        
        if(CurrentValue <= 0)
            HandleDeath();
    }

    private void HandleDeath()
    {
        _isActive = false;
        _cancellationToken.Cancel();
        
        LostHealth?.Invoke();
    }
    
    private async UniTaskVoid Regenerating()
    {
        while (_isActive)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_regenerationTime), cancellationToken: _cancellationToken.Token);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            RegenerateAmount.UpdateModifiers();
            Heal(RegenerateAmount.CurrentValue);
        }
    }
}

[Serializable]
public class RegenerateAmount : BaseStat { }

[Serializable]
public class JumpDistance : BaseStat { }

[Serializable]
public class JumpTime : BaseStat { }

[Serializable]
public class JumpReloadTime : BaseStat { }

[Serializable]
public class WeaponSpread : BaseStat { }

[Serializable]
public class WeaponDamage : BaseStat { }

[Serializable]
public class BulletPerShootCount : BaseStat { }

[Serializable]
public class WeaponBulletReloadTime : BaseStat { }

[Serializable]
public class WeaponRechargingTime : BaseStat { }

[Serializable]
public class WeaponMagazineSize : BaseStat { }

[Serializable]
public class MagnetRange : BaseStat { }

[Serializable]
public class MagnetForce : BaseStat { }
