using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillViews.Boomerang
{
    public class BoomerangView : MonoBehaviour
    {
        [SerializeField] private float rotationDuration = 1f;

        private Tween _rotationTween;
        
        private void OnEnable()
        {
            StartRotation();
        }

        private void OnDisable()
        {
            StopRotation();
        }

        private void StartRotation()
        {
            _rotationTween?.Kill();

            _rotationTween = transform
                .DOLocalRotate(new Vector3(0, 0, 360), rotationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);
        }

        private void StopRotation()
        {
            _rotationTween?.Kill();
        }
    }
}