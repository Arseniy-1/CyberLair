using Project.Scripts.SkillSystem.SkillInstances;
using UnityEngine;

namespace Project.Scripts.Spawners.StreamingEnergies
{
    public class StreamingEnergyPool : Pool<StreamingEnergy>
    {
        public StreamingEnergyPool(StreamingEnergy prefab, int startAmount) : base(prefab, startAmount) { }

        protected override StreamingEnergy Create()
        {
            StreamingEnergy template = Object.Instantiate(Prefab);
            template.gameObject.SetActive(false);

            return template;
        }
    }
}