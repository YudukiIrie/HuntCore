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
        float _rotSpeed;
        float _hitStartRatio;
        float _afterImageEndRatio;

        public PlayerSpecialAttackState(Player player)
        {
            _player = player;
            _rotSpeed = PlayerData.Data.SpecialAttackRotSpeed;
            _hitStartRatio = WeaponData.Data.SpecialAttackHitStartRatio;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.SpecialAttack();
            _targetRot = _player.transform.rotation;
        }

        public void Update()
        {
            // === 回転計算 ===
            // 地面に平行な視点方向の取得
            Vector3 viewV = Camera.main.transform.forward.normalized;
            viewV = Vector3.ProjectOnPlane(viewV, _player.NormalVector);
            // 回転値の取得
            _targetRot = Quaternion.LookRotation(viewV);

            // === 方向転換 ===
            // 回転速度の取得
            float rotSpeed = _rotSpeed * Time.deltaTime;
            // 回転
            Quaternion rot = _player.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _player.transform.rotation = rot;

            // === 当たり判定 ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashSpecialAttack) >= _hitStartRatio)
            {
                if (OBBHitChecker.IsCollideBoxOBB(_player.WeaponOBB, _player.Enemy.DamageableOBBs))
                    _player.IncreaseHitNum();
            }

            // === 残像の生成 ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashSpecialAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);

            // === 状態遷移 ===
            // 待機
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashSpecialAttack))
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
        }

        public void FixedUpdate()
        {
           
        }

        public void Exit()
        {
            OBBHitChecker.ResetHitInfo(_player.WeaponOBB, _player.Enemy.DamageableOBBs);
        }
    }
}
