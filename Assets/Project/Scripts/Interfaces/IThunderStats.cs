using Project.Prefabs.Configs.Skills.Thunder;
using UnityEngine;

namespace Project.Scripts.Interfaces
{
    public interface IThunderStats
    {
        public float ActionRadius { get; }
        public LayerMask LayerMask { get; }
        public int Damage { get; }
        public float StrikesCount { get; }
        public float ShootsNeeded { get; }
        public CommonSkillView CommonSkillView { get; }
    }
}