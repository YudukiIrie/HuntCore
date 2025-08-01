using Stage.Enemies;
using Stage.HitDetection;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーパリィ状態
    /// </summary>
    public class PlayerParryState : IState
    {
        Player _player;
        Vector3 _velocity;      // 移動方向と速度

        // データキャッシュ用
        float _moveSpeed;
        float _rotSpeed;
        float _afterImageEndRatio;
        Vector2 _moveWindow;
        Vector2 _hitWindow;

        public PlayerParryState(Player player)
        {
            _player = player;

            _moveSpeed  = PlayerData.Data.ParryMoveSpd;
            _rotSpeed   = PlayerData.Data.ParryRotSpd;
            _moveWindow = PlayerData.Data.ParryMoveWindow;
            _hitWindow  = WeaponData.Data.ParryHitWindow;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.Parry();
        }

        public void Update()
        {
            MoveUpdate();

            Rotate();

            DetectHit();

            SpawnAfterImage();

            Transition();
        }

        public void FixedUpdate()
        {
            MoveFixedUpdate();
        }

        public void Exit()
        {
            HitChecker.ResetHitInfo(_player.Collider.Weapon, _player.Enemy.Collider.Colliders);
        }

        /// <summary>
        /// Update()用移動処理
        /// </summary>
        void MoveUpdate()
        {
            _velocity = _player.transform.forward * _moveSpeed;
        }

        /// <summary>
        /// 回転
        /// </summary>
        void Rotate()
        {
            // カメラから見たときの旋回方向を取得
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            Transform cam = Camera.main.transform;
            Vector3 direction = ((cam.forward * input.y) + (cam.right * input.x)).normalized;
            direction = Vector3.ProjectOnPlane(direction, _player.NormalVector);
            //制限を設け回転
            Quaternion targetRot = _player.transform.rotation;
            if (direction.magnitude > 0.001f)
                targetRot = Quaternion.LookRotation(direction);
            float rotSpeed = _rotSpeed * Time.deltaTime;
            if (IsInMoveWindow())
                _player.transform.rotation =
                Quaternion.RotateTowards(_player.transform.rotation, targetRot, rotSpeed);
        }

        /// <summary>
        /// 当たり判定
        /// </summary>
        void DetectHit()
        {
            float progress = _player.Animation.CheckRatio(PlayerAnimation.HashParry);
            float start = _hitWindow.x;
            float end = _hitWindow.y;
            if (progress >= start && progress <= end)
            {
                HitCollider weapon = _player.Collider.Weapon;
                if (HitChecker.IsColliding(weapon, _player.Enemy.Collider.Colliders))
                {
                    _player.FreezeFrame();
                    _player.Enemy.IncreaseHitNum();
                    _player.Enemy.TakeImpact(EnemyState.Down);
                    _player.BloodFXSpawner.Spawn(weapon.Other.Position);
                }
            }
        }

        /// <summary>
        /// 残像の生成
        /// </summary>
        void SpawnAfterImage()
        {
            if (!_player.Animation.CompareRatio(
                PlayerAnimation.HashParry, _afterImageEndRatio))
                _player.AfterImageSpawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // 待機
            if (_player.Animation.CheckEnd(PlayerAnimation.HashParry))
                _player.StateMachine.TransitionTo(PlayerState.Idle);
        }

        /// <summary>
        /// FixedUpdate()用移動処理
        /// </summary>
        void MoveFixedUpdate()
        {
            if (IsInMoveWindow())
                _player.Rigidbody.velocity = _velocity;
        }

        /// <summary>
        /// アニメーションの再生割合が
        /// 移動可能区間かどうかを返却
        /// </summary>
        /// <returns>true:移動可能, false:移動不可</returns>
        bool IsInMoveWindow()
        {
            float start = _moveWindow.x;
            float end = _moveWindow.y;
            float progress = _player.Animation.CheckRatio(PlayerAnimation.HashParry);

            return progress >= start && progress <= end;
        }
    }
}