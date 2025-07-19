using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "JumpSwirlSkill", menuName = "Skill/Hard/JumpSwirl", order = 51)]
    public class JumpSwirlSkill : HardSkill
    {
        [field: SerializeField] public float KnockbackForce { get; private set; }
        [field: SerializeField] public float KnockbackRadius { get; private set; }
        [field: SerializeField] public float StunTime { get; private set; }
        [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
    }
}