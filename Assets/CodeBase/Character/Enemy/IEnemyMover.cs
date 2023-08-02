namespace CodeBase.Character.Enemy
{
    public interface IEnemyMover
    {
        public void MoveToNewPoint();
        public void Stop();
        public void LookAtPlayer();
    }
}