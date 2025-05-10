using UnityEngine;

[CreateAssetMenu(fileName = "HellCatsSkill", menuName = "Skill/Mutant/HellCatsSkill", order = 51)]
public class HellCatsSkill : MutantSkill
{
    [field: SerializeField] public HellCat HellCatPrefab { get; private set; }
    [field: SerializeField] public int MaxSummonCount { get; private set; } = 3;
    [field: SerializeField] public int MinSummonCount { get; private set; } = 1;
}