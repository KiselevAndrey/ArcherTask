namespace CodeBase.Character.Enemy
{
    public interface IEnemyDamagingService : IDamagingService
    {
        public bool CanAttack(UnityEngine.Vector3 target);
    }
}