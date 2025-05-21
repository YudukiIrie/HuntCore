using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
{
    /// <summary>
    /// プレイヤー移動状態
    /// </summary>
    public class PlayerMoveState : IPlayerState
    {
        // プレイヤークラス
        Player _player;

        // 移動方向と速度
        Vector3 _velocity;

        // 向くべき角度
        Quaternion _targetRot;

        public PlayerMoveState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Move();
            _targetRot = _player.transform.rotation;
        }

        public void Update()
        {
            // == 移動計算 ==
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            // ===== 抜刀状態のみになったため不要 =====
            //// 歩きと小走りの区別
            //float currentSpeed;
            //if (input.magnitude <= PlayerData.Data.MagnitudeBorder)
            //    currentSpeed = PlayerData.Data.WalkSpeed;
            //else
            //    currentSpeed = PlayerData.Data.JogSpeed;
            //// 走っているかどうかの区別
            //currentSpeed = _player.Action.Player.Run.IsPressed() ? PlayerData.Data.RunSpeed : currentSpeed;
            // ========================================

            // 移動方向と速度を合成
            Transform cam = Camera.main.transform;
            _velocity = ((cam.forward * input.y) + (cam.right * input.x)).normalized;
            // 合成したベクトルを接触中の面に沿うベクトルに変換
            _velocity = Vector3.ProjectOnPlane(_velocity, _player.NormalVector).normalized * PlayerData.Data.DrawnMoveSpeed;

            // == 回転計算 ==
            // ベクトルの大きさが0の角度は渡したくないため極小の際は計算しない
            if (input.magnitude > 0.001f)
                _targetRot = Quaternion.LookRotation(_velocity.normalized);

            // ===== 抜刀状態のみになったため不要 =====
            // == Animatorに移動速度を反映 ==
            // BlendTreeでアニメーションを管理するためRunSpeedを1とした割合を反映
            //_player.Animator.SetFloat("Speed", currentSpeed / PlayerData.Data.RunSpeed);
            // ========================================

            // == 状態遷移 ==
            // 通常
            if (_player.Action.Player.Move.ReadValue<Vector2>() == Vector2.zero)
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            // 攻撃
            else if (_player.Action.Player.Attack.IsPressed())
                _player.StateMachine.TransitionTo(_player.StateMachine.Attack1State);
        }

        public void FixedUpdate()
        {
            // == 取得した方向と速度を使い移動 ==
            // Y軸方向の移動を考慮しYのみ分離
            Vector3 vel = _velocity;
            vel.y = _player.Rigidbody.velocity.y;
            _player.Rigidbody.velocity = vel;

            // == 取得した角度をオブジェクトに反映 ==
            // 落ち着きをもって回転してほしいため制限を設ける
            float rotSpeed = PlayerData.Data.DrawnRotSpeed * Time.deltaTime;
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, rotSpeed);
        }

        public void Exit()
        {

        }
    }
}
