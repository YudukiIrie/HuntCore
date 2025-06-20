using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーガード後状態
    /// </summary>
    public class PlayerBlockedState : IState
    {
        Player _player;     // プレイヤークラス
        float _elapsedTime; // 経過時間
        bool _isCanceled = false;   // 状態キャンセルの有無
        float _toOtherDuration;

        public PlayerBlockedState(Player player)
        {
            _player = player;
            _toOtherDuration = PlayerData.Data.BlockedToOtherDuration;
        }

        public void Enter()
        {
            _player.Animation.Blocked();
        }

        public void Update()
        {
            // === 状態遷移 ===
            // ガードキャンセル
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashBlocked) && !_isCanceled)
            {
                _player.Animation.CancelGuard();
                _isCanceled = true;
            }
            // 待機
            if (_isCanceled)
            {
                _elapsedTime += Time.deltaTime;

                // ガードキャンセル終了かつ、Animatorと内部処理を同期させるための待ち
                if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashGuard) <= 0.0f &&
                    _elapsedTime > _toOtherDuration)
                    _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            }
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _isCanceled = false;
            _elapsedTime = 0.0f;
            _player.Animation.ResetSpeed();
        }
    }
}
