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
        float _rotSpeed;
        float _hitStartRatio;
        float _chainTime;
        float _afterImageEndRatio;

        public PlayerHeavyAttackState(Player player)
        {
            _player = player;
            _rotSpeed = PlayerData.Data.HeavyAttackRotSpeed;
            _hitStartRatio = WeaponData.Data.HeavyAttackHitStartRatio;
            _chainTime = PlayerData.Data.ChainTime;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.HeavyAttack();
            _targetRot = _player.transform.rotation;
        }

        public void Update()
        {
            // === 回転 ===
            // 地面に平行な視点方向の取得
            Vector3 viewV = Camera.main.transform.forward.normalized;
            viewV = Vector3.ProjectOnPlane(viewV, _player.NormalVector);
            // 回転値の取得
            _targetRot = Quaternion.LookRotation(viewV);

            // === 当たり判定 ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack) >= _hitStartRatio)
            {
                if (OBBHitChecker.IsCollideBoxOBB(_player.WeaponOBB, _player.Enemy.DamageableOBBs))
                {
                    _player.IncreaseHitNum();
                    Debug.Log("ヘビー攻撃ヒット");
                }
            }

            // === 残像の生成 ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);

            // === 状態遷移 ===
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashHeavyAttack))
            {
                _elapseTime += Time.deltaTime;
                // スペシャル攻撃
                if (_elapseTime <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(_player.StateMachine.SpecialAttackState);
                }
                // 待機
                else
                    _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            }
        }

        public void FixedUpdate()
        {
            // 取得した角度に制限を設けオブジェクトに反映
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, _rotSpeed);
        }

        public void Exit()
        {
            _elapseTime = 0.0f;
            OBBHitChecker.ResetHitInfo(_player.WeaponOBB, _player.Enemy.DamageableOBBs);
        }
    }
}
