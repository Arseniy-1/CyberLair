using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class AudioData
{
    [field: SerializeField] public AudioID AudioID { get; private set; }
    [field: SerializeField] public AudioClip AudioClip { get; private set; }
    [field: SerializeField] public AudioMixerGroup AudioMixer { get; private set; }
    [field: SerializeField] public float Volume { get; private set; }
    [field: SerializeField, MinMaxSlider(0.5f, 3f, showFields: true)] public Vector2 PitchRange { get; private set; }
    [field: SerializeField] public bool IsLooped { get; private set; }
}