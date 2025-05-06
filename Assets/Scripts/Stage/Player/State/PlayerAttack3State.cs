using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
{
    public class PlayerAttack3State : IPlayerState
    {
        // プレイヤークラス
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
            // === 状態遷移 ===
            // 待機
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
