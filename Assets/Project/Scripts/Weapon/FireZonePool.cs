using UnityEngine;

public class FireZonePool : Pool<FireZone>
{
    public FireZonePool(FireZone prefab, int startAmount) : base(prefab, startAmount)
    {
        CreateStartCount();
    }

    protected override FireZone Create()
    {
        FireZone template = Object.Instantiate(Prefab);
        template.gameObject.SetActive(false);
        Stack.Push(template);

        return template;
    }
}