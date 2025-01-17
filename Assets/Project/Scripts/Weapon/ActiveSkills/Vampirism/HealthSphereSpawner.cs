using Project.Scripts.EnemySystem;
using Project.Scripts.Weapon.ActiveSkills.Vampirism;

namespace Project.Scripts.Weapon.ActiveSkills
{
    public class HealthSphereSpawner : Spawner<HealthSphere>
    {
        private float _healthMultiplier = 1;
        
        private void OnEnable()
        {
            Enemy.OnDeath += HandleEnemyDeath;
        }

        private void OnDisable()
        {
            Enemy.OnDeath -= HandleEnemyDeath;
        }

        public void Initialize()
        {
            Pool = new HealthSpherePool(Prefab);
        }

        public void ApplyMultiplier(float healthMultiplier)
        {
            _healthMultiplier = healthMultiplier;
        }

        private void HandleEnemyDeath(Enemy enemy)
        {
            HealthSphere sphere = Spawn();
            
            sphere.transform.position = enemy.transform.position;
            sphere.ApplyStats((int)(enemy.Health.Max * _healthMultiplier));
        }
    }
}