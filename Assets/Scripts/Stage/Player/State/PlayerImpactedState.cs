using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーが衝撃を受けた状態
    /// </summary>
    public class PlayerImpactedState : IState
    {
        Player _player; // プレイヤークラス

        public PlayerImpactedState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Impacted();
        }

        public void Update()
        {
            // === 状態遷移 ===
            // 待機
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashImpacted))
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
