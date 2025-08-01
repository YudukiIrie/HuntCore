using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�̒ʏ���
    /// </summary>
    public class PlayerIdleState : IState
    {
        Player _player;     // �v���C���[�N���X
        
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
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // �ړ�
            if (_player.Action.Player.Move.ReadValue<Vector2>() != Vector2.zero)
                _player.StateMachine.TransitionTo(PlayerState.Move);
            // ���C�g�U��
            else if (_player.Action.Player.Attack.IsPressed())
                _player.StateMachine.TransitionTo(PlayerState.LightAttack);
            // �K�[�h
            else if (_player.Action.Player.Guard.IsPressed())
                _player.StateMachine.TransitionTo(PlayerState.Guard);
            // ���
            else if (_player.Action.Player.Roll.IsPressed())
                _player.StateMachine.TransitionTo(PlayerState.Roll);
        }
    }
}
