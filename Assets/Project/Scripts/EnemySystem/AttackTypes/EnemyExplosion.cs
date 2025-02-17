using Project.Scripts.Servises;
using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    public class EnemyExplosion : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private EnemyAttacker _attacker;
        [SerializeField] private Explosion _explosion;

        private void OnEnable()
        {
            _attacker.AttackPerformed += Explode;
        }

        private void OnDisable()
        {
            _attacker.AttackPerformed -= Explode;
        }
        
        private void Explode()
        {
            _explosion.Explode(transform.position);
            
            EndExplosion();
        }

        private void EndExplosion()
        {
            _attacker.AttackPerformed -= Explode;
            
            _enemy.Die();
        }
    }
}