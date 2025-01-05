using UnityEngine;

namespace Project.Scripts.EnemySystem.MoveTypes
{
    public class EnemyFlightMover : EnemyMover
    {
        protected override void Move()
        {
            if(EnemyTargetProvider.HasPlayer == false)
                return;
            
            EnemyRigidbody.velocity += Direction * (Speed * Time.fixedDeltaTime);
        }
    }
}