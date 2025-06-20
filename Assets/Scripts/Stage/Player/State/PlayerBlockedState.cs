using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーガード後状態
    /// </summary>
    public class PlayerBlockedState : IState
    {
        Player _player;     // プレイヤークラス

        public PlayerBlockedState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Blocked();
        }

        public void Update()
        {
            // === 状態遷移 ===
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashBlocked))
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            
        }
    }
}
