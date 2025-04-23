namespace Project.Scripts.EnemySystem.MoveTypes
{
    public class EnemyWalkMover : EnemyMover
    {
        protected override void Move()
        {
            if(EnemyTargetProvider.HasPlayer == false)
                return;

            EnemyRigidbody.velocity = Direction.normalized * MoverStats.Speed.CurrentValue;
        }
    }
}