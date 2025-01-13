using System;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Health _health;
    private IDieable _dieable;

    public void Initialize(Health health, IDieable dieable)
    {
        _health = health;
        _dieable = dieable;
        
        _health.LostHealth += RaiseDeath;
    }
    
    private void RaiseDeath()
    {
        _health.LostHealth -= RaiseDeath;
        
        _dieable.Die();
    }
}