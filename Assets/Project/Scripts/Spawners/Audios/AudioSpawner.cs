using UnityEngine;

namespace Project.Scripts.Spawners.Audios
{
    public class AudioSpawner : Spawner<Audio>
    {
        public AudioSpawner(Transform parentTransform, Audio audioPrefab)
        {
            var audioFabric = new AudioFabric(parentTransform);
            
            Pool = new AudioPool(audioFabric, audioPrefab, StartAmount);
        }
    }
}