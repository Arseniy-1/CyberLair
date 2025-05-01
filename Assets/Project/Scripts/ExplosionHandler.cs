using UnityEngine;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using UniRx;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private SoundPlayer _explosionSound;
    [SerializeField] private CameraShakeSettings _cameraShakeSettings;

    private CompositeDisposable _disposable;
    
    private void Awake()
    {
        _disposable = new CompositeDisposable();
        
        MessageBrokerHolder.Game.Receive<M_Exploded>().Subscribe((message) => HandleExplosion())
            .AddTo(_disposable);
    }
    
    private void HandleExplosion()
    {
        MessageBrokerHolder.Camera.Publish(new M_CameraShake(_cameraShakeSettings));
        _explosionSound.Play();
    }
}