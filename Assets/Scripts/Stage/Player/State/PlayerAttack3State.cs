using System.Collections;
using System.Collections.Generic;
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

        public PlayerAttack3State(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Attack3();
            _player.HitCheck.ResetHitEnemies();
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
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashAttack3) >= WeaponData.Data.Attack3HitStartRatio)
            {
                if (_player.HitCheck.IsCollideBoxOBB(_player.HitCheck.GreatSwordOBB))
                {
                    _player.HitCheck.ChangeEnemyColor();
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
            float rotSpeed = PlayerData.Data.Attack3RotSpeed * Time.deltaTime;
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, rotSpeed);
        }

        public void Exit()
        {
            _player.HitCheck.ResetEnemyColor();
        }
    }
}
