using System.Collections.Generic;
using Project.Scripts.Weapon.ActiveSkills;
using Sirenix.Utilities;
using UnityEngine;

[CreateAssetMenu(fileName = "BoomerangSkill", menuName = "Skill/Simple/Boomerang", order = 51)]
public class BoomerangSkill : Skill
{
    [SerializeField] private Boomerang _boomerangPrefab;
    [SerializeField] private SkillConfig _speedConfig;
    
    private readonly List<Boomerang> _boomerangs = new();
    
    public override void Apply(SkillData skillData)
    {
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