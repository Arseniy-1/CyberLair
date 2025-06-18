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

    private void OnEnable()
    {
        _cameraTransform = _camera.transform;

        MessageBrokerHolder.Camera.Receive<M_CameraShake>().Subscribe(Shake)
            .AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable?.Clear();
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