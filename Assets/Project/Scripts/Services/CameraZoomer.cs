using DG.Tweening;
using Project.Scripts.MessageBroker;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class CameraZoomer : MonoBehaviour
    {
        [SerializeField, MinMaxSlider(9f, 20f)] private Vector2 _cameraZoomSize;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _zoomDuration;
        
        private readonly CompositeDisposable _disposable = new ();
        private Tween _cameraZoomTween;

        private void OnEnable()
        {
            MessageBrokerHolder.Camera
                .Receive<M_CameraZoomIn>()
                .Subscribe(_ => ApplyCameraZoom(_cameraZoomSize.x))
                .AddTo(_disposable);
            
            MessageBrokerHolder.Camera
                .Receive<M_CameraZoomOut>()
                .Subscribe(_ => ApplyCameraZoom(_cameraZoomSize.y))
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable?.Clear();
            _cameraZoomTween?.Kill();
        }
        
        private void ApplyCameraZoom(float endValue)
        {
            _cameraZoomTween?.Kill();
            
            _cameraZoomTween = _camera.DOOrthoSize(endValue, _zoomDuration).SetEase(Ease.InOutSine);
        }
    }
}