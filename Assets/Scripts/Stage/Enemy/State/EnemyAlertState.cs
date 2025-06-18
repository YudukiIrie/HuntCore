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
            // === ��ԑJ�� ===
            // �ǐ�
            if (_enemy.GetDistanceToPlayer() > _attackDistance)
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.ChaseState);
            // �����]��
            else if (_enemy.GetAngleToPlayer() > _limitAngle)
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.TurnState);
            // �U��
            else if (_enemy.CheckAttackState())
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.AttackState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}