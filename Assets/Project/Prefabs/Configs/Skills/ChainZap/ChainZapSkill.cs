using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Zap
{
    [CreateAssetMenu(fileName = "ChainZapSkill", menuName = "Skill/Simple/ChainZap", order = 51)]
    public class ChainZapSkill : Skill
    {
        [SerializeField] private ChainZap _chainZap;
        
        public override void Apply(SkillData skillData)
        {
            _chainZap.Initialize(skillData.WeaponHolder.Weapon);
        }
    }
}