using TMPro;
using UnityEngine;
using Stage.HitCheck;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーガード状態
    /// </summary>
    public class PlayerGuardState : IState
    {
        Player _player;     // プレイヤークラス
        bool _isCanceled = false;   // 状態キャンセルの有無

        public PlayerGuardState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            // ガード状態とOBBタイプの切り替え
            _player.SetGuardState(true);
            _player.WeaponOBB.SetOBBType(OBB.OBBType.Guard);

            _player.Animation.Guard();
        }

        public void Update()
        {
            // === 状態遷移 ===
            // ガードキャンセル
            if (_player.Action.Player.Guard.WasReleasedThisFrame() && !_isCanceled)
            {
                _player.Animation.CancelGuard(_player.Animation.CheckAnimRatio(PlayerAnimation.HashGuard));
                _isCanceled = true;
            }
            // 待機
            if (_isCanceled)
            {
                // 逆再生の終了割合 = 0.0f
                if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashGuard) <= 0.0f)
                    _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            }
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _isCanceled = false;

            // ガード状態とOBBタイプの切り替え
            _player.SetGuardState(false);
            _player.WeaponOBB.SetOBBType(OBB.OBBType.Weapon);

            _player.Animation.ResetSpeed();
        }
    }
}
