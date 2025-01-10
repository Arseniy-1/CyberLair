using System.Collections.Generic;
using Project.Scripts.Weapon.ActiveSkills;
using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boomerang Skill", menuName = "Skill/Active/Boomerang", order = 51)]
public class BoomerangSkill : ActiveSkill
{
    [SerializeField] private Boomerang _boomerangPrefab;
    [SerializeField] private float _radius;
    
    private readonly List<Boomerang> _boomerangs = new();
    
    public override void Apply(WeaponHolder weaponHolder)
    {
        Boomerang activeWeapon = Instantiate(_boomerangPrefab, weaponHolder.transform);
        activeWeapon.Initialize(_radius, weaponHolder.transform);
            
        _boomerangs.Add(activeWeapon);
        DistributeEqually(weaponHolder.transform);
    }
    
    private void DistributeEqually(Transform holder)
    {
        if (_boomerangs.IsNullOrEmpty()) return;

        int count = _boomerangs.Count;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep;
            Vector3 position = CalculatePosition(angle, holder);
            
            _boomerangs[i].transform.position = position;
            _boomerangs[i].CalculateOffset();
        }
    }
    
    private Vector3 CalculatePosition(float angle, Transform holder)
    {
        float radians = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians) * _radius;
        float y = Mathf.Sin(radians) * _radius;
        
        Vector3 localPosition = new Vector3(x, y, 0);
        return holder.position + localPosition;
    }
}