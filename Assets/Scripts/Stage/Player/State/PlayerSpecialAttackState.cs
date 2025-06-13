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
            // === 回転 ===
            // 地面に平行な視点方向の取得
            Vector3 viewV = Camera.main.transform.forward.normalized;
            viewV = Vector3.ProjectOnPlane(viewV, _player.NormalVector);
            // 回転値の取得
            _targetRot = Quaternion.LookRotation(viewV);

            // === 当たり判定 ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashSpecialAttack) >= _hitStartRatio)
            {
                if (_player.HitChecker.IsCollideBoxOBB(_player.HitChecker.GreatSwordOBB, _player.HitChecker.EnemyOBB))
                {
                    Debug.Log("スペシャル攻撃ヒット");
                }
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
            // 取得した角度に制限を設けオブジェクトに反映
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, _rotSpeed);
        }

        public void Exit()
        {
            _player.HitChecker.ResetHitInfo();
        }
    }
}
