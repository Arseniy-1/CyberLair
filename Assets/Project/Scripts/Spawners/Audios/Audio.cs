using System;
using System.Collections;
using Project.Scripts.Interfaces;
using Project.Scripts.Services.Audios;
using Project.Scripts.Services.Enum;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Spawners.Audios
{
    public class Audio : MonoBehaviour, IDestoyable<Audio>
    {
        [SerializeField] private AudioSource audioSource;
        
        private Coroutine _timer;
        
        public event Action<Audio> OnDestroyed;
        
        public AudioID AudioID { get; private set; }

        public void Initialize(AudioData audioData)
        {
            AudioID = audioData.AudioID;
            audioSource.clip = audioData.AudioClip;
            audioSource.outputAudioMixerGroup = audioData.AudioMixer;
            audioSource.volume = audioData.Volume;
            audioSource.pitch = Random.Range(audioData.PitchRange.x, audioData.PitchRange.y);
            audioSource.loop = audioData.IsLooped;
            
            audioSource.Stop();
        }
        
        public void PlayLoop() => audioSource.Play();

        public void PlayOneShot()
        { 
            if (_timer != null)
                StopCoroutine(_timer);
            
            audioSource.Play();

            _timer = StartCoroutine(DestroyTimer());
        }

        public void Stop()
        {
            audioSource.Stop();
            
            OnDestroyed?.Invoke(this);

            if (_timer == null) 
                return;
            
            StopCoroutine(_timer);
            
            _timer = null;
        } 

        private IEnumerator DestroyTimer()
        {
            var wait = new WaitForSeconds(audioSource.clip.length / audioSource.pitch);

            yield return wait;
            
            Stop();
        }
    }
}