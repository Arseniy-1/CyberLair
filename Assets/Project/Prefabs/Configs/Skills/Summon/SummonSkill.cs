using Project.Scripts.Weapon;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SummonSkill", menuName = "Skill/Simple/Summon", order = 51)]
public class SummonSkill : Skill
{
    [field: SerializeField] public Summon SummonPrefab { get; private set; }
}