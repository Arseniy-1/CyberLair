using System.Collections;
using UnityEngine;

public class ShieldRegenerator : MonoBehaviour
{
    [SerializeField] private float _regenerateInterval = 1f;
    [SerializeField] private float _afterDamagePause = 3f;

    private ShieldAmount _shield;
    private Health _health;

    private float ShieldRegenerateAmount => _shield.MaxShield * 0.1f;
    private WaitForSeconds _regenerateWait;
    private WaitForSeconds _pauseWait;

    private Coroutine _regenerationCoroutine;
    private Coroutine _resumeCoroutine;

    private void OnDisable()
    {
        _health.DamageTaken -= OnDamageTaken;

        if (_regenerationCoroutine == null)
            return;
        
        StopCoroutine(_regenerationCoroutine);
        _regenerationCoroutine = null;

        if (_resumeCoroutine == null)
            return;
        
        StopCoroutine(_resumeCoroutine);
        _resumeCoroutine = null;
    }

    public void Initialize(ShieldAmount shield, Health health)
    {
        _health = health;
        _health.DamageTaken += OnDamageTaken;

        _shield = shield;
        _regenerateWait = new WaitForSeconds(_regenerateInterval);
        _pauseWait = new WaitForSeconds(_afterDamagePause);
        _regenerationCoroutine = StartCoroutine(RegenerateShield());
    }

    private IEnumerator RegenerateShield()
    {
        while (enabled)
        {
            yield return _regenerateWait;
            _shield.RepairShield(ShieldRegenerateAmount);
        }
    }

    private void OnDamageTaken(float amount)
    {
        if (_regenerationCoroutine != null)
        {
            StopCoroutine(_regenerationCoroutine);
            _regenerationCoroutine = null;
        }

        if (_resumeCoroutine != null)
            StopCoroutine(_resumeCoroutine);

        _resumeCoroutine = StartCoroutine(ResumeRegenerationAfterDelay());
    }

    private IEnumerator ResumeRegenerationAfterDelay()
    {
        yield return _pauseWait;
        _regenerationCoroutine = StartCoroutine(RegenerateShield());
        _resumeCoroutine = null;
    }
}