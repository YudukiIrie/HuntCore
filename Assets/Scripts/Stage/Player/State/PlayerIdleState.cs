using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーの通常状態
    /// </summary>
    public class PlayerIdleState : IState
    {
        Player _player;     // プレイヤークラス
        
        public PlayerIdleState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Idle();
        }

        public void Update()
        {
            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }

        /// <summary>
        /// 状態遷移
        /// </summary>
        void Transition()
        {
            // 移動
            if (_player.Action.Player.Move.ReadValue<Vector2>() != Vector2.zero)
                _player.StateMachine.TransitionTo(PlayerState.Move);
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
    }
}
