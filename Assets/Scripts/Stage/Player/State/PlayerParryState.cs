using UnityEngine;

namespace Stage.Players
{
    public class PlayerParryState : IState
    {
        Player _player;
        Vector3 _velocity;      // �ړ������Ƒ��x

        // �f�[�^�L���b�V���p
        float _moveSpeed;
        float _rotSpeed;
        Vector2 _moveWindow;

        public PlayerParryState(Player player)
        {
            _player = player;

            _moveSpeed = PlayerData.Data.ParryMoveSpd;
            _rotSpeed = PlayerData.Data.ParryRotSpd;
            _moveWindow = PlayerData.Data.ParryMoveWindow;
        }

        public void Enter()
        {
            _player.Animation.Parry();
        }

        public void Update()
        {
            // === �ړ��v�Z ===
            _velocity = _player.transform.forward * _moveSpeed;

            // === ��] ===
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

            // === ��ԑJ�� ===
            // �ҋ@
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashParry))
                _player.StateMachine.TransitionTo(PlayerState.Idle);
        }

        public void FixedUpdate()
        {
            // === �ړ� ===
            if (IsInMoveWindow())
                _player.Rigidbody.velocity = _velocity;
        }

        public void Exit()
        {

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