using Object = UnityEngine.Object;

namespace Project.Scripts.Spawners.ParticleEffects
{
    public class DeathEffectsPool : Pool<Effect>
    {
        public DeathEffectsPool(Effect prefab, int startAmount) 
            : base(prefab, startAmount) { }
        
        protected override Effect Create()
        {
            var effect = Object.Instantiate(Prefab);
            effect.gameObject.SetActive(false);
            
            return effect;
        }
    }
}