using UnityEngine;

namespace Project.Prefabs.Configs.Skills.StunZap
{
    [CreateAssetMenu(fileName = "StunZapSkill", menuName = "Skill/Simple/StunZap", order = 51)]
    public class StunZapSkill : Skill
    {
        [field: SerializeField] public float StunDuration { get; private set; }
    }
}