using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤー移動状態
    /// </summary>
    public class PlayerMoveState : IState
    {
        Player _player;         // プレイヤークラス
        Vector3 _velocity;      // 移動方向と速度
        Quaternion _targetRot;  // 向くべき角度

        // データキャッシュ用
        float _moveSpeed;
        float _rotSpeed;

        public PlayerMoveState(Player player)
        {
            _player = player;

            _moveSpeed = PlayerData.Data.DrawnMoveSpeed;
            _rotSpeed = PlayerData.Data.DrawnRotSpeed;
        }

        public void Enter()
        {
            _player.Animation.Move();
            _targetRot = _player.transform.rotation;
        }

        public void Update()
        {
            MoveUpdate();

            Rotate();

            Transition();
        }

        public void FixedUpdate()
        {
            MoveFixedUpdate();
        }

        public void Exit()
        {

        }

        /// <summary>
        /// Update()用移動処理
        /// </summary>
        void MoveUpdate()
        {
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            // 移動方向と速度を合成
            Transform cam = Camera.main.transform;
            _velocity = ((cam.forward * input.y) + (cam.right * input.x)).normalized;
            // 合成したベクトルを接触中の面に沿うベクトルに変換
            _velocity = Vector3.ProjectOnPlane(_velocity, _player.NormalVector).normalized * _moveSpeed;
        }

        /// <summary>
        /// 回転
        /// </summary>
        void Rotate()
        {
            // ベクトルの大きさが0の角度は渡したくないため極小の際は計算しない
            if (_velocity.magnitude > 0.001f)
                _targetRot = Quaternion.LookRotation(_velocity.normalized);
            // 回転速度の取得
            float rotSpeed = _rotSpeed * Time.deltaTime;
            // 回転
            Quaternion rot = _player.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _player.transform.rotation = rot;
        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // 通常
            if (_player.Action.Player.Move.ReadValue<Vector2>() == Vector2.zero)
                _player.StateMachine.TransitionTo(PlayerState.Idle);
            // ライト攻撃
            else if (_player.Action.Player.Attack.IsPressed())
                _player.StateMachine.TransitionTo(PlayerState.LightAttack);
            // ガード
            else if (_player.Action.Player.Guard.IsPressed())
                _player.StateMachine.TransitionTo(PlayerState.Guard);
            // 回避
            else if (_player.Action.Player.Roll.IsPressed())
                _player.StateMachine.TransitionTo(PlayerState.Roll);
        }

        /// <summary>
        /// FixedUpdate()用移動処理
        /// </summary>
        void MoveFixedUpdate()
        {
            // Y軸方向の移動を考慮しYのみ分離
            Vector3 vel = _velocity;
            vel.y = _player.Rigidbody.velocity.y;
            _player.Rigidbody.velocity = vel;
        }
    }
}
