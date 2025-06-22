using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthRegenerator : MonoBehaviour
{
    [SerializeField] private float _healInterval = 1f; 

    private Health _health; 

    private HealthRegenerateAmount _healthRegenerateAmount;
    private WaitForSeconds _healWait;
    private Coroutine _regeneratingCoroutine;

    private void OnDisable()
    {
        DisableRegeneration();
    }
    
    public void Initialize(Health health, HealthRegenerateAmount healthRegenerateAmount)
    {
        _healthRegenerateAmount = healthRegenerateAmount;
        _health = health;
        _healWait = new WaitForSeconds(_healInterval);
        
        DisableRegeneration();
        
        _regeneratingCoroutine = StartCoroutine(Regenerating());
    }
    
    private IEnumerator Regenerating()
    {
        while (isActiveAndEnabled)
        {
            yield return _healWait;
            _health.Heal(_healthRegenerateAmount.CurrentValue);
        }
    }

    private void DisableRegeneration()
    {
        if (_regeneratingCoroutine == null)
            return;
        
        StopCoroutine(_regeneratingCoroutine);
        _regeneratingCoroutine = null;
    }
}