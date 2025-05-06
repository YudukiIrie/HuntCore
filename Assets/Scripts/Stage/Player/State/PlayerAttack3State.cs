using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
{
    public class PlayerAttack3State : IPlayerState
    {
        Player _player;         // �v���C���[�N���X
        Quaternion _targetRot;  // ���_�����x�N�g��

        public PlayerAttack3State(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Attack3();
        }

        public void Update()
        {
            // === ��ԑJ�� ===
            // �ҋ@
            if (_player.Animation.IsAttack3StateFinished())
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);

            // === ��] ===
            // �n�ʂɕ��s�Ȏ��_�����̎擾
            Vector3 viewV = Camera.main.transform.forward.normalized;
            viewV = Vector3.ProjectOnPlane(viewV, _player.NormalVector);
            // ��]�l�̎擾
            _targetRot = Quaternion.LookRotation(viewV);
        }

        public void FixedUpdate()
        {
            // �擾�����p�x�ɐ�����݂��I�u�W�F�N�g�ɔ��f
            float rotSpeed = PlayerData.Data.Attack3RotSpeed * Time.deltaTime;
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, rotSpeed);
        }

        public void Exit()
        {

        }
    }
}
