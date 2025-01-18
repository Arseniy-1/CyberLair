using System.Collections.Generic;
using Project.Scripts.Weapon.ActiveSkills;
using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boomerang Skill", menuName = "Skill/Active/Boomerang", order = 51)]
public class BoomerangSkill : ActiveSkill
{
    [SerializeField] private Boomerang _boomerangPrefab;
    [SerializeField] private SkillConfig _speedConfig;
    
    private readonly List<Boomerang> _boomerangs = new();
    
    public override void Apply(WeaponHolder weaponHolder, int level)
    {
        if (level > MaxLevel && level < 1)
            return;
        
        Boomerang boomerang = Instantiate(_boomerangPrefab, weaponHolder.transform);
        boomerang.Initialize(weaponHolder.transform.position);

        var speed = _speedConfig.Multipliers[level - 1];
        
        _boomerangs.Add(boomerang);
        DistributeEqually(weaponHolder.transform, speed);
    }
    
    private void DistributeEqually(Transform holder, float speed)
    {
        if (_boomerangs.IsNullOrEmpty()) return;

        int count = _boomerangs.Count;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            var currentBoomerang = _boomerangs[i];
            
            float angle = i * angleStep;
            Vector3 position = CalculatePosition(angle, holder);
            
            currentBoomerang.transform.position = position;
            currentBoomerang.CalculateOffset();
            currentBoomerang.ApplyStats(speed);
        }
    }
    
    private Vector3 CalculatePosition(float angle, Transform holder)
    {
        float radians = angle * Mathf.Deg2Rad;

        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);
        
        Vector3 localPosition = new Vector3(x, y, 0);
        return holder.position + localPosition;
    }
}