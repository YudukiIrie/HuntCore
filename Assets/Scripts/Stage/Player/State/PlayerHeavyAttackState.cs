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
        Quaternion _targetRot;  // 視点方向ベクトル
        float _elapseTime;      // コンボ間猶予経過時間

        // データキャッシュ用
        float _rotSpeed;
        float _hitStartRatio;
        float _hitEndRatio;
        float _chainTime;
        float _afterImageEndRatio;

        public PlayerHeavyAttackState(Player player)
        {
            _player = player;

            _rotSpeed      = PlayerData.Data.HeavyAttackRotSpeed;
            _hitStartRatio = WeaponData.Data.HeavyAttackHitStartRatio;
            _hitEndRatio   = WeaponData.Data.HeavyAttackHitEndRatio;
            _chainTime     = PlayerData.Data.ChainTime;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.HeavyAttack();
            _targetRot = _player.transform.rotation;
        }

        public void Update()
        {
            Rotate();

            DetectHit();

            SpawnAfterImage();

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapseTime = 0.0f;
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
        void DetectHit()
        {
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack) >= _hitStartRatio &&
                _player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack) <= _hitEndRatio)
            {
                if (OBBHitChecker.IsColliding(_player.WeaponOBB, _player.Enemy.EnemyColliders))
                    _player.IncreaseHitNum();
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
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashHeavyAttack))
            {
                _elapseTime += Time.deltaTime;
                // スペシャル攻撃
                if (_elapseTime <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(PlayerState.SpecialAttack);
                }
                // 待機
                else
                    _player.StateMachine.TransitionTo(PlayerState.Idle);
            }
        }
    }
}
