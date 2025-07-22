using Project.Scripts.MessageBroker;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using UniRx;
using UnityEngine;

namespace Project.Scripts.Props
{
    public class ExplosionHandler : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new ();
        
        [SerializeField] private AudioID _explosionSound = AudioID.Explosion;
        [SerializeField] private ShakeID _shakeID = ShakeID.Medium;
    
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
}