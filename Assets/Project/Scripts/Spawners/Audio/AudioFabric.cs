using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts.Spawners.Audio
{
    public class AudioFabric
    {
       private readonly Transform _parentTransform;

       public AudioFabric(Transform parentTransform)
       {
           _parentTransform = parentTransform;
       }
       
       public Audio Create(Audio audio)
       {
           Audio doneEnemy = Object.Instantiate(audio, _parentTransform, true);

           return doneEnemy;
       }
    }
}