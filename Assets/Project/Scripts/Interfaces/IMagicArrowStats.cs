using Project.Prefabs.Configs.Skills.MagicArrow;
using UnityEngine;

namespace Project.Scripts.Interfaces
{
    public interface IMagicArrowStats
    {
        public float Radius { get; }
        public float Delay { get; }
        public LayerMask LayerMask { get; }
        public MagicArrow MagicArrowPrefab { get; }
    }
}