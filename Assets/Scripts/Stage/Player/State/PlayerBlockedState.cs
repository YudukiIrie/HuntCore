using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーガード後状態
    /// </summary>
    public class PlayerBlockedState : IState
    {
        Player _player;     // プレイヤークラス

        // のけぞり関連
        Vector3 _velocity;  // 移動方向と速度
        Vector3 _oldPos;    // 前フレームの座標
        float _moveDist;    // のけぞり時総移動距離

        // アニメーション関連
        float _elapsedTime; // 経過時間
        bool _isCanceled = false;   // 状態キャンセルの有無

        float _recoilSpeed;
        float _recoilDist;
        float _toOtherDuration;

        public PlayerBlockedState(Player player)
        {
            _player = player;
            _recoilSpeed = PlayerData.Data.RecoilSpeed;
            _recoilDist  = PlayerData.Data.RecoilDistance;
            _toOtherDuration = PlayerData.Data.BlockedToOtherDuration;
        }

        public void Enter()
        {
            _oldPos = _player.transform.position;
            _player.Animation.Blocked();
        }

        public void Update()
        {
            // === のけぞり計算 ===
            // プレイヤーの背中側のベクトルを取得
            _velocity = (_player.transform.forward * -1.0f) * _recoilSpeed;
            // のけぞり時移動距離の取得
            float dist = Vector3.Distance(_oldPos, _player.transform.position);
            _moveDist += dist;
            _oldPos = _player.transform.position;

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
                if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashGuardBegin) <= 0.0f &&
                    _elapsedTime > _toOtherDuration)
                    _player.StateMachine.TransitionTo(PlayerState.Idle);
            }
        }

        public void FixedUpdate()
        {
            // === のけぞり ===
            // 総移動距離が一定になるまでのけぞる
            if (_moveDist < _recoilDist)
            {
                Vector3 vel = _velocity;
                vel.y = _player.Rigidbody.velocity.y;
                _player.Rigidbody.velocity = vel;
            }
        }

        public void Exit()
        {
            _moveDist = 0.0f;
            _isCanceled = false;
            _elapsedTime = 0.0f;
        }
    }
}
