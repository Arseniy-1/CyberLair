using System;

public enum EnemyTypes
{
    Walker,
    Hawk,
    Kamikaze,
    Blight,
    Trooper,
    Sniper,
    Doomguard,
    Imp,
    Boss,
    PerimeterSentinel,
    FireColossus,
    DeathReaper
}

public enum BossTypes
{
    PerimeterSentinel = EnemyTypes.PerimeterSentinel,
    FireColossus = EnemyTypes.FireColossus,
    DeathReaper = EnemyTypes.DeathReaper
}