using CodeBase.Infrastructure;

namespace CodeBase.Character.Enemy.State
{
    public class EnemyStateMachine : CharacterStateMashine
    {
        public EnemyStateMachine(EnemyMover mover, UnityEngine.Vector2 waitTime, ICoroutineRunner coroutineRunner, IEnemyDamagingService attacker, PlayerFinder playerFinder, System.Action die) 
        {
            States = new System.Collections.Generic.Dictionary<CharactersState, ICharacterState>
            {
                [CharactersState.Stay] = new StayState(this, waitTime, coroutineRunner, mover),
                [CharactersState.Move] = new MoveState(this, mover),
                [CharactersState.Attack] = new AttackState(this, attacker, mover, playerFinder),
                [CharactersState.Die] = new DieState(mover, die)
            };
        }
    }
}