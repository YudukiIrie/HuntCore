using UnityEngine;

namespace Stage.Enemies
{
    public class EnemyGetHitState : IState
    {
        Enemy _enemy;

        public EnemyGetHitState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.Animation.GetHit();
        }

        public void Update()
        {
            // === ó‘Ô‘JˆÚ ===
            // Œx‰ú
            if (_enemy.Animation.CheckEndAnim(EnemyAnimation.HashGetHit))
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}