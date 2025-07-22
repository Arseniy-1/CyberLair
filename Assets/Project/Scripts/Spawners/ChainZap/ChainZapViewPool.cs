using Project.Scripts.SkillSystem.SkillViews;
using UnityEngine;

namespace Project.Scripts.Spawners.ChainZap
{
    public class ChainZapViewPool : Pool<ChainZapView>
    {
        public ChainZapViewPool(ChainZapView prefab, int startAmount) 
            : base(prefab, startAmount) { }

        protected override ChainZapView Create()
        {
            var view = Object.Instantiate(Prefab);
            view.gameObject.SetActive(false);

            return view;
        }
    }
}