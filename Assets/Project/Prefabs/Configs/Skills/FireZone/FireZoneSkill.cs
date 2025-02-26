using Project.Prefabs.Configs.Skills.FireZone;
using UnityEngine;

[CreateAssetMenu(fileName = "FireZoneSkill", menuName = "Skill/Hard/FireZone",order = 51)]
public class FireZoneSkill : HardSkill
{
    [field: SerializeField] public FireZone FireZonePrefab { get; private set; }
}