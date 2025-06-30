using UnityEngine;

[CreateAssetMenu(fileName = "TirelessSkill", menuName = "Skill/Hard/Tireless", order = 51)]
public class TirelessSkill : HardSkill
{
    [field: SerializeField] public StatModifier JumpReloadTimeModifier { get; private set; }
}