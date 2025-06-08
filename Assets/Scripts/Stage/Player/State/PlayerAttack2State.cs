using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤー攻撃2段目状態
    /// </summary>
    public class PlayerAttack2State : IState
    {
        Player _player;         // プレイヤークラス
        Quaternion _targetRot;  // 視点方向ベクトル
        float _elapseTime;      // コンボ間猶予経過時間
        float _rotSpeed;
        float _hitStartRatio;
        float _chainTime;

        public PlayerAttack2State(Player player)
        {
            _player = player;
            _rotSpeed = PlayerData.Data.Attack2RotSpeed;
            _hitStartRatio = WeaponData.Data.Attack2HitStartRatio;
            _chainTime = PlayerData.Data.ChainTime;
        }

        public void Enter()
        {
            _player.Animation.Attack2();
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
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashAttack2) >= _hitStartRatio)
            {
                if (_player.HitCheck.IsCollideBoxOBB(_player.HitCheck.GreatSwordOBB, _player.HitCheck.EnemyOBB))
                {
                    Debug.Log("攻撃2ヒット");
                }
            }

            // === 状態遷移 ===
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashAttack2))
            {
                _elapseTime += Time.deltaTime;
                // 攻撃3
                if (_elapseTime <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(_player.StateMachine.Attack3State);
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
            _player.HitCheck.ResetHitInfo();
        }
    }
}
