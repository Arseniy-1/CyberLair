using System;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.ChainZap
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