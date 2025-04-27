using System;
using System.Collections.Generic;
using Project.Scripts.Weapon;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWeapon : Weapon
{
    public override bool TryAttack()
    {
        if (!IsReloaded)
            return false;

        Attack();
        IsReloaded = false;

        return true;
    }
}