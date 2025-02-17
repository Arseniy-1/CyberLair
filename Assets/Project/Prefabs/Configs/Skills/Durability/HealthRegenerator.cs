using System.Collections;
using UnityEngine;

public class HealthRegenerator : MonoBehaviour
{
    [SerializeField] private float _healInterval = 1f; 

    private Health _health; 

    private HealthRegenerateAmount _healthRegenerateAmount;
    private WaitForSeconds _healWait;
    
    public void Initialize(Health health, HealthRegenerateAmount healthRegenerateAmount)
    {
        _healthRegenerateAmount = healthRegenerateAmount;
        _health = health;
        _healWait = new WaitForSeconds(_healInterval);
        StartCoroutine(RegenerateHealth());
    }
    
    private IEnumerator RegenerateHealth()
    {
        while (enabled)
        {
            yield return _healWait;
            _health.Heal(_healthRegenerateAmount.CurrentValue);
        }
    }
}