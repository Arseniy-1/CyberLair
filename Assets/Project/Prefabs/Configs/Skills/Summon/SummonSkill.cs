using Project.Scripts.Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SummonSkill", menuName = "Skill/Mutant/Summon", order = 51)]
public class SummonSkill : MutantSkill
{
    [field: SerializeField] public Summon SummonPrefab { get; private set; }
}