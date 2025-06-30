using UnityEngine;

public class HellCatPool : Pool<HellCat>
{
    public HellCatPool(HellCat prefab, int startAmount) : base(prefab, startAmount) { }

    protected override HellCat Create()
    {
        var hellCat = Object.Instantiate(Prefab);
        hellCat.gameObject.SetActive(false);
        
        return hellCat;
    }
}