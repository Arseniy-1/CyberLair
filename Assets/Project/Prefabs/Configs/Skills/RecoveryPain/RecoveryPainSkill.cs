using UnityEngine;

[CreateAssetMenu(fileName = "RecoveryPainSkill", menuName = "Skill/Simple/RecoveryPain", order = 51)]
public class RecoveryPainSkill : Skill
{
    [field: SerializeField] public StatModifier RegenerateModifier { get; private set; }
}