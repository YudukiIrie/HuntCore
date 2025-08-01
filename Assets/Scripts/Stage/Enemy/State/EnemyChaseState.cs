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

        // �f�[�^�L���b�V���p
        float _chaseSpeed;
        float _chaseRotSpeed;
        float _stopDist;

        public EnemyChaseState(Enemy enemy)
        {
            _enemy = enemy;

            _chaseSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ChaseSpeed;
            _chaseRotSpeed = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ChaseRotSpeed;
            _stopDist = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).StopDist;
        }

        public void Enter()
        {
            _enemy.Animation.Chase();
            _targetRot = _enemy.transform.rotation;
        }

        public void Update()
        {
            MoveUpdate();

            Rotate();

            Transition();
        }

        public void FixedUpdate()
        {
            MoveFixedUpdate();
        }

        public void Exit()
        {
            // �J�ڌ�ɓ����Ăق����Ȃ����߁A�������͂����Z�b�g
            _enemy.Rigidbody.velocity = Vector3.zero;
        }

        /// <summary>
        /// Update()�p�ړ�����
        /// </summary>
        void MoveUpdate()
        {
            _velocity = _enemy.Player.transform.position - _enemy.transform.position;
            _velocity = Vector3.ProjectOnPlane(_velocity, Vector3.up);
            _velocity = _velocity.normalized * _chaseSpeed;
        }

        /// <summary>
        /// ��]
        /// </summary>
        void Rotate()
        {
            // �x�N�g���̑傫����0�ɓ������ꍇ�͏��O
            if (_velocity.magnitude > 0.001f)
                _targetRot = Quaternion.LookRotation(_velocity);
            // ��]���x�̎擾
            float rotSpeed = _chaseRotSpeed * Time.deltaTime;
            // ��]
            Quaternion rot = _enemy.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _enemy.transform.rotation = rot;
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // �x��
            if (_enemy.GetDistanceToPlayer() <= _stopDist)
                _enemy.StateMachine.TransitionTo(EnemyState.Alert);
        }

        /// <summary>
        /// FixedUpdate()�p�ړ�����
        /// </summary>
        void MoveFixedUpdate()
        {
            var vel = _velocity;
            vel.y = _enemy.Rigidbody.velocity.y;
            _enemy.Rigidbody.velocity = vel;
        }
    }
}