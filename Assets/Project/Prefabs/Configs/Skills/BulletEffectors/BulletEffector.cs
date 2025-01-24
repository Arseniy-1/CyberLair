using System;
using System.Collections;
using Project.Scripts.Weapon;
using UnityEngine;


public abstract class BulletEffector : ScriptableObject
{
    protected Weapon Weapon;
    
    public abstract void Initialize(Weapon weapon);
}