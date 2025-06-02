using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G��������
    /// </summary>
    public class EnemyWanderState : IState
    {
        Enemy _enemy;   // �G�N���X

        public EnemyWanderState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.Animation.Wander();

            // === AI�ݒ� ===
            // �ړ����x�Ɖ�]���x
            _enemy.Agent.speed = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).WanderSpeed;
            _enemy.Agent.angularSpeed = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).WanderRotSpeed;
            // �ڕW�ɂ��ǂ蒅�����Ƃ���ڕW�܂ł̋���
            _enemy.Agent.stoppingDistance = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).WanderStoppingDistance;
        }

        public void Update()
        {
            // �ړI�n���Ȃ��Ƃ�
            if (!_enemy.Agent.hasPath)
            {
                // �w��͈͓��ŖړI�n��ݒ�
                var range = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).WanderRange;
                var posX = _enemy.transform.position.x + Random.Range(-range, range);
                var posZ = _enemy.transform.position.z + Random.Range(-range, range);
                Vector3 nextPos = new(posX, _enemy.transform.position.y, posZ);
                _enemy.Agent.SetDestination(nextPos);
            }

            // ���������ҋ@�ւ̑J�ڊm�����擾
            var percent = EnemyDataList.Data.GetData(EnemyData.Type.NormalEnemy).WanderToIdlePercent;

            // === ��ԑJ�� ===
            // �ҋ@
            if (_enemy.IsTransitionHit(percent))
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.IdleState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            // �ړI�̃��Z�b�g
            _enemy.Agent.ResetPath();
        }
    }
}
