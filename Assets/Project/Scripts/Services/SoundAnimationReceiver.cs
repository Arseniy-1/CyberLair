using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Services.Extensions;
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

        private void OnSoundInvoked(string soundAnimationName)
        {
            SoundAnimation sound = _soundAnimations
                .FirstOrDefault(soundAnimation => soundAnimation.Name == soundAnimationName);

            sound?.Audio.Play();
        }
    }
}