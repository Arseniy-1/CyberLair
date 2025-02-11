using UnityEngine;

namespace Project.Prefabs.Configs.Skills.StunZap
{
    public class StanZapSkill : Skill
    {
        [SerializeField] private float _stunTime;
        
        public override void Apply(SkillData skillData)
        {
            skillData.WeaponHolder.Weapon.OnShooted += InnerSubscribe;
        }

        private void InnerSubscribe(Bullet bullet)
        {
            bullet.OnDamagableCollided += StunEnemy;
        }

        private void StunEnemy(IDamageable damageable)
        {
            (damageable as IStunable)?.TakeStun(_stunTime);
        }
    }
}