using UnityEngine;

[CreateAssetMenu(fileName = "ArtayaWillSkill", menuName = "Skill/Hard/ArtayaWill", order = 51)]
public class ArtayaWillSkill : Skill
{
    [field: SerializeField] public StatModifier ZeroModifier { get; private set; }
    [field: SerializeField] public StatModifier HeatlhModifier { get; private set; }
    [field: SerializeField] public StatModifier ShieldModifier { get; private set; }
}