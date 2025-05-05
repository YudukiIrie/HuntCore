using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Stage.Player
{
    /// <summary>
    /// プレイヤーの通常状態
    /// </summary>
    public class PlayerIdleState : IPlayerState
    {
        // プレイヤークラス
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
            // === 状態遷移 ===
            // 移動
            if (_player.Action.Player.Move.ReadValue<Vector2>() != Vector2.zero)
                _player.StateMachine.TransitionTo(_player.StateMachine.MoveState);

            // 攻撃
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
