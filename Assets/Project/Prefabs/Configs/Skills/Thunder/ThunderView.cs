using System.Collections;
using UnityEngine;

namespace Project.Scripts.Weapon.ActiveSkills
{
    public class ThunderView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _lifeTime = 0.4f;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SpriteRenderer _sprite;
        
        private readonly int _strikeTrigger = Animator.StringToHash("Strike");
        private Coroutine _striking;

        private void OnDisable()
        {
            EndStriking();
        }

        public void Initialize()
        {
            EndStriking();
            
            _sprite.enabled = true;

            _striking = StartCoroutine(Striking());
        }

        public void EndStriking()
        {
            _animator.ResetTrigger(_strikeTrigger);
            
            if(_striking != null)
                StopCoroutine(_striking);
            
            _striking = null;
            
            _sprite.enabled = false;
        }

        private IEnumerator Striking()
        {
            var wait = new WaitForSeconds(_lifeTime);
            _animator.SetTrigger(_strikeTrigger);
            _audioSource.Play();
            
            yield return wait;
            
            EndStriking();
        }
    }
}