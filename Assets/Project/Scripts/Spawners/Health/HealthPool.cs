using UnityEngine;

public class HealthPool : Pool<HealingHeart>
{
    public HealthPool(HealingHeart prefab, int startAmount) : base(prefab, startAmount) { }
        
    protected override HealingHeart Create()
    {
        var health =  Object.Instantiate(Prefab);
        health.gameObject.SetActive(false);
        
        return health;
    }
}