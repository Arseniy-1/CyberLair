using Project.Scripts.Skill;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.ArtayaShield
{
    [CreateAssetMenu(fileName = "ArtayaShieldSkill", menuName = "Skill/Hard/ArtayaShield", order = 51)]
    public class ArtayaShieldSkill : HardSkill
    {
        [field: SerializeField] public float ShieldRepairAmount { get; private set; }
    }
}