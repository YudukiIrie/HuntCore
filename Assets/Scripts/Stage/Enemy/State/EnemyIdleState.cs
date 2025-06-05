using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// “G‘Ò‹@ó‘Ô
    /// </summary>
    public class EnemyIdleState : IState
    {
        Enemy _enemy;   // “GƒNƒ‰ƒX
        float _findDistance;

        public EnemyIdleState(Enemy enemy)
        {
            _enemy = enemy;
            _findDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).FindDistance;
        }

        public void Enter()
        {
            _enemy.Animation.ResetAll();
        }

        public void Update()
        {
            // === ó‘Ô‘JˆÚ ===
            // ™ôšK
            if (_enemy.CheckDistanceToPlayer() <= _findDistance)
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.RoarState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
