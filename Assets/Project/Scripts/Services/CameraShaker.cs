using DG.Tweening;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using UniRx;
using UnityEngine;
using YG;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private readonly CompositeDisposable _disposable = new();
    private Transform _cameraTransform;
    
    private Vector3 _cameraOriginalPosition;
    private Tween _shakeTween;

    private void OnEnable()
    {
        _cameraTransform = _camera.transform;
        
        _cameraOriginalPosition = _cameraTransform.localPosition;

        MessageBrokerHolder.Camera.Receive<M_CameraShake>().Subscribe(Shake)
            .AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable?.Clear();
        _shakeTween?.Kill();
    }

    private void Shake(M_CameraShake settings)
    {
        if(YandexGame.savesData.IsCameraShakeEnabled == false)
            return;
        
        _shakeTween?.Kill();

        _shakeTween = _camera.transform.DOShakePosition(settings.ShakeSettings.Duration,
                settings.ShakeSettings.Strength, settings.ShakeSettings.Vibrato, settings.ShakeSettings.Randomness)
            .OnComplete(() => _cameraTransform.localPosition = _cameraOriginalPosition);
    }
}