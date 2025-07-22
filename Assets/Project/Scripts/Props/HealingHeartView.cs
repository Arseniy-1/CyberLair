using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Props
{
    public class HealingHeartView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _heart;
        [SerializeField] private float _pulseDuration = 1f; 
        [SerializeField] [Range(0, 1)] private float _minAlpha = 0.3f;

        private Tween _pulseTween;

        private void OnEnable()
        {
            StartPulseAnimation();
        }
    
        private void OnDisable()
        {
            _pulseTween?.Kill();
        }

        private void StartPulseAnimation()
        {
            _pulseTween?.Kill();
        
            _pulseTween = DOTween
                .To(
                    () => _heart.color.a,
                    alpha => 
                    {
                        var color = _heart.color;
                        color.a = alpha;
                        _heart.color = color;
                    },
                    _minAlpha,
                    _pulseDuration / 2)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}