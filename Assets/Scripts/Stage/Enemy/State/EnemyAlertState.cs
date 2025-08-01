using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// “GŒx‰úó‘Ô
    /// </summary>
    public class EnemyAlertState : IState
    {
        Enemy _enemy;   // “GƒNƒ‰ƒX

        // ƒf[ƒ^ƒLƒƒƒbƒVƒ…—p
        float _actionkDist;
        float _limitAngle;
        float _attackProb;
        float _attackDist;

        public EnemyAlertState(Enemy enemy)
        {
            _enemy = enemy;

            _actionkDist = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ActionDist;
            _limitAngle = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).LimitAngle;
            _attackProb = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackProb;
            _attackDist = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackDist;
        }

        public void Enter()
        {
            _enemy.Animation.Alert();
        }

        public void Update()
        {
            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }

        /// <summary>
        /// ó‘Ô‘JˆÚ
        /// </summary>
        void Transition()
        {
            // ’ÇÕ
            if (_enemy.GetDistanceToPlayer() > _actionkDist)
                _enemy.StateMachine.TransitionTo(EnemyState.Chase);
            // •ûŒü“]Š·
            else if (_enemy.GetAngleToPlayer() > _limitAngle)
                _enemy.StateMachine.TransitionTo(EnemyState.Turn);
            // UŒ‚
            else if (_enemy.CheckAttackState())
            {
                // ’ÊíUŒ‚
                if (_enemy.GetDistanceToPlayer() <= _attackDist)
                    _enemy.StateMachine.TransitionTo(EnemyState.Attack);
                // ’ÜUŒ‚
                else
                    _enemy.StateMachine.TransitionTo(EnemyState.ClawAttack);
            }
        }
    }
}