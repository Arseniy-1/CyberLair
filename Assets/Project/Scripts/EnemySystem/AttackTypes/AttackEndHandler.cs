using UnityEngine;

namespace Project.Scripts.EnemySystem.AttackTypes
{
    public abstract class AttackEndHandler : MonoBehaviour
    {
        [SerializeField] private EnemyAttacker _attacker;
        
        private void OnEnable()
        {
            _attacker.AttackPerforming += OnPerforming;
        }

        private void OnDisable()
        {
            _attacker.AttackPerforming -= OnPerforming;
        }

        private void OnPerforming(bool isContinuing)
        {
            if(isContinuing)
                return;

            EndAttack();
        }

        protected abstract void EndAttack();
    }
}