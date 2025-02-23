using UnityEngine;

[CreateAssetMenu(fileName = "ThunderSkill", menuName = "Skill/Simple/Thunder", order = 51)]
public class ThunderSkill : Skill, IThunderStats
{
    [field: SerializeField] public float ActionRadius { get; private set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float StrikesCount { get; private set; }
    [field: SerializeField] public float ShootsNeeded { get; private set; }
}