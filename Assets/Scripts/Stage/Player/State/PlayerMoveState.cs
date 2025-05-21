using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
{
    /// <summary>
    /// �v���C���[�ړ����
    /// </summary>
    public class PlayerMoveState : IPlayerState
    {
        // �v���C���[�N���X
        Player _player;

        // �ړ������Ƒ��x
        Vector3 _velocity;

        // �����ׂ��p�x
        Quaternion _targetRot;

        public PlayerMoveState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Move();
            _targetRot = _player.transform.rotation;
        }

        public void Update()
        {
            // == �ړ��v�Z ==
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            // ===== ������Ԃ݂̂ɂȂ������ߕs�v =====
            //// �����Ə�����̋��
            //float currentSpeed;
            //if (input.magnitude <= PlayerData.Data.MagnitudeBorder)
            //    currentSpeed = PlayerData.Data.WalkSpeed;
            //else
            //    currentSpeed = PlayerData.Data.JogSpeed;
            //// �����Ă��邩�ǂ����̋��
            //currentSpeed = _player.Action.Player.Run.IsPressed() ? PlayerData.Data.RunSpeed : currentSpeed;
            // ========================================

            // �ړ������Ƒ��x������
            Transform cam = Camera.main.transform;
            _velocity = ((cam.forward * input.y) + (cam.right * input.x)).normalized;
            // ���������x�N�g����ڐG���̖ʂɉ����x�N�g���ɕϊ�
            _velocity = Vector3.ProjectOnPlane(_velocity, _player.NormalVector).normalized * PlayerData.Data.DrawnMoveSpeed;

            // == ��]�v�Z ==
            // �x�N�g���̑傫����0�̊p�x�͓n�������Ȃ����ߋɏ��̍ۂ͌v�Z���Ȃ�
            if (input.magnitude > 0.001f)
                _targetRot = Quaternion.LookRotation(_velocity.normalized);

            // ===== ������Ԃ݂̂ɂȂ������ߕs�v =====
            // == Animator�Ɉړ����x�𔽉f ==
            // BlendTree�ŃA�j���[�V�������Ǘ����邽��RunSpeed��1�Ƃ��������𔽉f
            //_player.Animator.SetFloat("Speed", currentSpeed / PlayerData.Data.RunSpeed);
            // ========================================

            // == ��ԑJ�� ==
            // �ʏ�
            if (_player.Action.Player.Move.ReadValue<Vector2>() == Vector2.zero)
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            // �U��
            else if (_player.Action.Player.Attack.IsPressed())
                _player.StateMachine.TransitionTo(_player.StateMachine.Attack1State);
        }

        public void FixedUpdate()
        {
            // == �擾���������Ƒ��x���g���ړ� ==
            // Y�������̈ړ����l����Y�̂ݕ���
            Vector3 vel = _velocity;
            vel.y = _player.Rigidbody.velocity.y;
            _player.Rigidbody.velocity = vel;

            // == �擾�����p�x���I�u�W�F�N�g�ɔ��f ==
            // ���������������ĉ�]���Ăق������ߐ�����݂���
            float rotSpeed = PlayerData.Data.DrawnRotSpeed * Time.deltaTime;
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, rotSpeed);
        }

        public void Exit()
        {

        }
    }
}
