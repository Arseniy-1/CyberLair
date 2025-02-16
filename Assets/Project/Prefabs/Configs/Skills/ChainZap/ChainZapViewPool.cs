using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Zap
{
    public class ChainZapViewPool : Pool<ChainZapView>
    {
        public ChainZapViewPool(ChainZapView prefab, int startAmount) : base(prefab, startAmount) { }

        protected override ChainZapView Create()
        {
            var view = Object.Instantiate(Prefab);
            view.gameObject.SetActive(false);

            return view;
        }
    }
}