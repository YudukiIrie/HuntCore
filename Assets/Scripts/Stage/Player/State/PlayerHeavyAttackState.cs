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
        float _chainDuration;   // コンボ間猶予経過時間

        // データキャッシュ用
        Vector2 _hitWindow;
        float _rotSpeed;
        float _chainTime;
        float _transRatio;
        float _afterImageEndRatio;

        public PlayerHeavyAttackState(Player player)
        {
            _player = player;

            _hitWindow = WeaponData.Data.HeavyAttackHitWindow;
            _rotSpeed  = PlayerData.Data.HeavyAttackRotSpeed;
            _chainTime = PlayerData.Data.ChainTime;
            _transRatio = PlayerData.Data.HeavyAttackTransRatio;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            Rotate();
            _player.Animation.HeavyAttack();
            _targetRot = _player.transform.rotation;
        }

        public void Update()
        {
            //Rotate();

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
            //// 地面に平行な視点方向の取得
            //Vector3 viewV = Camera.main.transform.forward.normalized;
            //viewV = Vector3.ProjectOnPlane(viewV, _player.NormalVector);
            //// 回転値の取得
            //_targetRot = Quaternion.LookRotation(viewV);
            //// 回転速度の取得
            //float rotSpeed = _rotSpeed * Time.deltaTime;
            //// 回転
            //Quaternion rot = _player.transform.rotation;
            //rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            //_player.transform.rotation = rot;

            // 回転方向取得
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            Transform cam = Camera.main.transform;
            Vector3 direction = (cam.forward * input.y) + (cam.right * input.x);
            direction = Vector3.ProjectOnPlane(direction, _player.NormalVector).normalized;
            Quaternion targetRot;
            if (direction.magnitude > 0.001f)
                targetRot = Quaternion.LookRotation(direction);
            // 回転
            float rotSpeed = _rotSpeed * Time.deltaTime;
            targetRot = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, rotSpeed);
            _player.transform.rotation = targetRot;
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
