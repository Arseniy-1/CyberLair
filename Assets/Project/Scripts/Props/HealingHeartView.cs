using DG.Tweening;
using UnityEngine;

public class HealingHeartView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _heart;
    [SerializeField] private float _pulseDuration = 1f; 
    [SerializeField] [Range(0, 1)] private float _minAlpha = 0.3f;

    private Sequence _pulseSequence;
    private Tween _pulseTween;

    private void OnEnable()
    {
        StartPulseAnimation();
    }
    
    private void OnDisable()
    {
        // _pulseSequence?.Kill();
        
        _pulseTween?.Kill();
    }

    private void StartPulseAnimation()
    {
        // _pulseSequence?.Kill();
        
        // Color originalColor = _heart.color;
        // float originalAlpha = originalColor.a;
        
        // _pulseSequence = DOTween.Sequence()
        //     .Append(
        //         DOTween.To(
        //             () => _heart.color.a,
        //             (alpha) => {
        //                 Color newColor = _heart.color;
        //                 newColor.a = alpha;
        //                 _heart.color = newColor;
        //             },
        //             _minAlpha,
        //             _pulseDuration / 2
        //         ).SetEase(Ease.InOutSine)
        //     )
        //  
        //     .Append(
        //         DOTween.To(
        //             () => _heart.color.a,
        //             (alpha) => {
        //                 Color newColor = _heart.color;
        //                 newColor.a = alpha;
        //                 _heart.color = newColor;
        //             },
        //             originalAlpha,
        //             _pulseDuration / 2
        //         ).SetEase(Ease.InOutSine)
        //     )
        //  
        //     .SetLoops(-1);
        
        _pulseTween?.Kill();
        
        _pulseTween = DOTween.To(
                () =>  _heart.color.a,
                alpha => {
                    var color = _heart.color;
                    color.a = alpha;
                    _heart.color = color;
                },
                _minAlpha,
                _pulseDuration / 2
            )
            .SetLoops(-1, LoopType.Yoyo);
    }
}