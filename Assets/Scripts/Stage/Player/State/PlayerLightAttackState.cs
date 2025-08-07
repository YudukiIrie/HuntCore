using Stage.HitDetection;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーライト攻撃状態
    /// </summary>
    public class PlayerLightAttackState : IState
    {
        Player _player;     // プレイヤークラス
        float _elapsedTime; // 経過時間
        float _exitTime;    // 退出時間
        float _chainDuration;   // コンボ間猶予経過時間
        bool _isExitTimeSet;    // 退出時間設定フラグ

        // データキャッシュ用
        Vector2 _hitWindow;
        float _chainTime;
        float _transRatio;
        float _afterImageEndRatio;


        public PlayerLightAttackState(Player player)
        {
            _player = player;

            _hitWindow = WeaponData.Data.LightAttackHitWindow;
            _chainTime = PlayerData.Data.ChainTime;
            _transRatio = PlayerData.Data.LightAttackTransRatio;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            Rotate();

            _player.Animation.LightAttack();
        }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;

            DetectHit();

            SpawnAferImage();

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapsedTime = 0.0f;
            _chainDuration = 0.0f;
            HitChecker.ResetHitInfo(_player.Collider.Weapon, _player.Enemy.Collider.Colliders);
        }

        /// <summary>
        /// 回転
        /// </summary>
        void Rotate()
        {
            // 回転方向取得
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            Transform cam = Camera.main.transform;
            Vector3 direction = (cam.forward * input.y) + (cam.right * input.x);
            direction = Vector3.ProjectOnPlane(direction, _player.NormalVector).normalized;
            // 回転
            if (direction.magnitude > 0.001f)
                _player.transform.rotation = Quaternion.LookRotation(direction);
        }

        /// <summary>
        /// 当たり判定
        /// </summary>
        void DetectHit()
        {
            var start = _hitWindow.x;
            var end   = _hitWindow.y;
            var progress = _player.Animation.CheckRatio(PlayerAnimation.HashLightAttack);
            if (progress >= start && progress <= end)
            {
                HitCollider weapon = _player.Collider.Weapon;
                if (HitChecker.IsColliding(weapon, _player.Enemy.Collider.Colliders))
                {
                    _player.FreezeFrame();
                    _player.Enemy.IncreaseHitNum();
                    _player.BloodFXSpawner.Spawn(weapon.Other.Position);
                }
            }
        }

        /// <summary>
        /// 残像の生成
        /// </summary>
        void SpawnAferImage()
        {
            if (!_player.Animation.CompareRatio(
                PlayerAnimation.HashLightAttack, _afterImageEndRatio))
                _player.AfterImageSpawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // === 終了後遷移 ===
            if (_player.Animation.CheckEnd(PlayerAnimation.HashLightAttack))
            {
                // _elapsedTimeと_exitTimeの関係はPlayerRollStateを参照
                if (!_isExitTimeSet)
                {
                    _isExitTimeSet = true;
                    _exitTime = _elapsedTime;
                }

                if (_elapsedTime >= _exitTime)
                {
                    _chainDuration += Time.deltaTime;
                    // ヘビー攻撃
                    if (_chainDuration <= _chainTime)
                    {
                        if (_player.Action.Player.Attack.IsPressed())
                            _player.StateMachine.TransitionTo(PlayerState.HeavyAttack);
                    }
                    // 待機
                    else
                        _player.StateMachine.TransitionTo(PlayerState.Idle);
                }
            }
            // === 途中遷移 ===
            else if (_player.Animation.CompareRatio(
                PlayerAnimation.HashLightAttack, _transRatio))
            {
                // ヘビー攻撃
                if (_player.Action.Player.Attack.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.HeavyAttack);
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
