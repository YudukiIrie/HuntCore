using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�K�[�h����
    /// </summary>
    public class PlayerBlockedState : IState
    {
        Player _player;     // �v���C���[�N���X

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
            // === ��ԑJ�� ===
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
