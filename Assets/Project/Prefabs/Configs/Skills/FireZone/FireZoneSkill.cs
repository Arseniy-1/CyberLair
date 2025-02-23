using Project.Prefabs.Configs.Skills.FireZone;
using UnityEngine;

[CreateAssetMenu(fileName = "FireZoneSkill", menuName = "Skill/Hard/FireZone",order = 51)]
public class FireZoneSkill : HardSkill
{
    [field: SerializeField] public FireZone FireZonePrefab { get; private set; }
    [field: SerializeField] public FireZoneSpawner _fireZoneSpawner { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float _chance { get; private set; }
    [field: SerializeField] public FireZoneManager _fireZoneManager {get; private set;}
}