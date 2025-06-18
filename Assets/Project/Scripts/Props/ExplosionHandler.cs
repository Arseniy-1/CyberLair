using UnityEngine;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using UniRx;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private SoundPlayer _explosionSound;
    [SerializeField] private CameraShakeSettings _cameraShakeSettings;

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
        MessageBrokerHolder.Camera
            .Publish(new M_CameraShake(_cameraShakeSettings));
        
        _explosionSound.Play();
    }
}