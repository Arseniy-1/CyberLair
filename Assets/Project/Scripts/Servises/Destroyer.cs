using System;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Health _health;
    private IDieable _dieable;
    
    private void OnEnable()
    {
        _health.LostHealth += RaiseDeath;
    }

    private void OnDisable()
    {
        _health.LostHealth -= RaiseDeath;
    }

    public void Initialize(Health health, IDieable dieable)
    {
        _health = health;
        _dieable = dieable;
    }
    
    private void RaiseDeath()
    {
        _dieable.Die();
    }
}