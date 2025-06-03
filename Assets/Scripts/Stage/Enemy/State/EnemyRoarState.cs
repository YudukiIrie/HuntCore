using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G���K���
    /// </summary>
    public class EnemyRoarState : IState
    {
        Enemy _enemy;   // �G�N���X
        float _roarDistance;

        public EnemyRoarState(Enemy enemy)
        {
            _enemy = enemy;
            _roarDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).RoarDistance;
        }

        public void Enter()
        {
            _enemy.Animation.Roar();

            // �v���C���[�ɏՌ���^����
            if (_enemy.CheckDistanceToPlayer() <= _roarDistance)
                _enemy.Player.TakeImpact();
        }

        public void Update()
        {
            // === �J�� ===
            // �x��
            if (_enemy.Animation.IsAnimFinished(EnemyAnimation.HashRoar))
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
