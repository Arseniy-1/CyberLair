using System;
using Project.Prefabs.Configs.Skills.StunZap;
using System.Collections.Generic;
using Project.Prefabs.Configs.Skills.Durability;

[Serializable]
public class StunZap : ISkillInstance
{
    private float _stunDuration;

    private SkillData _date;
    
    private List<Bullet> _subscribedBullets;
    
    public StunZap(SkillData skillData, StunZapSkill skill)
    {
        _date = skillData;
        _stunDuration = skill.StunDuration;

        _date.WeaponHolder.Weapon.Shooted += InnerSubscribe;
    }
    
    public void Disable()
    {
        _date.WeaponHolder.Weapon.Shooted -= InnerSubscribe;

        foreach (var bullet in _subscribedBullets)
            bullet.OnDamagableCollided -= StunEnemy;
    }
    
    private void InnerSubscribe(Bullet bullet)
    {
        if(_subscribedBullets.Contains(bullet))
            return;
        
        bullet.OnDamagableCollided += StunEnemy;
        _subscribedBullets.Add(bullet);
    }

    private void StunEnemy(IDamageable damageable)
    {
        (damageable as IStunable)?.TakeStun(_stunDuration);
    }
}
