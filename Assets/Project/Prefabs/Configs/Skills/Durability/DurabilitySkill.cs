using UnityEngine;

[CreateAssetMenu(fileName = "DurabilitySkill", menuName = "Skill/Simple/Durability", order = 51)]
public class DurabilitySkill : Skill
{
    [field: SerializeField] public StatModifier HealthModifier { get; private set; }
    [field: SerializeField] public StatModifier RegenerationModifier { get; private set; }
}