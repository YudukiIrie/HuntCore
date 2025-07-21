using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�K�[�h����
    /// </summary>
    public class PlayerBlockedState : IState
    {
        Player _player;     // �v���C���[�N���X

        // �̂�����֘A
        Vector3 _velocity;  // �ړ������Ƒ��x
        Vector3 _oldPos;    // �O�t���[���̍��W
        float _moveDist;    // �̂����莞���ړ�����

        // �A�j���[�V�����֘A
        float _elapsedTime; // �o�ߎ���
        bool _isCanceled = false;   // ��ԃL�����Z���̗L��

        float _recoilSpeed;
        float _recoilDist;
        float _toOtherDuration;

        public PlayerBlockedState(Player player)
        {
            _player = player;
            _recoilSpeed = PlayerData.Data.RecoilSpeed;
            _recoilDist  = PlayerData.Data.RecoilDistance;
            _toOtherDuration = PlayerData.Data.BlockedToOtherDuration;
        }

        public void Enter()
        {
            _oldPos = _player.transform.position;
            _player.Animation.Blocked();
        }

        public void Update()
        {
            // === �̂�����v�Z ===
            // �v���C���[�̔w�����̃x�N�g�����擾
            _velocity = (_player.transform.forward * -1.0f) * _recoilSpeed;
            // �̂����莞�ړ������̎擾
            float dist = Vector3.Distance(_oldPos, _player.transform.position);
            _moveDist += dist;
            _oldPos = _player.transform.position;

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
                if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashGuardBegin) <= 0.0f &&
                    _elapsedTime > _toOtherDuration)
                    _player.StateMachine.TransitionTo(PlayerState.Idle);
            }
        }

        public void FixedUpdate()
        {
            // === �̂����� ===
            // ���ړ����������ɂȂ�܂ł̂�����
            if (_moveDist < _recoilDist)
            {
                Vector3 vel = _velocity;
                vel.y = _player.Rigidbody.velocity.y;
                _player.Rigidbody.velocity = vel;
            }
        }

        public void Exit()
        {
            _moveDist = 0.0f;
            _isCanceled = false;
            _elapsedTime = 0.0f;
        }
    }
}
