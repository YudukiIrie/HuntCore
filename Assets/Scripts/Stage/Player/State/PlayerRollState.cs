using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤー回避状態
    /// </summary>
    public class PlayerRollState : IState
    {
        Player _player;
        Vector3 _velocity;  // 移動方向と速度
        float _elapsedTime; // 経過時間
        float _exitTime;    // 退出時間

        // データキャッシュ用
        float _rollSpd;

        public PlayerRollState(Player player)
        {
            _player = player;

            _rollSpd = PlayerData.Data.RollSpd;
        }

        public void Enter()
        {
            _player.Animation.Roll();
        }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;

            MoveUpdate();

            Transition();
        }

        public void FixedUpdate()
        {
            MoveFixedUpdate();
        }

        public void Exit()
        {
            _elapsedTime = 0.0f;
        }

        /// <summary>
        /// Update()用移動処理
        /// </summary>
        void MoveUpdate()
        {
            _velocity = _player.transform.forward * _rollSpd;
        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // Animatorの更新にラグがあるため
            // 実際のアニメーション終了時間を計測し
            // 条件を強固にすることで確実にアニメーション終了を待つ
            if (_elapsedTime >= _exitTime)
            {
                // 通常
                if (_player.Animation.CheckEndAnim(PlayerAnimation.HashRoll))
                {
                    // アニメーション終了時間の記録
                    _exitTime = _elapsedTime;
                    _player.StateMachine.TransitionTo(PlayerState.Idle);
                }
            }
        }

        /// <summary>
        /// FixedUpdate()用移動処理
        /// </summary>
        void MoveFixedUpdate()
        {
            var vel = _velocity;
            vel.y = _player.Rigidbody.velocity.y;
            _player.Rigidbody.velocity = vel;
        }
    }
}