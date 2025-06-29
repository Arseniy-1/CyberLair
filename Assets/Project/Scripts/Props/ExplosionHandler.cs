using UnityEngine;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using UniRx;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private AudioID _explosionSound = AudioID.Explosion;
    [SerializeField] private ShakeID _shakeID = ShakeID.Medium;

    private readonly CompositeDisposable _disposable = new();
    
    private void Awake()
    {
        MessageBrokerHolder.Game
            .Receive<M_Exploded>()
            .Subscribe(_ => HandleExplosion())
            .AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable?.Clear();
    }

    private void HandleExplosion()
    {
        _shakeID.Shake();
        
        _explosionSound.Play();
    }
}