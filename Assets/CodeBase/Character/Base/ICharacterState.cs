using CodeBase.Infrastrucure.State;

namespace CodeBase.Character
{
    public interface ICharacterState : IState
    {
        public void Update();
    }
}