using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
{
    public class PlayerAttack3State : IPlayerState
    {
        // �v���C���[�N���X
        Player _player;

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
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
