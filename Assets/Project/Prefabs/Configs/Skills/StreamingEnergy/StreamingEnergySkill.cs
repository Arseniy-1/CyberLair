using UnityEngine;

[CreateAssetMenu(fileName = "StreamingEnergySkill", menuName = "Skill/Mutant/StreamingEnergy", order = 51)]
public class StreamingEnergySkill : MutantSkill
{
    [field: SerializeField] public StreamingEnergy Prefab { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float Chance { get; private set; }
}