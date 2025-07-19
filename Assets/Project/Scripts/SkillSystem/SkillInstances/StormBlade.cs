using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using Project.Scripts.SkillSystem.SkillSOClasses;
using Project.Scripts.SkillSystem.SkillViews.Boomerang;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillInstances
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