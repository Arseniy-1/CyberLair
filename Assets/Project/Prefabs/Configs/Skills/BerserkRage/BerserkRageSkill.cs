using UnityEngine;

[CreateAssetMenu(fileName = "BerserkRageSkill", menuName = "Skill/Simple/BerserkRage", order = 0)]
public class BerserkRageSkill : HardSkill
{
    [field: SerializeField] public float CriticalHealthLevel { get; private set; }
    [field: SerializeField] public StatModifier HealthRegeneratorModifier {get; private set;}
}