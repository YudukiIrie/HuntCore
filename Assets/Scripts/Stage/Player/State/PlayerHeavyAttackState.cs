using Stage.HitCheck;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�w�r�[�U�����
    /// </summary>
    public class PlayerHeavyAttackState : IState
    {
        Player _player;         // �v���C���[�N���X
        Quaternion _targetRot;  // ���_�����x�N�g��
        float _elapseTime;      // �R���{�ԗP�\�o�ߎ���

        // �f�[�^�L���b�V���p
        float _rotSpeed;
        float _hitStartRatio;
        float _hitEndRatio;
        float _chainTime;
        float _afterImageEndRatio;

        public PlayerHeavyAttackState(Player player)
        {
            _player = player;

            _rotSpeed      = PlayerData.Data.HeavyAttackRotSpeed;
            _hitStartRatio = WeaponData.Data.HeavyAttackHitStartRatio;
            _hitEndRatio   = WeaponData.Data.HeavyAttackHitEndRatio;
            _chainTime     = PlayerData.Data.ChainTime;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.HeavyAttack();
            _targetRot = _player.transform.rotation;
        }

        public void Update()
        {
            Rotate();

            DetectHit();

            SpawnAfterImage();

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapseTime = 0.0f;
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
        void DetectHit()
        {
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack) >= _hitStartRatio &&
                _player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack) <= _hitEndRatio)
            {
                if (OBBHitChecker.IsColliding(_player.WeaponOBB, _player.Enemy.EnemyColliders))
                    _player.IncreaseHitNum();
            }
        }

        /// <summary>
        /// �c���̐���
        /// </summary>
        void SpawnAfterImage()
        {
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashHeavyAttack))
            {
                _elapseTime += Time.deltaTime;
                // �X�y�V�����U��
                if (_elapseTime <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(PlayerState.SpecialAttack);
                }
                // �ҋ@
                else
                    _player.StateMachine.TransitionTo(PlayerState.Idle);
            }
        }
    }
}
