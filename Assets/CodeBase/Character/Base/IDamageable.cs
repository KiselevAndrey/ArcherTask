namespace CodeBase.Character
{
    public interface IDamageable
    {
        public bool IsAlive { get; }
        public void TakeDamage(int damage);
    }
}