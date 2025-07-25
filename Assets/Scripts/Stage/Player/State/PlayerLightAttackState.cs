using Stage.HitCheck;
using UnityEngine;
using UnityEngine.UIElements;

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
        bool _isAnimFinished = false;   // アニメーション終了フラグ

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
            if (!_isAnimFinished)
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
            _isAnimFinished = false;
            OBBHitChecker.ResetHitInfo(_player.WeaponOBB, _player.Enemy.EnemyColliders);
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
            var progress = _player.Animation.CheckAnimRatio(PlayerAnimation.HashLightAttack);
            if (progress >= start && progress <= end)
            {
                if (OBBHitChecker.IsColliding(_player.WeaponOBB, _player.Enemy.EnemyColliders))
                    _player.Enemy.IncreaseHitNum();
            }
        }

        /// <summary>
        /// 残像の生成
        /// </summary>
        void SpawnAferImage()
        {
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashLightAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // === 終了後遷移 ===
            // _elapsedTimeと_exitTimeの関係はPlayerRollStateを参照
            if (_elapsedTime >= _exitTime)
            {
                if (_player.Animation.CheckEndAnim(PlayerAnimation.HashLightAttack))
                {
                    _isAnimFinished = true;
                    // アニメーション終了時間を記録
                    _exitTime = _elapsedTime;

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
            else if(_player.Animation.CompareAnimRatio(
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
