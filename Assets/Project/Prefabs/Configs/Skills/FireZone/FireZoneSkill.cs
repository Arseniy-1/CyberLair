using Project.Scripts.Skill;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.FireZone
{
    [CreateAssetMenu(fileName = "FireZoneSkill", menuName = "Skill/Hard/FireZone",order = 51)]
    public class FireZoneSkill : HardSkill
    {
        [field: SerializeField] public FireZone FireZonePrefab { get; private set; }
        [field: SerializeField] public float SpawnChance { get; private set; }
    }
}