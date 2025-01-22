using UnityEngine;

public class ExperiencePaticlePool : Pool<ExperienceParticle>
{
    public ExperiencePaticlePool(ExperienceParticle prefab, int startAmount) : base(prefab, startAmount)
    {
    }

    protected override ExperienceParticle Create()
    {
        var experienceParticle = Object.Instantiate(Prefab);
        experienceParticle.gameObject.SetActive(false);
        
        return experienceParticle;
    }
}