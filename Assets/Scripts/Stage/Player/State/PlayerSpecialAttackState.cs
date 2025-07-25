using Stage.HitCheck;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤースペシャル攻撃状態
    /// </summary>
    public class PlayerSpecialAttackState : IState
    {
        Player _player;         // プレイヤークラス
        Quaternion _targetRot;  // 視点方向ベクトル

        // データキャッシュ用
        Vector2 _hitWindow;
        float _rotSpeed;
        float _afterImageEndRatio;

        public PlayerSpecialAttackState(Player player)
        {
            _player = player;

            _hitWindow = WeaponData.Data.SpecialAttackHitWindow;
            _rotSpeed  = PlayerData.Data.SpecialAttackRotSpeed;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.SpecialAttack();
            _targetRot = _player.transform.rotation;
        }

        public void Update()
        {
            Rotate();

            HitDetect();

            SpawnAfterImage();

            Transition();
        }

        public void FixedUpdate()
        {
           
        }

        public void Exit()
        {
            OBBHitChecker.ResetHitInfo(_player.WeaponOBB, _player.Enemy.EnemyColliders);
        }

        /// <summary>
        /// 回転
        /// </summary>
        void Rotate()
        {
            // 地面に平行な視点方向の取得
            Vector3 viewV = Camera.main.transform.forward.normalized;
            viewV = Vector3.ProjectOnPlane(viewV, _player.NormalVector);
            // 回転値の取得
            _targetRot = Quaternion.LookRotation(viewV);
            // 回転速度の取得
            float rotSpeed = _rotSpeed * Time.deltaTime;
            // 回転
            Quaternion rot = _player.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _player.transform.rotation = rot;
        }

        /// <summary>
        /// 当たり判定
        /// </summary>
        void HitDetect()
        {
            var start = _hitWindow.x;
            var end   = _hitWindow.y;
            var progress = _player.Animation.CheckAnimRatio(PlayerAnimation.HashSpecialAttack);
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
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashSpecialAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // 待機
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashSpecialAttack))
                _player.StateMachine.TransitionTo(PlayerState.Idle);
        }
    }
}
