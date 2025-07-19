using System.Collections;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillViews
{
    [RequireComponent(typeof(InvincibilityCollisionHandler))]
    public class Invincibility : MonoBehaviour
    {
        [SerializeField] private float _minDisableTime = 3f;
        [SerializeField] private float _maxDisableTime = 10f;
        [SerializeField] private float _activeTime = 2f;

        [SerializeField] private InvincibilityCollisionHandler invincibilityCollision;
    
        private Coroutine _invincibilityCoroutine;

        private void OnEnable()
        {
            invincibilityCollision.gameObject.SetActive(false);

            DisableInvincibility();
        
            _invincibilityCoroutine = StartCoroutine(InvincibilityRoutine());
        }

        private void OnDisable()
        {
            DisableInvincibility();
        }

        private IEnumerator InvincibilityRoutine()
        {
            while (isActiveAndEnabled)
            {
                var waitForDelay = new WaitForSeconds(Random.Range(_minDisableTime, _maxDisableTime));
                var waitForActiveTime = new WaitForSeconds(_activeTime); 
            
                yield return waitForDelay;

                ActivateShield();

                yield return waitForActiveTime;

                DeactivateShield();
            }
        }

        private void ActivateShield()
        {
            invincibilityCollision.gameObject.SetActive(true);
        }

        private void DeactivateShield()
        {
            invincibilityCollision.gameObject.SetActive(false);
        }

        private void DisableInvincibility()
        {
            if(_invincibilityCoroutine == null)
                return;
        
            StopCoroutine(_invincibilityCoroutine);
            _invincibilityCoroutine = null;
        }
    }
}