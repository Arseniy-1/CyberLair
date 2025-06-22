using System.Collections;
using UnityEngine;

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
            float randomDelay = Random.Range(_minDisableTime, _maxDisableTime);
            yield return new WaitForSeconds(randomDelay);

            ActivateShield();

            yield return new WaitForSeconds(_activeTime);

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