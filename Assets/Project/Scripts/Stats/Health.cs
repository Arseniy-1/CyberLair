using System;
using UnityEngine;

[Serializable]
public class Health : BaseStat
{
    public event Action LostHealth;

    private float MaxHealth => CalculateValue();

    public void Heal(int amount)
    {
        if(amount < 0)
            throw new ArgumentOutOfRangeException(amount.ToString());
        
        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0, MaxHealth);
    }

    public void TakeDamage(int amount)
    {
        if(amount < 0)
            throw new ArgumentOutOfRangeException(amount.ToString());
        
        CurrentValue = Mathf.Clamp(CurrentValue - amount, 0, MaxHealth);
        
        if(CurrentValue <= 0)
            LostHealth?.Invoke();
    }
}

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
