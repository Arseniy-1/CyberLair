using System;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class SoundAnimationEvents : MonoBehaviour
    {
        public event Action<string> SoundInvoked;

        public void PlaySound(string soundName)
        {
            SoundInvoked?.Invoke(soundName);
        }
    }
}