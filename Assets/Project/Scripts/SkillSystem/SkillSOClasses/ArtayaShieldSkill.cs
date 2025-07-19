using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillSOClasses
{
    [CreateAssetMenu(fileName = "ArtayaShieldSkill", menuName = "Skill/Hard/ArtayaShield", order = 51)]
    public class ArtayaShieldSkill : HardSkill
    {
        [field: SerializeField] public float ShieldRepairAmount { get; private set; }
    }
}