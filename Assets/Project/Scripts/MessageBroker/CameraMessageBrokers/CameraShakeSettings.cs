using System;
using UnityEngine;

namespace Project.Scripts.MessageBroker.CameraMessageBrokers
{
    [Serializable]
    public struct CameraShakeSettings
    {
        [field: SerializeField, Range(0, 1f)] public float Duration { get; private set; }
        [field: SerializeField, Range(0, 1f)] public float Strength { get; private set; }
        [field: SerializeField, Range(0, 20)] public int Vibrato { get; private set; }
        [field: SerializeField, Range(0, 180f)] public float Randomness { get; private set; }
    }
}