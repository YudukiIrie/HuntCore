using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
{
    public class PlayerAttack3State : IPlayerState
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
        }

        public void Update()
        {
            // === 状態遷移 ===
            // 待機
            if (_player.Animation.IsAttack3StateFinished())
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);

            // === 回転 ===
            // 地面に平行な視点方向の取得
            Vector3 viewV = Camera.main.transform.forward.normalized;
            viewV = Vector3.ProjectOnPlane(viewV, _player.NormalVector);
            // 回転値の取得
            _targetRot = Quaternion.LookRotation(viewV);
        }

        public void FixedUpdate()
        {
            // 取得した角度に制限を設けオブジェクトに反映
            float rotSpeed = PlayerData.Data.Attack3RotSpeed * Time.deltaTime;
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, rotSpeed);
        }

        public void Exit()
        {

        }
    }
}
