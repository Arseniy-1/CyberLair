using Project.Scripts.SkillSystem.SkillInstances;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "FireZoneSkill", menuName = "Skill/Hard/FireZone", order = 51)]
    public class FireZoneSkill : HardSkill
    {
        [field: SerializeField] public FireZone FireZonePrefab { get; private set; }
        [field: SerializeField] public float SpawnChance { get; private set; }
    }
}