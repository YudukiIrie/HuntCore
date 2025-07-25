using Stage.HitCheck;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�X�y�V�����U�����
    /// </summary>
    public class PlayerSpecialAttackState : IState
    {
        Player _player;         // �v���C���[�N���X
        Quaternion _targetRot;  // ���_�����x�N�g��

        // �f�[�^�L���b�V���p
        Vector2 _hitWindow;
        float _rotSpeed;
        float _afterImageEndRatio;

        public PlayerSpecialAttackState(Player player)
        {
            _player = player;

            _hitWindow = WeaponData.Data.SpecialAttackHitWindow;
            _rotSpeed  = PlayerData.Data.SpecialAttackRotSpeed;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.SpecialAttack();
            _targetRot = _player.transform.rotation;
        }

        public void Update()
        {
            Rotate();

            HitDetect();

            SpawnAfterImage();

            Transition();
        }

        public void FixedUpdate()
        {
           
        }

        public void Exit()
        {
            OBBHitChecker.ResetHitInfo(_player.WeaponOBB, _player.Enemy.EnemyColliders);
        }

        /// <summary>
        /// ��]
        /// </summary>
        void Rotate()
        {
            // �n�ʂɕ��s�Ȏ��_�����̎擾
            Vector3 viewV = Camera.main.transform.forward.normalized;
            viewV = Vector3.ProjectOnPlane(viewV, _player.NormalVector);
            // ��]�l�̎擾
            _targetRot = Quaternion.LookRotation(viewV);
            // ��]���x�̎擾
            float rotSpeed = _rotSpeed * Time.deltaTime;
            // ��]
            Quaternion rot = _player.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _player.transform.rotation = rot;
        }

        /// <summary>
        /// �����蔻��
        /// </summary>
        void HitDetect()
        {
            var start = _hitWindow.x;
            var end   = _hitWindow.y;
            var progress = _player.Animation.CheckAnimRatio(PlayerAnimation.HashSpecialAttack);
            if (progress >= start && progress <= end)
            {
                if (OBBHitChecker.IsColliding(_player.WeaponOBB, _player.Enemy.EnemyColliders))
                    _player.Enemy.IncreaseHitNum();
            }
        }

        /// <summary>
        /// �c���̐���
        /// </summary>
        void SpawnAfterImage()
        {
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashSpecialAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // �ҋ@
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashSpecialAttack))
                _player.StateMachine.TransitionTo(PlayerState.Idle);
        }
    }
}
