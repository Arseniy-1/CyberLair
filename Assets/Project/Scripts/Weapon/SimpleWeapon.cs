using Project.Scripts.Weapon;

public class SimpleWeapon : Weapon
{
    public override bool TryAttack()
    {
        if (IsReloaded == false)
            return false;

        Attack();
        IsReloaded = false;

        return true;
    }
}