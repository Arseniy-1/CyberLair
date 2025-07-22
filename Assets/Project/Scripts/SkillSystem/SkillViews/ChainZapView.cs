using System;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillViews
{
    public class ChainZapView : MonoBehaviour, IDestoyable<ChainZapView>
    { 
        public event Action<ChainZapView> OnDestroyed;
        
        [field: SerializeField] public LineRenderer ZapView { get; private set; }

        public void Disable()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}