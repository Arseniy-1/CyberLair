
using UnityEngine;

namespace Project.Scripts.EnemySystem.MoveTypes
{
    public class EnemyFlightMover : EnemyMover
    {
        [SerializeField] private float _maxSpeed;
        
        protected override void Move()
        {
            if(EnemyTargetProvider.HasPlayer == false)
                return;
            
            // EnemyRigidbody.velocity += (Direction * (MoverStats.Speed * Time.fixedDeltaTime)).normalized;
            EnemyRigidbody.velocity = Vector2.ClampMagnitude(EnemyRigidbody.velocity, _maxSpeed);
        }
    }
}