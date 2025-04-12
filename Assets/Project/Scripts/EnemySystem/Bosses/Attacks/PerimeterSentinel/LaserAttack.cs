using System.Collections;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using UnityEngine;

namespace Project.Scripts.EnemySystem.Bosses.PerimeterSentinel
{
    public class LaserAttack : BossAttack
    {
        [SerializeField] private EnemyCollisionHandler _laser;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private CameraShakeSettings _cameraShakeSettings;

        public override void Initialize()
        {
            BossAttackAnimationTrigger = Animator.StringToHash("LaserAttack");
            
            Disable();
            
            _laser.Initialize(Damage);
        }

        protected override IEnumerator Attack()
        {   
            _collider.enabled = true;
            
            MessageBrokerHolder.Camera.Publish(new M_CameraShake(_cameraShakeSettings));
            
            yield return new WaitUntil(() => IsAttacking == false);
            
            Disable();
        }

        public override void Disable()
        {
            _collider.enabled = false;
            View.enabled = false;
        }
    }
}