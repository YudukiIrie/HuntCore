using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーの通常状態
    /// </summary>
    public class PlayerIdleState : IState
    {
        Player _player;     // プレイヤークラス
        float _elapsedTime; // 経過時間
        float _toOtherDuration;

        public PlayerIdleState(Player player)
        {
            _player = player;
            _toOtherDuration = PlayerData.Data.IdleToOtherDuration;
        }

        public void Enter()
        {
            _player.Animation.Idle();
        }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;

            // === 状態遷移 ===
            // 移動
            if (_player.Action.Player.Move.ReadValue<Vector2>() != Vector2.zero)
                _player.StateMachine.TransitionTo(_player.StateMachine.MoveState);
            // ライト攻撃
            // Animatorと内部処理を同期させるため待機
            else if (_player.Action.Player.Attack.IsPressed())
            {
                if (_elapsedTime > _toOtherDuration)
                    _player.StateMachine.TransitionTo(_player.StateMachine.LightAttackState);
            }
            // ガード
            else if (_player.Action.Player.Guard.IsPressed())
                _player.StateMachine.TransitionTo(_player.StateMachine.GuardState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapsedTime = 0.0f;
        }
    }
}
