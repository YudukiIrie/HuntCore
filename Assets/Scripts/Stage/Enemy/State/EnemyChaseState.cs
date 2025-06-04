using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Enemies
{
    /// <summary>
    /// �G�ǐՏ��
    /// </summary>
    public class EnemyChaseState : IState
    {
        Enemy _enemy;           // �G�N���X
        Vector3 _velocity;      // �ړ������Ƒ��x
        Quaternion _targetRot;  // �����ׂ��p�x
        float _chaseSpeed;
        float _chaseRotSpeed;
        float _attackDistance;

        public EnemyChaseState(Enemy enemy)
        {
            _enemy = enemy;
            _chaseSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ChaseSpeed;
            _chaseRotSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ChaseRotSpeed;
            _attackDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackDistance;
        }

        public void Enter()
        {
            _enemy.Animation.Chase();
            _targetRot = _enemy.transform.rotation;
        }

        public void Update()
        {
            // === �ړ��v�Z ===
            _velocity = _enemy.Player.transform.position - _enemy.transform.position;
            _velocity = Vector3.ProjectOnPlane(_velocity, Vector3.up);
            _velocity = _velocity.normalized * _chaseSpeed;

            // === ��]�v�Z ===
            // �x�N�g���̑傫����0�ɓ������ꍇ�͏��O
            if (_velocity.magnitude > 0.001f)
                _targetRot = Quaternion.LookRotation(_velocity);

            // === �J�� ===
            // �x��
            if (_enemy.CheckDistanceToPlayer() <= _attackDistance)
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.AlertState);
        }

        public void FixedUpdate()
        {
            // === �ړ� ===
            var vel = _velocity;
            vel.y = _enemy.Rigidbody.velocity.y;
            _enemy.Rigidbody.velocity = vel;

            // === ��] ===
            _enemy.transform.rotation = Quaternion.RotateTowards(_enemy.transform.rotation, _targetRot, _chaseRotSpeed);
        }

        public void Exit()
        {

        }
    }
}