using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Stage.Player
{
    /// <summary>
    /// �v���C���[�̒ʏ���
    /// </summary>
    public class PlayerIdleState : IPlayerState
    {
        // �v���C���[�N���X
        Player _player;

        public PlayerIdleState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.ResetAll();
        }

        public void Update()
        {
            // === ��ԑJ�� ===
            // �ړ�
            if (_player.Action.Player.Move.ReadValue<Vector2>() != Vector2.zero)
                _player.StateMachine.TransitionTo(_player.StateMachine.MoveState);

            // �U��
            if (_player.Action.Player.Attack.IsPressed())
                _player.StateMachine.TransitionTo(_player.StateMachine.Attack1State);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
