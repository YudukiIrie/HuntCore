using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Stage.Player
{
    public class PlayerAttack2State : IPlayerState
    {
        Player _player;         // プレイヤークラス
        Vector3 _hipInitPos;    // 腰パーツの移動を制限するための初期位置

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
            // === 状態遷移 ===
            // 待機
            if (_player.Animation.IsAttack2StateFinished())
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);

            // アニメーションによる移動の制限
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
