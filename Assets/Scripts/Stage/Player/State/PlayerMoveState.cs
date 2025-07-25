using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�ړ����
    /// </summary>
    public class PlayerMoveState : IState
    {
        Player _player;         // �v���C���[�N���X
        Vector3 _velocity;      // �ړ������Ƒ��x
        Quaternion _targetRot;  // �����ׂ��p�x

        // �f�[�^�L���b�V���p
        float _moveSpeed;
        float _rotSpeed;

        public PlayerMoveState(Player player)
        {
            _player = player;

            _moveSpeed = PlayerData.Data.DrawnMoveSpeed;
            _rotSpeed = PlayerData.Data.DrawnRotSpeed;
        }

        public void Enter()
        {
            _player.Animation.Move();
            _targetRot = _player.transform.rotation;
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

        }

        /// <summary>
        /// Update()�p�ړ�����
        /// </summary>
        void MoveUpdate()
        {
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            // �ړ������Ƒ��x������
            Transform cam = Camera.main.transform;
            _velocity = ((cam.forward * input.y) + (cam.right * input.x)).normalized;
            // ���������x�N�g����ڐG���̖ʂɉ����x�N�g���ɕϊ�
            _velocity = Vector3.ProjectOnPlane(_velocity, _player.NormalVector).normalized * _moveSpeed;
        }

        /// <summary>
        /// ��]
        /// </summary>
        void Rotate()
        {
            // �x�N�g���̑傫����0�̊p�x�͓n�������Ȃ����ߋɏ��̍ۂ͌v�Z���Ȃ�
            if (_velocity.magnitude > 0.001f)
                _targetRot = Quaternion.LookRotation(_velocity.normalized);
            // ��]���x�̎擾
            float rotSpeed = _rotSpeed * Time.deltaTime;
            // ��]
            Quaternion rot = _player.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _player.transform.rotation = rot;
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // �ʏ�
            if (_player.Action.Player.Move.ReadValue<Vector2>() == Vector2.zero)
                _player.StateMachine.TransitionTo(PlayerState.Idle);
            // ���C�g�U��
            else if (_player.Action.Player.Attack.IsPressed())
                _player.StateMachine.TransitionTo(PlayerState.LightAttack);
            // �K�[�h
            else if (_player.Action.Player.Guard.IsPressed())
                _player.StateMachine.TransitionTo(PlayerState.Guard);
            // ���
            else if (_player.Action.Player.Roll.IsPressed())
                _player.StateMachine.TransitionTo(PlayerState.Roll);
        }

        /// <summary>
        /// FixedUpdate()�p�ړ�����
        /// </summary>
        void MoveFixedUpdate()
        {
            // Y�������̈ړ����l����Y�̂ݕ���
            Vector3 vel = _velocity;
            vel.y = _player.Rigidbody.velocity.y;
            _player.Rigidbody.velocity = vel;
        }
    }
}
