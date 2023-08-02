namespace CodeBase.Character
{
    public interface IDamagingService
    {
        public void TryAttack(UnityEngine.Vector3 target);
        public bool CanAttack();
        public bool AttackEnded { get; }
    }
}