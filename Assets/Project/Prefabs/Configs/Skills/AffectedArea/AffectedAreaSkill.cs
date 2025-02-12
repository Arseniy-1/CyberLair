using UnityEngine;

namespace Project.Prefabs.Configs.Skills.AffectedArea
{
    [CreateAssetMenu(fileName = "AffectedAreaSkill", menuName = "Skill/Simple/AffectedArea", order = 51)]
    public class AffectedAreaSkill : Skill
    {
        [SerializeField] private AffectedArea _affectedArea;
        
        public override void Apply(SkillData skillData)
        {
            _affectedArea.Initialize(skillData.WeaponHolder.Weapon);
        }
    }
}