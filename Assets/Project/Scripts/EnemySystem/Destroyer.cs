using System;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Health _health;
    private Enemy _enemy;

    public void Initialize(Health health, Enemy enemy)
    {
        _health = health;
        _enemy = enemy;
        
        _health.LostHealth += RaiseDeath;
    }
    
    private void RaiseDeath()
    {
        _health.LostHealth -= RaiseDeath;
        
        _enemy.Die();
    }
}