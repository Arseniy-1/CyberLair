using System;

[System.Serializable]
public class Health : BaseStat
{
    public event Action LostHealth;

    public void Heal(int amount)
    {
    }

    public void TakeDamage(int amount)
    {

    }

    public void SetHealth(int amount)
    {

    }
}

[System.Serializable]
public class JumpDistance : BaseStat
{
}

[System.Serializable]
public class JumpTime : BaseStat
{
}

[System.Serializable]
public class JumpReloadTime : BaseStat
{
}

[System.Serializable]
public class WeaponSpread : BaseStat
{
}

[System.Serializable]
public class WeaponDamage : BaseStat
{
}

[System.Serializable]
public class BulletPerShootCount : BaseStat
{
}

[System.Serializable]
public class WeaponBulletReloadTime : BaseStat
{
}

[System.Serializable]
public class WeaponRechargingTime : BaseStat
{
}

[System.Serializable]
public class WeaponMagazineSize : BaseStat
{
}

[System.Serializable]
public class MagnetRange : BaseStat
{
}

[System.Serializable]
public class MagnetForce : BaseStat
{
}
