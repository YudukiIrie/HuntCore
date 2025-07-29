using Stage.HitDetection;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�X�y�V�����U�����
    /// </summary>
    public class PlayerSpecialAttackState : IState
    {
        Player _player;         // �v���C���[�N���X

        // �f�[�^�L���b�V���p
        Vector2 _hitWindow;
        float _rotLimit;
        float _transRatio;
        float _afterImageEndRatio;

        public PlayerSpecialAttackState(Player player)
        {
            _player = player;

            _hitWindow = WeaponData.Data.SpecialAttackHitWindow;
            _rotLimit  = PlayerData.Data.AttackRotLimit;
            _transRatio = PlayerData.Data.SpecialAttackTransRatio;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            Rotate();

            _player.Animation.SpecialAttack();
        }

        public void Update()
        {
            HitDetect();

            SpawnAfterImage();

            Transition();
        }

        public void FixedUpdate()
        {
           
        }

        public void Exit()
        {
            HitChecker.ResetHitInfo(_player.Collider.Weapon, _player.Enemy.Collider.Colliders);
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
        void HitDetect()
        {
            var start = _hitWindow.x;
            var end   = _hitWindow.y;
            var progress = _player.Animation.CheckRatio(PlayerAnimation.HashSpecialAttack);
            if (progress >= start && progress <= end)
            {
                if (HitChecker.IsColliding(_player.Collider.Weapon, _player.Enemy.Collider.Colliders))
                    _player.Enemy.IncreaseHitNum();
            }
        }

        /// <summary>
        /// �c���̐���
        /// </summary>
        void SpawnAfterImage()
        {
            if (!_player.Animation.CompareRatio(
                PlayerAnimation.HashSpecialAttack, _afterImageEndRatio))
                _player.Spawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // === �I����J�� ===
            if (_player.Animation.CheckEnd(PlayerAnimation.HashSpecialAttack))
            {
                // �ҋ@
                _player.StateMachine.TransitionTo(PlayerState.Idle);
            }
            // === �r���J�� ===
            else if (_player.Animation.CompareRatio(
                PlayerAnimation.HashSpecialAttack, _transRatio))
            {
                // �K�[�h
                if (_player.Action.Player.Guard.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.Guard);
                // ���
                else if (_player.Action.Player.Roll.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.Roll);
            }
        }
    }
}
