using System.Collections.Generic;

namespace CodeBase.Character
{
    public abstract class CharacterStateMashine : IStateMachine, IStateMachineInternal
    {
        protected Dictionary<CharactersState, ICharacterState> States;

        private ICharacterState _activeState;
        public CharactersState ActiveStateName { get; private set; }

        private bool _changeStateIsBlocked = false;

        public void ChangeState(CharactersState newState)
        {
            if (ActiveStateName != newState 
                && _changeStateIsBlocked == false)
                EnterTo(newState);
        }

        public void UpdateState()
        {
            _activeState.Update();
        }

        public void BlockChangeState(bool needBlock)
        {
            _changeStateIsBlocked = needBlock;
        }

        private void EnterTo(CharactersState newState)
        {
            ChangeTo(newState).Enter();
        }

        private ICharacterState ChangeTo(CharactersState newState)
        {
            _activeState?.Exit();

            ICharacterState state = GetState(newState);
            _activeState = state;
            ActiveStateName = newState;

            return state;
        }

        private ICharacterState GetState(CharactersState newState) =>
            States[newState];
    }
}