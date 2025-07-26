using Stage.HitCheck;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーヘビー攻撃状態
    /// </summary>
    public class PlayerHeavyAttackState : IState
    {
        Player _player;         // プレイヤークラス
        float _chainDuration;   // コンボ間猶予経過時間

        // データキャッシュ用
        Vector2 _hitWindow;
        float _rotLimit;
        float _chainTime;
        float _transRatio;
        float _afterImageEndRatio;

        public PlayerHeavyAttackState(Player player)
        {
            _player = player;

            _hitWindow = WeaponData.Data.HeavyAttackHitWindow;
            _rotLimit  = PlayerData.Data.AttackRotLimit;
            _chainTime = PlayerData.Data.ChainTime;
            _transRatio = PlayerData.Data.HeavyAttackTransRatio;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            Rotate();

            _player.Animation.HeavyAttack();
        }

        public void Update()
        {
            DetectHit();

            SpawnAfterImage();

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _chainDuration = 0.0f;
            OBBHitChecker.ResetHitInfo(_player.WeaponOBB, _player.Enemy.EnemyColliders);
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
        void DetectHit()
        {
            var start = _hitWindow.x;
            var end   = _hitWindow.y;
            var progress = _player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack);
            if (progress >= start && progress <= end)
            {
                if (OBBHitChecker.IsColliding(_player.WeaponOBB, _player.Enemy.EnemyColliders))
                    _player.Enemy.IncreaseHitNum();
            }
        }

        /// <summary>
        /// 残像の生成
        /// </summary>
        void SpawnAfterImage()
        {
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // === 終了後遷移 ===
            if (_player.Animation.CheckEndAnim(PlayerAnimation.HashHeavyAttack))
            {
                _chainDuration += Time.deltaTime;
                // スペシャル攻撃
                if (_chainDuration <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(PlayerState.SpecialAttack);
                }
                // 待機
                else
                    _player.StateMachine.TransitionTo(PlayerState.Idle);
            }
            // === 途中遷移 ===
            else if (_player.Animation.CompareAnimRatio(
                PlayerAnimation.HashHeavyAttack, _transRatio))
            {
                // スペシャル攻撃
                if (_player.Action.Player.Attack.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.SpecialAttack);
                // ガード
                else if (_player.Action.Player.Guard.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.Guard);
                // 回避
                else if (_player.Action.Player.Roll.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.Roll);
            }
        }
    }
}
