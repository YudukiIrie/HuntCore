using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Stage.Player
{
    public class PlayerAttack2State : IPlayerState
    {
        Player _player;         // �v���C���[�N���X
        Vector3 _hipInitPos;    // ���p�[�c�̈ړ��𐧌����邽�߂̏����ʒu

        public PlayerAttack2State(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Attack2();
            _hipInitPos = _player.Hip.transform.localPosition;
            Debug.Log(_hipInitPos);
        }

        public void Update()
        {
            // === ��ԑJ�� ===
            // �ҋ@
            if (_player.Animation.IsAttack2StateFinished())
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);

            // �A�j���[�V�����ɂ��ړ��̐���
            _player.Hip.transform.localPosition = _hipInitPos;
            Debug.Log(_player.Hip.transform.localPosition);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            
        }
    }
}
