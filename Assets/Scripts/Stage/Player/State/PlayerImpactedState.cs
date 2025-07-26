using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[���Ռ����󂯂����
    /// </summary>
    public class PlayerImpactedState : IState
    {
        Player _player; // �v���C���[�N���X

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
            // === ��ԑJ�� ===
            // �ҋ@
            if (_player.Animation.CheckEnd(PlayerAnimation.HashImpacted))
                _player.StateMachine.TransitionTo(PlayerState.Idle);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
