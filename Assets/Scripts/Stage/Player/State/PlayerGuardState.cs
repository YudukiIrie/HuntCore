using Stage.HitCheck;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�K�[�h���
    /// </summary>
    public class PlayerGuardState : IState
    {
        Player _player;     // �v���C���[�N���X
        float _elapsedTime; // �o�ߎ���
        bool _isCanceled = false;   // ��ԃL�����Z���̗L��

        public PlayerGuardState(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            // �K�[�h��Ԃ�OBB�^�C�v�̐؂�ւ�
            _player.SetGuardState(true);
            _player.WeaponOBB.SetColliderRole(HitCollider.ColliderRole.Parry);

            _player.Animation.Guard();
        }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;

            SwitchColliderRole();

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapsedTime = 0.0f;
            _isCanceled = false;

            // �K�[�h��Ԃ�OBB�^�C�v�̐؂�ւ�
            _player.SetGuardState(false);
            _player.WeaponOBB.SetColliderRole(HitCollider.ColliderRole.Weapon);
        }

        /// <summary>
        /// ����R���C�_�[�̑����؂�ւ�
        /// </summary>
        void SwitchColliderRole()
        {
            if (_elapsedTime > PlayerData.Data.ParryableTime)
                _player.WeaponOBB.SetColliderRole(HitCollider.ColliderRole.Guard);
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // �K�[�h�L�����Z��
            if (_player.Action.Player.Guard.WasReleasedThisFrame() && !_isCanceled)
            {
                _player.Animation.CancelGuard(_player.Animation.CheckRatio(PlayerAnimation.HashGuardBegin));
                _isCanceled = true;
            }
            // �ҋ@
            if (_isCanceled)
            {
                // �t�Đ��̏I������ = 0.0f
                if (_player.Animation.CheckRatio(PlayerAnimation.HashGuardBegin) <= 0.0f)
                    _player.StateMachine.TransitionTo(PlayerState.Idle);
            }
        }
    }
}
