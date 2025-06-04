using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�̒ʏ���
    /// </summary>
    public class PlayerIdleState : IState
    {
        Player _player;     // �v���C���[�N���X
        float _elapsedTime; // �o�ߎ���
        float _toOtherDuration;

        public PlayerIdleState(Player player)
        {
            _player = player;
            _toOtherDuration = PlayerData.Data.IdleToOtherDuration;
        }

        public void Enter()
        {
            _player.Animation.ResetAll();
            Debug.Log("�ҋ@");
        }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;

            // === ��ԑJ�� ===
            // Animator�Ɠ��������𓯊������邽�ߑҋ@
            if (_elapsedTime > _toOtherDuration)
            {
                // �ړ�
                if (_player.Action.Player.Move.ReadValue<Vector2>() != Vector2.zero)
                    _player.StateMachine.TransitionTo(_player.StateMachine.MoveState);
                // �U��
                else if (_player.Action.Player.Attack.IsPressed())
                    _player.StateMachine.TransitionTo(_player.StateMachine.Attack1State);
            }
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapsedTime = 0.0f;
        }
    }
}
