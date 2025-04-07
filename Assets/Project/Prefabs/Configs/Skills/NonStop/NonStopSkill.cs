using UnityEngine;

[CreateAssetMenu(fileName = "NonStopSkill", menuName = "Skill/Mutant/NonStop", order = 51)]
public class NonStopSkill : MutantSkill
{
    [field: SerializeField] public LandMine LandMinePrefab { get; private set; }
    [field: SerializeField] public int NedeedDiedEnemyCount { get; private set; }
}