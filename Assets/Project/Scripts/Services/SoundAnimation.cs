using System;
using UnityEngine;

namespace Project.Scripts.Services
{
    [Serializable]
    public class SoundAnimation
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AudioID Audio { get; private set; }
    }
}