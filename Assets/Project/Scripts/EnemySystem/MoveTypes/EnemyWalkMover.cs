using UnityEngine;

namespace Project.Scripts.EnemySystem.MoveTypes
{
    public class EnemyWalkMover : EnemyMover
    {
        protected override void Move()
        {
            if(EnemyTargetProvider.HasPlayer == false)
                return;

            EnemyRigidbody.MovePosition(EnemyPrefab.Position + Direction * (Speed * Time.fixedDeltaTime));
        }
    }
}