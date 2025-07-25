using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[������
    /// </summary>
    public class PlayerRollState : IState
    {
        Player _player;
        Vector3 _velocity;  // �ړ������Ƒ��x
        float _elapsedTime; // �o�ߎ���
        float _exitTime;    // �ޏo����

        // �f�[�^�L���b�V���p
        float _rollSpd;

        public PlayerRollState(Player player)
        {
            _player = player;

            _rollSpd = PlayerData.Data.RollSpd;
        }

        public void Enter()
        {
            Rotate();

            _player.Animation.Roll();
        }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;

            MoveUpdate();

            Transition();
        }

        public void FixedUpdate()
        {
            MoveFixedUpdate();
        }

        public void Exit()
        {
            _elapsedTime = 0.0f;
        }

        /// <summary>
        /// ��]
        /// </summary>
        void Rotate()
        {
            // ��]�����̎擾
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            Transform cam = Camera.main.transform;
            Vector3 direction = (cam.forward * input.y) + (cam.right * input.x);
            direction = Vector3.ProjectOnPlane(direction, _player.NormalVector).normalized;
            // ��]
            if (direction.magnitude > 0.001f)
                _player.transform.rotation = Quaternion.LookRotation(direction);
        }

        /// <summary>
        /// Update()�p�ړ�����
        /// </summary>
        void MoveUpdate()
        {
            _velocity = _player.transform.forward * _rollSpd;
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // Animator�̍X�V�Ƀ��O�����邽��
            // ���ۂ̃A�j���[�V�����I�����Ԃ��v����
            // ���������łɂ��邱�ƂŊm���ɃA�j���[�V�����I����҂�
            if (_elapsedTime >= _exitTime)
            {
                // �ʏ�
                if (_player.Animation.CheckEndAnim(PlayerAnimation.HashRoll))
                {
                    // �A�j���[�V�����I�����Ԃ̋L�^
                    _exitTime = _elapsedTime;
                    _player.StateMachine.TransitionTo(PlayerState.Idle);
                }
            }
        }

        /// <summary>
        /// FixedUpdate()�p�ړ�����
        /// </summary>
        void MoveFixedUpdate()
        {
            var vel = _velocity;
            vel.y = _player.Rigidbody.velocity.y;
            _player.Rigidbody.velocity = vel;
        }
    }
}