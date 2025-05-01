namespace Game.Weapon
{
    public interface IWeaponSkill
    {
        void Execute();
        float GetCooldown();
    }
}
