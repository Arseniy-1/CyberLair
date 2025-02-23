using UnityEngine;

[CreateAssetMenu(fileName = "SnowBloodSkill", menuName = "Skill/Hard/SnowBlood", order = 51)]
public class SnowBloodSkill : HardSkill
{
    [field: SerializeField] public StatModifier HealthModifier {get; private set;}
    [field: SerializeField] public StatModifier DamageModifier {get; private set;}
}