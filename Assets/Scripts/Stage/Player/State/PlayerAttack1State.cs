using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
{
    /// <summary>
    /// プレイヤー攻撃1段目状態
    /// </summary>
    public class PlayerAttack1State : IPlayerState
    {
        // プレイヤークラス
        Player _player;

        // コンボ間猶予経過時間
        float _elapseTime;

        public PlayerAttack1State(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Attack1();
            _player.HitCheck.ResetHitEnemies();
        }

        public void Update()
        {
            // == 状態遷移 ==
            if (_player.Animation.IsAttack1StateFinished())
            {
                _elapseTime += Time.deltaTime;
                // 攻撃2
                if (_elapseTime <= PlayerData.Data.ChainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(_player.StateMachine.Attack2State);
                }
                // 待機
                else
                    _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            }

            // == 当たり判定 ==
            if (_player.HitCheck.IsCollideBoxOBB(_player.HitCheck.GreatSwordOBB))
            {
                _player.HitCheck.ChangeEnemyColor();
                Debug.Log("当たった");
            }
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapseTime = 0.0f;
            _player.HitCheck.ResetEnemyColor();
        }
    }
}
