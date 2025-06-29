using System;
using Project.Scripts.Services.Enum;
using UnityEngine;

namespace Project.Scripts.MessageBroker.CameraMessageBrokers
{
    [Serializable]
    public class CameraShakeData
    {
        [field: SerializeField] public ShakeID ShakeId { get; private set; }
        [field: SerializeField, Range(0, 5f)] public float Duration { get; private set; }
        [field: SerializeField, Range(0, 1f)] public float Strength { get; private set; }
        [field: SerializeField, Range(0, 20)] public int Vibrato { get; private set; }
        [field: SerializeField, Range(0, 180f)] public float Randomness { get; private set; }
    }
}