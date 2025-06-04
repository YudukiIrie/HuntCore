using System.Collections;
using System.Collections.Generic;
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

        public EnemyAlertState(Enemy enemy)
        {
            _enemy = enemy;
            _attackDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackDistance;
        }

        public void Enter()
        {
            _enemy.Animation.Alert();
        }

        public void Update()
        {
            // === ‘JˆÚ ===
            // ’ÇÕ
            if (_enemy.CheckDistanceToPlayer() > _attackDistance)
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.ChaseState);
            // UŒ‚
            else _enemy.StateMachine.TransitionTo(_enemy.StateMachine.AttackState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}