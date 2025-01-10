using System;
using System.Collections;
using UnityEngine;

[Serializable]
public abstract class BulletEffector
{
    protected Weapon Weapon;

    public abstract void Initialize(Weapon weapon);
}