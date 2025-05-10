using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.Services
{
    [Serializable]
    public class SoundAnimation
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AudioSource Source { get; private set; }
        [field: SerializeField, MinMaxSlider(0.5f, 2f, true)] public Vector2 PitchRange { get; private set; }
    }
}