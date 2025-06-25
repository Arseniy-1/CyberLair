using System.Linq;
using Project.Scripts.Spawners.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundSettings", menuName = "Sound/SoundSettings", order = 51)]
public class SoundSettings : ScriptableObject
{
    [SerializeField] private AudioData[] _audioData;
    
    [field: SerializeField] public Audio AudioPrefab { get; private set; }

    public bool TryGet(AudioID audioID, out AudioData audioData)
    {
        audioData = _audioData.FirstOrDefault(data => data.AudioID == audioID);

        return audioData != null;
    }
}