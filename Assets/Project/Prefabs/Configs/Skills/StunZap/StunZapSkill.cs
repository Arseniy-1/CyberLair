using UnityEngine;

namespace Project.Prefabs.Configs.Skills.StunZap
{
    [CreateAssetMenu(fileName = "StunZapSkill", menuName = "Skill/Simple/StunZap", order = 51)]
    public class StunZapSkill : Skill
    {
        [SerializeField] private StunZap _stunZap;
        
        public override void Apply(SkillData skillData)
        {
            _stunZap.Initialize(skillData.WeaponHolder.Weapon);
        }
    }
}