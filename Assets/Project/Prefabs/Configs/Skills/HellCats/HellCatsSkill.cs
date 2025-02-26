using UnityEngine;

[CreateAssetMenu(menuName = "HellCatsSkill", fileName = "HellCats", order = 51)]
public class HellCatsSkill : HardSkill
{
    [field: SerializeField] public HellCat HellCatPrefab { get; private set; }
    [field: SerializeField] public float SummonSpawnDelay { get; private set; }

    [field: SerializeField] public int MaxSummonCount { get; private set; } = 3;
    [field: SerializeField] public int MinSummonCount { get; private set; } = 1;
}