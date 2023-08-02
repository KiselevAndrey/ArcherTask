namespace CodeBase.Weapon
{
    public interface IWeapon
    {
        public void Attack(UnityEngine.LayerMask damageLayer, int damage);

        public bool CanAttack { get; }
        public bool AttackEnded { get; }
    }
}