using UnityEngine;

[CreateAssetMenu(fileName = "HardingSkill", menuName = "Skill/Simple/Harding", order = 51)]
public class HardingSkill : Skill
{
    [field: SerializeField] public StatModifier JumpReloadTimeModifier { get; private set; }
}