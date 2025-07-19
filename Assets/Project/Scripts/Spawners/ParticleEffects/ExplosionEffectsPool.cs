using UnityEngine;

namespace Project.Scripts.Spawners.ParticleEffects
{
    public class ExplosionEffectsPool : Pool<Effect>
    {
        public ExplosionEffectsPool(Effect prefab, int startAmount) : base(prefab, startAmount) { }
        
        protected override Effect Create()
        {
            var effect =  Object.Instantiate(Prefab);
            effect.gameObject.SetActive(false);
            
            return effect;
        }
    }
}