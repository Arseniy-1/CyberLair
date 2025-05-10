using DG.Tweening;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private AudioLowPassFilter _lowPassFilter;
        [SerializeField, MinMaxSlider(0, 22000, true)] private Vector2Int _musicCutoffRange;
        [SerializeField] private float _musicCutoffDuration;

        private readonly CompositeDisposable _disposable = new();
        private Tween _cutoffTween;
        
        private void Awake()
        {
            _lowPassFilter.cutoffFrequency = _musicCutoffRange.y;
            
            MessageBrokerHolder.Game.Receive<M_GamePaused>().Subscribe(message => OnGamePaused())
                .AddTo(_disposable);
            
            MessageBrokerHolder.Game.Receive<M_GameUnpaused>().Subscribe(message => OnGameUnpaused())
                .AddTo(_disposable);
        }

        private void OnGamePaused()
        {
            Debug.Log("On Game Paused");
            
            ApplyCutoffFrequency(_musicCutoffRange.x);
        }

        private void OnGameUnpaused()
        {
            Debug.Log("On Game UnPaused");
            ApplyCutoffFrequency(_musicCutoffRange.y);
        }

        private void ApplyCutoffFrequency(float endValue)
        {
            _cutoffTween?.Kill();

            _cutoffTween = DOTween
                .To(() => _lowPassFilter.cutoffFrequency, currentValue => _lowPassFilter.cutoffFrequency = currentValue, endValue, _musicCutoffDuration)
                .SetUpdate(true)
                .SetEase(Ease.InOutSine).OnComplete(() => _cutoffTween = null);
        }
    }
}