namespace CodeBase.Infrastrucure.State
{
    public interface IExitableState
    {
        public void Exit();
    }

    public interface IState : IExitableState
    {
        public void Enter();
    }
}