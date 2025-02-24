public class SkillData
{
    public SkillData(WeaponHolder weaponHolder, PlayerStats playerStats, Jumper playerJumper)
    {
        WeaponHolder = weaponHolder;
        PlayerStats = playerStats;
        PlayerJumper = playerJumper;
    }

    public WeaponHolder WeaponHolder { get; private set; }
    public PlayerStats PlayerStats { get; private set; }
    public Jumper PlayerJumper { get; private set; }
}