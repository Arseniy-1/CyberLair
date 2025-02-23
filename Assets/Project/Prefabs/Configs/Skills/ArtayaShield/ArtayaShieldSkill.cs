using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtayaShieldSkill", menuName = "Skill/Simple/ArtayaShield", order = 51)]
public class ArtayaShieldSkill : Skill
{
    [field: SerializeField] public float ShieldRepairAmount { get; private set; }
}