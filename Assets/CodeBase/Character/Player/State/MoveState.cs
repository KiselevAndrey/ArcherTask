namespace CodeBase.Character.Player.State
{
    public class MoveState : ICharacterState
    {
        private readonly PlayerBehaviour _playerBehaviour;
        private readonly PlayerMover _mover;

        public MoveState(PlayerBehaviour playerBehaviour, PlayerMover mover)
        {
            _playerBehaviour = playerBehaviour;
            _mover = mover;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Update()
        {
            _mover.Move(_playerBehaviour.InputVector);
        }
    }
}