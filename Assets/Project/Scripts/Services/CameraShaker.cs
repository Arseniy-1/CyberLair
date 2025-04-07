using DG.Tweening;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace.Tween
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [Header("Shake Settings")]
        [SerializeField] private float _duration;
        [SerializeField] private float _strength;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _randomness;
        
        private Vector3 _originalPosition;
        private CompositeDisposable _disposable;

        private void OnEnable()
        {
            _disposable = new CompositeDisposable();
            
            MessageBrokerHolder.Enemy.Receive<CameraShakeMessage>().Subscribe(_ => Shake())
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        private void Shake()
        {
            _originalPosition = _camera.transform.localPosition;
            
            _camera.transform.DOShakePosition(_duration, _strength, _vibrato, _randomness)
                .OnComplete(() => _camera.transform.localPosition = _originalPosition);
        }
    }
}