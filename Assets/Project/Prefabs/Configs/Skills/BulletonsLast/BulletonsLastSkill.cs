using UnityEngine;

namespace Project.Prefabs.Configs.Skills.BulletonsLast
{
    [CreateAssetMenu(fileName = "BulletonsLastSkill", menuName = "Skill/Hard/BulletonsLast", order = 51)]
    public class BulletonsLastSkill : HardSkill
    {
        private readonly BulletonsLast _bulletonsLast = new();
        
        public override void Apply(SkillData skillData)
        {
            _bulletonsLast.Initialize(skillData.WeaponHolder.Weapon);
        }
    }
}