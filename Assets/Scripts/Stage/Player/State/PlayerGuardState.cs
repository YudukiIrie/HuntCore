using Stage.HitCheck;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーガード状態
    /// </summary>
    public class PlayerGuardState : IState
    {
        Player _player;     // プレイヤークラス
        float _elapsedTime; // 経過時間
        bool _isCanceled = false;   // 状態キャンセルの有無

        public PlayerGuardState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            // ガード状態とOBBタイプの切り替え
            _player.SetGuardState(true);
            _player.WeaponOBB.SetColliderRole(HitCollider.ColliderRole.Parry);

            _player.Animation.Guard();
        }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;

            SwitchColliderRole();

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapsedTime = 0.0f;
            _isCanceled = false;

            // ガード状態とOBBタイプの切り替え
            _player.SetGuardState(false);
            _player.WeaponOBB.SetColliderRole(HitCollider.ColliderRole.Weapon);
        }

        /// <summary>
        /// 武器コライダーの属性切り替え
        /// </summary>
        void SwitchColliderRole()
        {
            if (_elapsedTime > PlayerData.Data.ParryableTime)
                _player.WeaponOBB.SetColliderRole(HitCollider.ColliderRole.Guard);
        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // ガードキャンセル
            if (_player.Action.Player.Guard.WasReleasedThisFrame() && !_isCanceled)
            {
                _player.Animation.CancelGuard(_player.Animation.CheckRatio(PlayerAnimation.HashGuardBegin));
                _isCanceled = true;
            }
            // 待機
            if (_isCanceled)
            {
                // 逆再生の終了割合 = 0.0f
                if (_player.Animation.CheckRatio(PlayerAnimation.HashGuardBegin) <= 0.0f)
                    _player.StateMachine.TransitionTo(PlayerState.Idle);
            }
        }
    }
}
