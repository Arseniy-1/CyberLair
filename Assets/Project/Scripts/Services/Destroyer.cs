using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private Health _health;
    private IDieable _dieable;

    private void OnDestroy()
    {
        if (_health != null)
        {
            _health.LostHealth -= RaiseDeath;
        }
    }
    
    public void Initialize(Health health, IDieable dieable)
    {
        _health = health;
        _dieable = dieable;
        
        _health.LostHealth += RaiseDeath;
    }
    
    private void RaiseDeath()
    {
        _dieable.Die();
    }
}