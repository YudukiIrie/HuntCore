using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// “GŒx‰úó‘Ô
    /// </summary>
    public class EnemyAlertState : IState
    {
        Enemy _enemy;   // “GƒNƒ‰ƒX
        float _attackDistance;
        float _limitAngle;

        public EnemyAlertState(Enemy enemy)
        {
            _enemy = enemy;
            _attackDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackDistance;
            _limitAngle = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).LimitAngle;
        }

        public void Enter()
        {
            _enemy.Animation.Alert();
        }

        public void Update()
        {
            // === ó‘Ô‘JˆÚ ===
            // ’ÇÕ
            if (_enemy.GetDistanceToPlayer() > _attackDistance)
                _enemy.StateMachine.TransitionTo(EnemyState.Chase);
            // •ûŒü“]Š·
            else if (_enemy.GetAngleToPlayer() > _limitAngle)
                _enemy.StateMachine.TransitionTo(EnemyState.Turn);
            // UŒ‚
            else if (_enemy.CheckAttackState())
                _enemy.StateMachine.TransitionTo(EnemyState.Attack);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}