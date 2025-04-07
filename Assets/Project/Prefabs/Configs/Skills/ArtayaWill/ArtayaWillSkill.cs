using UnityEngine;

[CreateAssetMenu(fileName = "ArtayaWillSkill", menuName = "Skill/Mutant/ArtayaWill", order = 51)]
public class ArtayaWillSkill : MutantSkill
{
    [field: SerializeField] public float ShieldMultiplier { get; private set; } = 3;
}