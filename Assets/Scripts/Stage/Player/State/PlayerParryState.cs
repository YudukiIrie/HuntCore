using Stage.Enemies;
using Stage.HitCheck;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�p���B���
    /// </summary>
    public class PlayerParryState : IState
    {
        Player _player;
        Vector3 _velocity;      // �ړ������Ƒ��x

        // �f�[�^�L���b�V���p
        float _moveSpeed;
        float _rotSpeed;
        Vector2 _moveWindow;
        Vector2 _hitWindow;

        public PlayerParryState(Player player)
        {
            _player = player;

            _moveSpeed  = PlayerData.Data.ParryMoveSpd;
            _rotSpeed   = PlayerData.Data.ParryRotSpd;
            _moveWindow = PlayerData.Data.ParryMoveWindow;
            _hitWindow  = WeaponData.Data.ParryHitWindow;
        }

        public void Enter()
        {
            _player.Animation.Parry();
        }

        public void Update()
        {
            MoveUpdate();

            Rotate();

            DetectHit();

            Transition();
        }

        public void FixedUpdate()
        {
            MoveFixedUpdate();
        }

        public void Exit()
        {
            OBBHitChecker.ResetHitInfo(_player.WeaponOBB, _player.Enemy.EnemyColliders);
        }

        /// <summary>
        /// Update()�p�ړ�����
        /// </summary>
        void MoveUpdate()
        {
            _velocity = _player.transform.forward * _moveSpeed;
        }

        /// <summary>
        /// ��]
        /// </summary>
        void Rotate()
        {
            // �J�������猩���Ƃ��̐���������擾
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            Transform cam = Camera.main.transform;
            Vector3 direction = ((cam.forward * input.y) + (cam.right * input.x)).normalized;
            direction = Vector3.ProjectOnPlane(direction, _player.NormalVector);
            //������݂���]
            Quaternion targetRot = _player.transform.rotation;
            if (direction.magnitude > 0.001f)
                targetRot = Quaternion.LookRotation(direction);
            float rotSpeed = _rotSpeed * Time.deltaTime;
            if (IsInMoveWindow())
                _player.transform.rotation =
                Quaternion.RotateTowards(_player.transform.rotation, targetRot, rotSpeed);
        }

        /// <summary>
        /// �����蔻��
        /// </summary>
        void DetectHit()
        {
            float progress = _player.Animation.CheckAnimRatio(PlayerAnimation.HashParry);
            float start = _hitWindow.x;
            float end = _hitWindow.y;
            if (progress >= start && progress <= end)
            {
                if (OBBHitChecker.IsColliding(_player.WeaponOBB, _player.Enemy.EnemyColliders))
                {
                    _player.Enemy.TakeImpact(EnemyState.Down);
                    _player.Enemy.IncreaseHitNum();
                }
            }
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // �ҋ@
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashParry))
                _player.StateMachine.TransitionTo(PlayerState.Idle);
        }

        /// <summary>
        /// FixedUpdate()�p�ړ�����
        /// </summary>
        void MoveFixedUpdate()
        {
            if (IsInMoveWindow())
                _player.Rigidbody.velocity = _velocity;
        }

        /// <summary>
        /// �A�j���[�V�����̍Đ�������
        /// �ړ��\��Ԃ��ǂ�����ԋp
        /// </summary>
        /// <returns>true:�ړ��\, false:�ړ��s��</returns>
        bool IsInMoveWindow()
        {
            float start = _moveWindow.x;
            float end = _moveWindow.y;
            float progress = _player.Animation.CheckAnimRatio(PlayerAnimation.HashParry);

            return progress >= start && progress <= end;
        }
    }
}