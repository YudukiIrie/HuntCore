using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーの通常状態
    /// </summary>
    public class PlayerIdleState : IState
    {
        Player _player;     // プレイヤークラス
        float _elapsedTime; // 経過時間
        float _toOtherDuration;

        public PlayerIdleState(Player player)
        {
            _player = player;
            _toOtherDuration = PlayerData.Data.IdleToOtherDuration;
        }

        public void Enter()
        {
            _player.Animation.ResetAll();
            Debug.Log("待機");
        }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;

            // === 状態遷移 ===
            // Animatorと内部処理を同期させるため待機
            if (_elapsedTime > _toOtherDuration)
            {
                // 移動
                if (_player.Action.Player.Move.ReadValue<Vector2>() != Vector2.zero)
                    _player.StateMachine.TransitionTo(_player.StateMachine.MoveState);
                // 攻撃
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
