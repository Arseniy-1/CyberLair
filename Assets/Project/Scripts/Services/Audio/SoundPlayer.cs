using System.Collections.Generic;
using System.Linq;
using Project.Scripts.MessageBroker.SoundMessageBrokers;
using Project.Scripts.Spawners.Audio;
using UniRx;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private SoundSettings _soundSettings;

    private readonly Dictionary<AudioID, Audio> _audioDatas = new ();
    private readonly CompositeDisposable _disposable = new();
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
        foreach (AudioID audioID in _audioDatas.Keys.ToList())
        {
            Stop(audioID);
        }
        
        _disposable?.Clear();
    }

    public void Play(AudioID audioID)
    {
        if (_soundSettings.TryGet(audioID, out AudioData audioData) == false)
            return;

        if (_audioDatas.TryGetValue(audioData.AudioID, out Audio audio) == false)
            audio = _audioSpawner.Spawn();
        
        audio.gameObject.SetActive(true);
        audio.Initialize(audioData);
        _audioDatas.TryAdd(audioData.AudioID, audio);
        audio.OnDestroyed += RemoveAudio;
        
        if(audioData.IsLooped)
            PlayLoop(audio);
        else
            PlayOneShot(audio);
    }

    private void PlayOneShot(Audio audio) => audio.PlayOneShot();
    
    private void PlayLoop(Audio audio) => audio.PlayLoop();

    private void Stop(AudioID audioID)
    {
        if (_audioDatas.TryGetValue(audioID, out Audio audio) == false)
            return;
        
        audio.Stop();
    }

    private void RemoveAudio(Audio audio)
    {
        audio.OnDestroyed -= RemoveAudio;
        
        _audioDatas.Remove(audio.AudioID);
    }
}