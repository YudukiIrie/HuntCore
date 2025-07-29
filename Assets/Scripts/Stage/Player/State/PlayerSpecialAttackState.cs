using Stage.HitDetection;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤースペシャル攻撃状態
    /// </summary>
    public class PlayerSpecialAttackState : IState
    {
        Player _player;         // プレイヤークラス

        // データキャッシュ用
        Vector2 _hitWindow;
        float _rotLimit;
        float _transRatio;
        float _afterImageEndRatio;

        public PlayerSpecialAttackState(Player player)
        {
            _player = player;

            _hitWindow = WeaponData.Data.SpecialAttackHitWindow;
            _rotLimit  = PlayerData.Data.AttackRotLimit;
            _transRatio = PlayerData.Data.SpecialAttackTransRatio;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            Rotate();

            _player.Animation.SpecialAttack();
        }

        public void Update()
        {
            HitDetect();

            SpawnAfterImage();

            Transition();
        }

        public void FixedUpdate()
        {
           
        }

        public void Exit()
        {
            HitChecker.ResetHitInfo(_player.Collider.Weapon, _player.Enemy.Collider.Colliders);
        }

        /// <summary>
        /// 回転
        /// </summary>
        void Rotate()
        {
            // === カメラに対する入力方向を取得(非入力時は無視) ===
            // 入力値の取得
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            if (input.magnitude < 0.001f) return;
            // カメラに対するベクトルへ変換
            Transform cam = Camera.main.transform;
            Vector3 direction = (cam.forward * input.y) + (cam.right * input.x);
            direction = Vector3.ProjectOnPlane(direction, _player.NormalVector).normalized;

            // === 入力に応じた角度をプレイヤーに反映 ===
            Transform transform = _player.transform;
            // 内積により角度(度数法)を取得
            float dot = Vector3.Dot(transform.forward, direction);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            // 外積により回転する向きを決定
            Vector3 cross = Vector3.Cross(transform.forward, direction);
            if (cross.y < 0.0f)
                angle = -angle;
            // 角度に制限を設けた後反映
            angle = Mathf.Clamp(angle, -_rotLimit, _rotLimit);
            Quaternion rot =
                Quaternion.AngleAxis(angle, transform.up) * _player.transform.rotation;
            _player.transform.rotation = rot;
        }

        /// <summary>
        /// 当たり判定
        /// </summary>
        void HitDetect()
        {
            var start = _hitWindow.x;
            var end   = _hitWindow.y;
            var progress = _player.Animation.CheckRatio(PlayerAnimation.HashSpecialAttack);
            if (progress >= start && progress <= end)
            {
                if (HitChecker.IsColliding(_player.Collider.Weapon, _player.Enemy.Collider.Colliders))
                    _player.Enemy.IncreaseHitNum();
            }
        }

        /// <summary>
        /// 残像の生成
        /// </summary>
        void SpawnAfterImage()
        {
            if (!_player.Animation.CompareRatio(
                PlayerAnimation.HashSpecialAttack, _afterImageEndRatio))
                _player.Spawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // === 終了後遷移 ===
            if (_player.Animation.CheckEnd(PlayerAnimation.HashSpecialAttack))
            {
                // 待機
                _player.StateMachine.TransitionTo(PlayerState.Idle);
            }
            // === 途中遷移 ===
            else if (_player.Animation.CompareRatio(
                PlayerAnimation.HashSpecialAttack, _transRatio))
            {
                // ガード
                if (_player.Action.Player.Guard.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.Guard);
                // 回避
                else if (_player.Action.Player.Roll.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.Roll);
            }
        }
    }
}
