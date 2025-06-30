using DG.Tweening;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using Project.Scripts.Services.Enum;
using UniRx;
using UnityEngine;
using YG;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private CameraShakeSettings _settings;

    private readonly CompositeDisposable _disposable = new();
    private Transform _cameraTransform;
    
    private Vector3 _cameraOriginalPosition;
    private Tween _shakeTween;

    private void OnEnable()
    {
        _cameraTransform = _camera.transform;
        
        _cameraOriginalPosition = _cameraTransform.localPosition;

        MessageBrokerHolder.Camera
            .Receive<M_CameraShake>()
            .Subscribe(message => Shake(message.ShakeID))
            .AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable?.Clear();
        _shakeTween?.Kill();
    }

    private void Shake(ShakeID shakeID)
    {
        if(YandexGame.savesData.IsCameraShakeEnabled == false)
            return;

        if (_settings.TryGet(shakeID, out CameraShakeData shake) == false)
            return;
        
        _shakeTween?.Kill();

        _shakeTween = _camera.transform
            .DOShakePosition(shake.Duration, shake.Strength, shake.Vibrato, shake.Randomness)
            .OnKill(() => _cameraTransform.localPosition = _cameraOriginalPosition);
    }
}