using System;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Zap
{
    public class ChainZapView : MonoBehaviour, IDestoyable<ChainZapView>
    {
        [field: SerializeField] public LineRenderer ZapView { get; private set; }

        public event Action<ChainZapView> OnDestroyed;

        public void Disable()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}