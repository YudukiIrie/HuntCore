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
            // === �ړ��v�Z ===
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            // �ړ������Ƒ��x������
            Transform cam = Camera.main.transform;
            _velocity = ((cam.forward * input.y) + (cam.right * input.x)).normalized;
            // ���������x�N�g����ڐG���̖ʂɉ����x�N�g���ɕϊ�
            _velocity = Vector3.ProjectOnPlane(_velocity, _player.NormalVector).normalized * _moveSpeed;

            // === ��]�v�Z ===
            // �x�N�g���̑傫����0�̊p�x�͓n�������Ȃ����ߋɏ��̍ۂ͌v�Z���Ȃ�
            if (input.magnitude > 0.001f)
                _targetRot = Quaternion.LookRotation(_velocity.normalized);

            // === ��ԑJ�� ===
            // �ʏ�
            if (_player.Action.Player.Move.ReadValue<Vector2>() == Vector2.zero)
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            // ���C�g�U��
            else if (_player.Action.Player.Attack.IsPressed())
                _player.StateMachine.TransitionTo(_player.StateMachine.LightAttackState);
        }

        public void FixedUpdate()
        {
            // === �擾���������Ƒ��x���g���ړ� ===
            // Y�������̈ړ����l����Y�̂ݕ���
            Vector3 vel = _velocity;
            vel.y = _player.Rigidbody.velocity.y;
            _player.Rigidbody.velocity = vel;

            // === �擾�����p�x���I�u�W�F�N�g�ɔ��f ===
            // ���������������ĉ�]���Ăق������ߐ�����݂���
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, _rotSpeed);
        }

        public void Exit()
        {

        }
    }
}
