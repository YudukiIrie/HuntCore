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
        float _chainDuration;   // �R���{�ԗP�\�o�ߎ���

        // �f�[�^�L���b�V���p
        Vector2 _hitWindow;
        float _rotSpeed;
        float _chainTime;
        float _afterImageEndRatio;

        public PlayerHeavyAttackState(Player player)
        {
            _player = player;

            _hitWindow = WeaponData.Data.HeavyAttackHitWindow;
            _rotSpeed  = PlayerData.Data.HeavyAttackRotSpeed;
            _chainTime = PlayerData.Data.ChainTime;
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
            _chainDuration = 0.0f;
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
            var start = _hitWindow.x;
            var end   = _hitWindow.y;
            var progress = _player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack);
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
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            if (_player.Animation.CheckEndAnim(PlayerAnimation.HashHeavyAttack))
            {
                _chainDuration += Time.deltaTime;
                // �X�y�V�����U��
                if (_chainDuration <= _chainTime)
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
