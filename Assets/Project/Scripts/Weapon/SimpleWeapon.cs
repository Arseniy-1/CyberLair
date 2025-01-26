using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleWeapon : Weapon
{
    public override bool TryAttack()
    {
        if (!_isReloaded)
            return false;

        Attack();
        _isReloaded = false;

        return true;
    }
}