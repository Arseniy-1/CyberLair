using System;
using Cysharp.Threading.Tasks;
using Project.Prefabs.Configs.Skills.Boomerang;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.StormBlade
{
    [Serializable]
    public class StormBlade
    {
        [SerializeField] private float _maxRadius;
        [SerializeField] private float _minRadius;
        [SerializeField] private float _changingSpeed;
        
        private bool _isActive;
        private BoomerangOrbital _boomerang;
        
        public void Initialize(BoomerangOrbital boomerang)
        {
            _isActive = true;
            _boomerang = boomerang;
            RadiusChanging().Forget();
        }

        private async UniTaskVoid RadiusChanging()
        {
            while (_isActive)
            {
                _boomerang.ApplyRadius(
                    Mathf.PingPong(Time.fixedTime * _changingSpeed, _maxRadius - _minRadius) + _minRadius);
                
                await UniTask.Yield(PlayerLoopTiming.FixedUpdate);
            }
        }
    }
}