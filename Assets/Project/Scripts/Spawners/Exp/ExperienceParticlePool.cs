using Project.Scripts.Props;
using UnityEngine;

namespace Project.Scripts.Spawners.Exp
{
    public class ExperienceParticlePool : Pool<ExperienceParticle>
    {
        public ExperienceParticlePool(ExperienceParticle prefab, int startAmount) 
            : base(prefab, startAmount) { }

        protected override ExperienceParticle Create()
        {
            var experienceParticle = Object.Instantiate(Prefab);
            experienceParticle.gameObject.SetActive(false);
        
            return experienceParticle;
        }
    }
}