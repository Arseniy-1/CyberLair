using Project.Scripts.Weapon.ActiveSkills.MagicArrow;

public class PhantomArrows : SkillInstance
{
    private MagicArrowSpawner _phantomArrowSpawner;

    private SkillData _data;
    private PhantomArrowsSkill _skill;

    public PhantomArrows(SkillData data, PhantomArrowsSkill skill)
    {
        _data = data;
        _skill = skill;
        _phantomArrowSpawner = new MagicArrowSpawner();

        _phantomArrowSpawner.Initialize(_skill.PhantomArrowPrefab, _data.WeaponHolder.transform);
    }

    public override void Disable()
    {
        _phantomArrowSpawner.Disable();
    }
}