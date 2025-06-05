using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// “GUŒ‚ƒXƒe[ƒg
    /// </summary>
    public class EnemyAttackState : IState
    {
        Enemy _enemy;   // “GƒNƒ‰ƒX
        
        public EnemyAttackState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.Animation.Attack();
        }

        public void Update()
        {
            // === ó‘Ô‘JˆÚ ===
            // Œx‰ú
            if (_enemy.Animation.IsAnimFinished(EnemyAnimation.HashAttack))
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.AlertState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}