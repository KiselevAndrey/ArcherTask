namespace CodeBase.Character
{
    public interface IStateMachine
    {
        public void ChangeState(CharactersState newState);
        public void UpdateState();
    }

    public interface IStateMachineInternal
    {
        public void BlockChangeState(bool needBlock);
    }
}