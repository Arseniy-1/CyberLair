using System;
using System.Linq;
using Project.Scripts.LevelSystem.ActiveSkills;
using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Overload
{
    [CreateAssetMenu(fileName = "OverloadSkill", menuName = "Skill/Hard/Overload", order = 51)]
    public class OverloadSkill : HardSkill
    {
        [SerializeField] private Thunder _overloadThunder;
        
        private ThunderSkill _thunderSkill;
        private Thunder PastThunder => _thunderSkill.Thunder;
        
        private void OnValidate()
        {
            _thunderSkill = NeededSkills.FirstOrDefault(skill => skill.GetType() == typeof(ThunderSkill)) as ThunderSkill;
            
            if(_thunderSkill == false)
                throw new NullReferenceException("ThunderSkill is not set");
        }

        public override void Apply(SkillData skillData)
        {
            PastThunder.Disable();
            _overloadThunder.Initialize(skillData.WeaponHolder.Weapon, skillData.WeaponHolder.transform);
        }
    }
}