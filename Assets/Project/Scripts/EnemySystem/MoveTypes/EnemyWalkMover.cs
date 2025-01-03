using UnityEngine;

namespace Project.Scripts.EnemySystem.MoveTypes
{
    public class EnemyWalkMover : EnemyMover
    {
        protected override void Move()
        {
            if(!PlayerPrefab)
                return;
            
            Vector2 direction = (PlayerPrefab.Position - EnemyPrefab.Position).normalized;
            Vector2 newPosition = EnemyPrefab.Position + direction * (Speed * Time.fixedDeltaTime);

            EnemyRigidbody.MovePosition(newPosition);
        }
    }
}