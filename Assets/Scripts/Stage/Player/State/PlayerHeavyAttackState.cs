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
        float _chainDuration;   // �R���{�ԗP�\�o�ߎ���

        // �f�[�^�L���b�V���p
        Vector2 _hitWindow;
        float _rotLimit;
        float _chainTime;
        float _transRatio;
        float _afterImageEndRatio;

        public PlayerHeavyAttackState(Player player)
        {
            _player = player;

            _hitWindow = WeaponData.Data.HeavyAttackHitWindow;
            _rotLimit  = PlayerData.Data.AttackRotLimit;
            _chainTime = PlayerData.Data.ChainTime;
            _transRatio = PlayerData.Data.HeavyAttackTransRatio;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            Rotate();

            _player.Animation.HeavyAttack();
        }

        public void Update()
        {
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
            // === �J�����ɑ΂�����͕������擾(����͎��͖���) ===
            // ���͒l�̎擾
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            if (input.magnitude < 0.001f) return;
            // �J�����ɑ΂���x�N�g���֕ϊ�
            Transform cam = Camera.main.transform;
            Vector3 direction = (cam.forward * input.y) + (cam.right * input.x);
            direction = Vector3.ProjectOnPlane(direction, _player.NormalVector).normalized;

            // === ���͂ɉ������p�x���v���C���[�ɔ��f ===
            Transform transform = _player.transform;
            // ���ςɂ��p�x(�x���@)���擾
            float dot = Vector3.Dot(transform.forward, direction);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            // �O�ςɂ���]�������������
            Vector3 cross = Vector3.Cross(transform.forward, direction);
            if (cross.y < 0.0f)
                angle = -angle;
            // �p�x�ɐ�����݂����㔽�f
            angle = Mathf.Clamp(angle, -_rotLimit, _rotLimit);
            Quaternion rot =
                Quaternion.AngleAxis(angle, transform.up) * _player.transform.rotation;
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
            // === �I����J�� ===
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
            // === �r���J�� ===
            else if (_player.Animation.CompareAnimRatio(
                PlayerAnimation.HashHeavyAttack, _transRatio))
            {
                // �X�y�V�����U��
                if (_player.Action.Player.Attack.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.SpecialAttack);
                // �K�[�h
                else if (_player.Action.Player.Guard.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.Guard);
                // ���
                else if (_player.Action.Player.Roll.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.Roll);
            }
        }
    }
}
