using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "ThunderSkill", menuName = "Skill/Simple/Thunder", order = 0)]
    public class ThunderSkill : Skill
    {
        [SerializeField] private Thunder _thunderPrefab;
        
        public override void Apply(SkillData skillData)
        {
            var thunder = Instantiate(_thunderPrefab, skillData.WeaponHolder.transform);
            thunder.Initialize(skillData.WeaponHolder.Weapon);
        }
    }
}