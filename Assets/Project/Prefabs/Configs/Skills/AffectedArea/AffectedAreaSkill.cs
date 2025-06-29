using Project.Scripts.MessageBroker.CameraMessageBrokers;
using Project.Scripts.Services.Enum;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.AffectedArea
{
    [CreateAssetMenu(fileName = "AffectedAreaSkill", menuName = "Skill/Simple/AffectedArea", order = 51)]
    public class AffectedAreaSkill : Skill
    {
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public LayerMask LayerMask { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float DamageProportion { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float Chance { get; private set; }
        [field: SerializeField] public ShakeID ShakeID { get; private set; } = ShakeID.Medium;
    }
}