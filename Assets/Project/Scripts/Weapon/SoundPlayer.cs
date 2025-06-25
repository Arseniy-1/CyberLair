using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.MessageBroker.SoundMessageBrokers;
using Project.Scripts.Spawners.Audio;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private SoundSettings _soundSettings;

    private readonly Dictionary<AudioID, Audio> _audioDatas = new ();
    private AudioSpawner _audioSpawner;
    
    private CompositeDisposable _disposable;

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
        foreach (AudioID audioID in _audioDatas.Keys)
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
        
        audio.Initialize(audioData);
        _audioDatas.Add(audioData.AudioID, audio);
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

public enum AudioID
{
    PlayerTakeDamage = 0,
    PlayerExperience = 1,
    PlayerJump = 2,
    PlayerWalk = 3,
    PlayerShoot = 4,
    OutOfAmmo = 5,
    Reload = 6,
    FullReload = 7,
    TrooperShoot = 8,
    SniperShoot = 9,
    EnemyTakeDamage = 10,
    MetalEnemyTakeDamage = 11,
    
    Explosion = 12,
    EnemyJump,
    
    BossHalo,
    BossHaloGround,
    
    BossFireAreaAttack,
    BossFireBreath,
    BossLavaWaveAttack,
    BossLavaWaveShoot,
    
    BossLaserBeam,
    BossShakeAttack,
    BossShakeProjectTile,
    BossShieldAttack,
    
    BossOrbitalAttack,
    BossSlashAttack,
    BossSlashView,
    BossSoulClotAttack,
    BossSoulClotProjectTile,
    BossSoulOrbitalProjectTile,
    
    SkillSelect,
    SkillApply,
    MenuChoose,
    MenuCancel,
    
    HellCat,
    StreamingEnergy,
    Thunder,
    InternalVoltage,
    HamsterWeapon
}

public static class AudioIDExtensions
{
    public static void Play(this AudioID audioID)
    {
        MessageBrokerHolder.Audio
            .Publish(new M_PlayAudio(audioID));
    }
    
    public static void Stop(this AudioID audioID)
    {
        MessageBrokerHolder.Audio
            .Publish(new M_StopAudio(audioID));
    }
}