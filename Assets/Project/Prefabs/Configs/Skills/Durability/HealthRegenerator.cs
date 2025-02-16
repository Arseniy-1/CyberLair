using System.Collections;
using UnityEngine;

public class HealthRegenerator : MonoBehaviour
{
    [SerializeField] private float _healInterval = 1f; 

    private Health _health; 

    private RegenerateAmount _regenerateAmount;
    private WaitForSeconds _healWait;
    
    public void Initialize(Health health, RegenerateAmount regenerateAmount)
    {
        _regenerateAmount = regenerateAmount;
        _health = health;
        _healWait = new WaitForSeconds(_healInterval);
        StartCoroutine(RegenerateHealth());
    }
    
    private IEnumerator RegenerateHealth()
    {
        while (enabled)
        {
            yield return _healWait;
            _health.Heal(_regenerateAmount.CurrentValue);
        }
    }
}