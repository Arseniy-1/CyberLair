using System.Collections;
using Project.Scripts.Services.Enum;
using Project.Scripts.Services.Extensions;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillViews
{
    public class CommonSkillView : MonoBehaviour
    {
        private readonly int _playingTrigger = Animator.StringToHash("Playing");
        
        [SerializeField] private Animator _animator;
        [SerializeField] private float _lifeTime = 0.4f;
        [SerializeField] private AudioID _audio;
        [SerializeField] private SpriteRenderer _sprite;
        
        private Coroutine _playingCoroutine;

        private void OnDisable()
        {
            EndPlaying();
        }

        public void Initialize()
        {
            EndPlaying();
            
            _sprite.enabled = true;

            _playingCoroutine = StartCoroutine(Playing());
        }

        public void EndPlaying()
        {
            _animator.ResetTrigger(_playingTrigger);
            
            if (_playingCoroutine != null)
                StopCoroutine(_playingCoroutine);
            
            _playingCoroutine = null;
            
            _sprite.enabled = false;
        }

        private IEnumerator Playing()
        {
            var wait = new WaitForSeconds(_lifeTime);
            _animator.SetTrigger(_playingTrigger);
            _audio.Play();
            
            yield return wait;
            
            EndPlaying();
        }
    }
}