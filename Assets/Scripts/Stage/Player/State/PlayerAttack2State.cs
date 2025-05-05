using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Stage.Player
{
    public class PlayerAttack2State : IPlayerState
    {
        // �v���C���[�N���X
        Player _player;

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
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
