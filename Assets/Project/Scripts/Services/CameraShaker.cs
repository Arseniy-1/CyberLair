using DG.Tweening;
using Project.Scripts.MessageBroker.CameraMessageBrokers;
using UniRx;
using UnityEngine;
using YG;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private CompositeDisposable _disposable;
    private Transform _cameraTransform;

    private void OnEnable()
    {
        _disposable = new CompositeDisposable();
        _cameraTransform = _camera.transform;

        MessageBrokerHolder.Camera.Receive<M_CameraShake>().Subscribe(Shake)
            .AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Dispose();
    }

    private void Shake(M_CameraShake settings)
    {
        if(YandexGame.savesData.IsCameraShakeEnabled == false)
            return;
        
        Vector3 originalPosition = _cameraTransform.localPosition;

        _camera.transform.DOShakePosition(settings.ShakeSettings.Duration,
                settings.ShakeSettings.Strength, settings.ShakeSettings.Vibrato, settings.ShakeSettings.Randomness)
            .OnComplete(() => _cameraTransform.localPosition = originalPosition);
    }
}