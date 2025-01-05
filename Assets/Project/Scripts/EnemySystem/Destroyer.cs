using System;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Health _health;
    private Enemy _enemy;
    
    private void OnEnable()
    {
        _health.LostHealth += RaiseDeath;
    }

    private void OnDisable()
    {
        _health.LostHealth -= RaiseDeath;
    }

    public void Initialize(Health health, Enemy enemy)
    {
        _health = health;
        _enemy = enemy;
    }
    
    private void RaiseDeath()
    {
        _enemy.Die();
    }
}