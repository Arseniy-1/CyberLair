using System;
using Cysharp.Threading.Tasks;
using Project.Prefabs.Configs.Skills.Boomerang;
using Project.Prefabs.Configs.Skills.Durability;
using Project.Prefabs.Configs.Skills.StormBlade;
using UnityEngine;

[Serializable]
public class StormBlade : ISkillInstance
{
    private float _maxRadius;
    private float _minRadius;
    private float _changingSpeed;

    private bool _isActive;
    private BoomerangOrbital _boomerang;

    public StormBlade(StormBladeSkill stormBladeSkill, BoomerangOrbital boomerang)
    {
        _maxRadius = stormBladeSkill.MaxRadius;
        _minRadius = stormBladeSkill.MinRadius;
        _changingSpeed = stormBladeSkill.ChangingSpeed;
        
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

    public void Disable()
    {
        throw new NotImplementedException();
    }
}