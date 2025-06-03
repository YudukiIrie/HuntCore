using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�x�����
    /// </summary>
    public class EnemyAlertState : IState
    {
        Enemy _enemy;   // �G�N���X
        float _attackDistance;

        public EnemyAlertState(Enemy enemy)
        {
            _enemy = enemy;
            _attackDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackDsitance;
        }

        public void Enter()
        {
            _enemy.Animation.Alert();
        }

        public void Update()
        {
            // === �J�� ===
            if (_enemy.CheckDistanceToPlayer() > _attackDistance)
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.ChaseState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}