using System.Collections.Generic;
using System.Linq;
using Project.Scripts.MessageBroker;
using Project.Scripts.MessageBroker.SoundMessageBrokers;
using Project.Scripts.Services.Enum;
using Project.Scripts.Spawners.Audios;
using UniRx;
using UnityEngine;

namespace Project.Scripts.Services.Audios
{
    public class SoundPlayer : MonoBehaviour
    {
        private readonly List<Audio> _audioDatas = new ();
        private readonly CompositeDisposable _disposable = new ();
    
        [SerializeField] private SoundSettings _soundSettings;

        private AudioSpawner _audioSpawner;

        private void Awake()
        {
            _audioSpawner = new AudioSpawner(transform, _soundSettings.AudioPrefab);
        }

        private void OnEnable()
        {
            MessageBrokerHolder.Audio
                .Receive<M_PlayAudio>()
                .Subscribe(message => Play(message.AudioID))
                .AddTo(_disposable);
        
            MessageBrokerHolder.Audio
                .Receive<M_StopAudio>()
                .Subscribe(message => Stop(message.AudioID))
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            foreach (Audio audio in _audioDatas.ToList())
            {
                Stop(audio.AudioID);
            }
        
            _disposable?.Clear();
        }

        public void Play(AudioID audioID)
        {
            if (_soundSettings.TryGet(audioID, out AudioData audioData) == false)
                return;
        
            Audio audio = _audioSpawner.Spawn();
        
            audio.gameObject.SetActive(true);
            audio.Initialize(audioData);
            _audioDatas.Add(audio);
            audio.OnDestroyed += RemoveAudio;
        
            if (audioData.IsLooped)
                PlayLoop(audio);
            else
                PlayOneShot(audio);
        }

        private void PlayOneShot(Audio audio) => audio.PlayOneShot();
    
        private void PlayLoop(Audio audio) => audio.PlayLoop();

        private void Stop(AudioID audioID)
        {
            Audio currentAudio = _audioDatas.FirstOrDefault(audio => audio.AudioID == audioID);
        
            if (currentAudio == false)
                return;
        
            currentAudio.Stop();
        }

        private void RemoveAudio(Audio audio)
        {
            audio.OnDestroyed -= RemoveAudio;
        
            _audioDatas.Remove(audio);
        }
    }
}