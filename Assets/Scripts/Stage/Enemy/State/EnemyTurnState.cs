using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�����]�����
    /// </summary>
    public class EnemyTurnState : IState
    {
        Enemy _enemy;           // �G�N���X
        Quaternion _targetRot;  // �����ׂ��p�x   
        float _turnSpeed;
        float _attackAngle;

        public EnemyTurnState(Enemy enemy)
        {
            _enemy = enemy;
            _turnSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).TurnSpeed;
            _attackAngle = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackAngle;
        }

        public void Enter()
        {
            _enemy.Animation.Walk();

            // �J�ڂ������_�ł̃^�[�Q�b�g�p�x���擾
            _targetRot = Quaternion.LookRotation(_enemy.GetDirectionToPlayer());
        }

        public void Update()
        {
            // === �����]�� ===
            // �^�[�Q�b�g�p�x�̎擾
            _targetRot = Quaternion.LookRotation(_enemy.GetDirectionToPlayer());
            // ��]���x�̎擾
            float rotSpeed = _turnSpeed * Time.deltaTime;
            // ��]
            Quaternion rot = _enemy.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _enemy.transform.rotation = rot;

            // === ��ԑJ�� ===
            // �x��
            if (_enemy.GetAngleToPlayer() <= _attackAngle)
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