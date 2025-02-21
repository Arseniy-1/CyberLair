using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "ThunderSkill", menuName = "Skill/Simple/Thunder", order = 51)]
    public class ThunderSkill : Skill
    {
        [field: SerializeField] public Thunder Thunder { get; private set; }

        public override void Apply(SkillData skillData)
        {
            Thunder.Initialize(skillData.WeaponHolder.Weapon, skillData.WeaponHolder.transform);
        }
    }
}