using System;
using System.Collections;
using Project.Scripts.Weapon;
using UnityEngine;

[Serializable]
public abstract class BulletEffector
{
    protected Weapon Weapon;

    public abstract void Initialize(Weapon weapon);
}