using UnityEngine;

[CreateAssetMenu(fileName = "NonStopSkill", menuName = "Skill/Hard/NonStop", order = 51)]
public class NonStopSkill : HardSkill
{
    [field: SerializeField] public LandMine LandMinePrefab { get; private set; }
    [field: SerializeField] public int NedeedDiedEnemyCount { get; private set; }
}