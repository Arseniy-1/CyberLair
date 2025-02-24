using UnityEngine;

[CreateAssetMenu(fileName = "ArtayaWillSkill", menuName = "Skill/Hard/ArtayaWill", order = 51)]
public class ArtayaWillSkill : Skill
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 1;
    [field: SerializeField] public float ShieldMultiplier { get; private set; } = 3;
}