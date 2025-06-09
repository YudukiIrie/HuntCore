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
            // === 移動計算 ===
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            // 移動方向と速度を合成
            Transform cam = Camera.main.transform;
            _velocity = ((cam.forward * input.y) + (cam.right * input.x)).normalized;
            // 合成したベクトルを接触中の面に沿うベクトルに変換
            _velocity = Vector3.ProjectOnPlane(_velocity, _player.NormalVector).normalized * _moveSpeed;

            // === 回転計算 ===
            // ベクトルの大きさが0の角度は渡したくないため極小の際は計算しない
            if (input.magnitude > 0.001f)
                _targetRot = Quaternion.LookRotation(_velocity.normalized);

            // === 状態遷移 ===
            // 通常
            if (_player.Action.Player.Move.ReadValue<Vector2>() == Vector2.zero)
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            // ライト攻撃
            else if (_player.Action.Player.Attack.IsPressed())
                _player.StateMachine.TransitionTo(_player.StateMachine.LightAttackState);
        }

        public void FixedUpdate()
        {
            // === 取得した方向と速度を使い移動 ===
            // Y軸方向の移動を考慮しYのみ分離
            Vector3 vel = _velocity;
            vel.y = _player.Rigidbody.velocity.y;
            _player.Rigidbody.velocity = vel;

            // === 取得した角度をオブジェクトに反映 ===
            // 落ち着きをもって回転してほしいため制限を設ける
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, _rotSpeed);
        }

        public void Exit()
        {

        }
    }
}
