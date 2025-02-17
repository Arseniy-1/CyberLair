using UnityEngine;

namespace Project.Prefabs.Configs.Skills.MercuryMimicry
{
    [CreateAssetMenu(fileName = "MercuryMimicrySkill", menuName = "Skill/Hard/MercuryMimicry", order = 51)]
    public class MercuryMimicrySkill : HardSkill
    {
        [SerializeField] private MercuryMimicry _mercuryMimicry;
        
        public override void Apply(SkillData skillData)
        {
            _mercuryMimicry.Initialize(skillData);
        }
    }
}