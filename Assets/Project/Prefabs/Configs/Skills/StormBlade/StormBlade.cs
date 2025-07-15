using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Prefabs.Configs.Skills.Boomerang;
using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.StormBlade
{
    [Serializable]
    public class StormBlade : ISkillInstance
    {
        private float _maxRadius;
        private float _minRadius;
        private float _changingSpeed;

        private BoomerangOrbital _boomerang;

        public StormBlade(StormBladeSkill stormBladeSkill, Orbital boomerang, CancellationToken token)
        {
            _maxRadius = stormBladeSkill.MaxRadius;
            _minRadius = stormBladeSkill.MinRadius;
            _changingSpeed = stormBladeSkill.ChangingSpeed;
            _boomerang = boomerang as BoomerangOrbital;
        
            RadiusChanging(token).Forget();
        }

        public void Disable() { }
    
        private async UniTaskVoid RadiusChanging(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                _boomerang.ApplyRadius(
                    Mathf.PingPong(Time.fixedTime * _changingSpeed, _maxRadius - _minRadius) + _minRadius);

                await UniTask.Yield(PlayerLoopTiming.FixedUpdate, cancellationToken: token);
            }
        }
    }
}