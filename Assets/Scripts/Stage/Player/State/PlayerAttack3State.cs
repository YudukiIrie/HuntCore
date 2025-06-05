using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤー攻撃3段目状態
    /// </summary>
    public class PlayerAttack3State : IState
    {
        Player _player;         // プレイヤークラス
        Quaternion _targetRot;  // 視点方向ベクトル
        float _rotSpeed;
        float _hitStartRatio;

        public PlayerAttack3State(Player player)
        {
            _player = player;
            _rotSpeed = PlayerData.Data.Attack3RotSpeed;
            _hitStartRatio = WeaponData.Data.Attack3HitStartRatio;
        }

        public void Enter()
        {
            _player.Animation.Attack3();
            _player.HitCheck.ResetHitInfo();
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
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashAttack3) >= _hitStartRatio)
            {
                if (_player.HitCheck.IsCollideBoxOBB(_player.HitCheck.GreatSwordOBB, _player.HitCheck.EnemyOBB))
                {
                    Debug.Log("3当たった");
                }
            }

            // === 状態遷移 ===
            // 待機
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashAttack3))
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
        }

        public void FixedUpdate()
        {
            // 取得した角度に制限を設けオブジェクトに反映
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, _rotSpeed);
        }

        public void Exit()
        {
    
        }
    }
}
