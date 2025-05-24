using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemy
{
    /// <summary>
    /// �G�ҋ@���
    /// </summary>
    public class EnemyIdleState : IState
    {
        Enemy _enemy;   // �G�N���X

        public EnemyIdleState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.Animation.ResetAll();
        }

        public void Update()
        {
            // �ҋ@���炤����ւ̑J�ڊm�����擾
            var percent = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).IdleToWanderPercent;

            // === ��ԑJ�� ===
            if (_enemy.IsTransitionHit(percent))
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.WanderState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
