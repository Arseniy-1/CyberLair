using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class SoundAnimationReceiver : MonoBehaviour
    {
        [SerializeField] private SoundAnimationEvents _soundAnimationEvents;
        [SerializeField] private List<SoundAnimation> _soundAnimations;

        private void OnEnable()
        {
            _soundAnimationEvents.SoundInvoked += OnSoundInvoked;
        }

        private void OnDisable()
        {
            _soundAnimationEvents.SoundInvoked -= OnSoundInvoked;
        }

        private void OnSoundInvoked(string soundAnimation)
        {
            SoundAnimation sound = _soundAnimations.FirstOrDefault(x => x.Name == soundAnimation);

            sound?.Audio.Play();
        }
    }
}