namespace CodeBase.Character.Player.State
{
    public class PlayerStateMachine : CharacterStateMashine
    {
        public PlayerStateMachine(PlayerBehaviour playerBehaviour, PlayerMover mover, IDamagingService attacker, World.IEnemySpawner enemySpawner, System.Action onFail)
        {
            States = new System.Collections.Generic.Dictionary<CharactersState, ICharacterState>
            {
                [CharactersState.Stay] = new StayState(mover, enemySpawner),
                [CharactersState.Move] = new MoveState(playerBehaviour, mover),
                [CharactersState.Attack] = new AttackState(this, attacker, mover, enemySpawner),
                [CharactersState.Die] = new DieState(onFail)
            };
        }
    }
}