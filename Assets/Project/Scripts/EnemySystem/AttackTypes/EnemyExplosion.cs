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
            _attacker.AttackPerforming += Explode;
        }

        private void OnDisable()
        {
            _attacker.AttackPerforming -= Explode;
        }
        
        private void Explode(bool isContinuing)
        {
            if (isContinuing)
                return;
            
            _explosion.Explode(transform.position);
            
            EndExplosion();
        }

        private void EndExplosion()
        {
            _attacker.AttackPerforming -= Explode;
            
            _enemy.Die();
        }
    }
}