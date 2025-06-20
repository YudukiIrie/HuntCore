using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�K�[�h����
    /// </summary>
    public class PlayerBlockedState : IState
    {
        Player _player;     // �v���C���[�N���X
        float _elapsedTime; // �o�ߎ���
        bool _isCanceled = false;   // ��ԃL�����Z���̗L��
        float _toOtherDuration;

        public PlayerBlockedState(Player player)
        {
            _player = player;
            _toOtherDuration = PlayerData.Data.BlockedToOtherDuration;
        }

        public void Enter()
        {
            _player.Animation.Blocked();
        }

        public void Update()
        {
            // === ��ԑJ�� ===
            // �K�[�h�L�����Z��
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashBlocked) && !_isCanceled)
            {
                _player.Animation.CancelGuard();
                _isCanceled = true;
            }
            // �ҋ@
            if (_isCanceled)
            {
                _elapsedTime += Time.deltaTime;

                // �K�[�h�L�����Z���I�����AAnimator�Ɠ��������𓯊������邽�߂̑҂�
                if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashGuard) <= 0.0f &&
                    _elapsedTime > _toOtherDuration)
                    _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            }
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _isCanceled = false;
            _elapsedTime = 0.0f;
            _player.Animation.ResetSpeed();
        }
    }
}
