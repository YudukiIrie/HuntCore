using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Stage.Player
{
    public class PlayerAttack2State : IPlayerState
    {
        Player _player;         // �v���C���[�N���X
        float _elapseTime;      // �R���{�ԗP�\�o�ߎ���
        Quaternion _targetRot;  // ���_�����x�N�g��

        public PlayerAttack2State(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Attack2();
        }

        public void Update()
        {
            // === ��ԑJ�� ===
            // �ҋ@
            if (_player.Animation.IsAttack2StateFinished())
            {
                _elapseTime += Time.deltaTime;
                // �U��3
                if (_elapseTime <= PlayerData.Data.ChainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(_player.StateMachine.Attack3State);
                }
                // �ҋ@
                else
                    _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            }

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
            float rotSpeed = PlayerData.Data.Attack2RotSpeed * Time.deltaTime;
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, rotSpeed);
        }

        public void Exit()
        {
            _elapseTime = 0.0f;
        }
    }
}
