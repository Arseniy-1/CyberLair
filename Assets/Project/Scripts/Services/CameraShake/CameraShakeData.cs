using System;
using Project.Scripts.Services.Enum;
using UnityEngine;

namespace Project.Scripts.Services.CameraShake
{
    [Serializable]
    public class CameraShakeData
    {
        [field: SerializeField] public ShakeID ShakeId { get; private set; }
        [field: SerializeField] [field: Range(0, 5f)] public float Duration { get; private set; }
        [field: SerializeField] [field: Range(0, 1f)] public float Strength { get; private set; }
        [field: SerializeField] [field: Range(0, 20)] public int Vibrato { get; private set; }
        [field: SerializeField] [field: Range(0, 180f)] public float Randomness { get; private set; }
    }
}