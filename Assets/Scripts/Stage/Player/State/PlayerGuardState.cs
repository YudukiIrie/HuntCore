using TMPro;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�K�[�h���
    /// </summary>
    public class PlayerGuardState : IState
    {
        Player _player;     // �v���C���[�N���X
        bool _isCanceled;   // ��ԃL�����Z���̗L��

        public PlayerGuardState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Guard();
        }

        public void Update()
        {
            // === ��ԑJ�� ===
            // �K�[�h�L�����Z��
            if (_player.Action.Player.Guard.WasReleasedThisFrame() && !_isCanceled)
            {
                _player.Animation.CancelGuard(_player.Animation.CheckAnimRatio(PlayerAnimation.HashGuard));
                _isCanceled = true;
            }
            // �ҋ@
            if (_isCanceled)
            {
                if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashGuard) <= 0.0f)
                    _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            }
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _isCanceled = false;
            _player.Animation.ResetSpeed();
        }
    }
}
