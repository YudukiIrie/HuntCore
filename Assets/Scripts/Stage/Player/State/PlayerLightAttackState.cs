using Stage.HitCheck;
using UnityEngine;
using UnityEngine.UIElements;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[���C�g�U�����
    /// </summary>
    public class PlayerLightAttackState : IState
    {
        Player _player;     // �v���C���[�N���X
        float _elapsedTime; // �o�ߎ���
        float _exitTime;    // �ޏo����
        float _chainDuration;   // �R���{�ԗP�\�o�ߎ���
        bool _isAnimFinished = false;   // �A�j���[�V�����I���t���O

        // �f�[�^�L���b�V���p
        Vector2 _hitWindow;
        float _chainTime;
        float _transRatio;
        float _afterImageEndRatio;


        public PlayerLightAttackState(Player player)
        {
            _player = player;

            _hitWindow = WeaponData.Data.LightAttackHitWindow;
            _chainTime = PlayerData.Data.ChainTime;
            _transRatio = PlayerData.Data.LightAttackTransRatio;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            Rotate();

            _player.Animation.LightAttack();
        }

        public void Update()
        {
            if (!_isAnimFinished)
                _elapsedTime += Time.deltaTime;

            DetectHit();

            SpawnAferImage();

            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapsedTime = 0.0f;
            _chainDuration = 0.0f;
            _isAnimFinished = false;
            OBBHitChecker.ResetHitInfo(_player.WeaponOBB, _player.Enemy.EnemyColliders);
        }

        /// <summary>
        /// ��]
        /// </summary>
        void Rotate()
        {
            // ��]�����擾
            Vector2 input = _player.Action.Player.Move.ReadValue<Vector2>();
            Transform cam = Camera.main.transform;
            Vector3 direction = (cam.forward * input.y) + (cam.right * input.x);
            direction = Vector3.ProjectOnPlane(direction, _player.NormalVector).normalized;
            // ��]
            if (direction.magnitude > 0.001f)
                _player.transform.rotation = Quaternion.LookRotation(direction);
        }

        /// <summary>
        /// �����蔻��
        /// </summary>
        void DetectHit()
        {
            var start = _hitWindow.x;
            var end   = _hitWindow.y;
            var progress = _player.Animation.CheckAnimRatio(PlayerAnimation.HashLightAttack);
            if (progress >= start && progress <= end)
            {
                if (OBBHitChecker.IsColliding(_player.WeaponOBB, _player.Enemy.EnemyColliders))
                    _player.Enemy.IncreaseHitNum();
            }
        }

        /// <summary>
        /// �c���̐���
        /// </summary>
        void SpawnAferImage()
        {
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashLightAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // === �I����J�� ===
            // _elapsedTime��_exitTime�̊֌W��PlayerRollState���Q��
            if (_elapsedTime >= _exitTime)
            {
                if (_player.Animation.CheckEndAnim(PlayerAnimation.HashLightAttack))
                {
                    _isAnimFinished = true;
                    // �A�j���[�V�����I�����Ԃ��L�^
                    _exitTime = _elapsedTime;

                    _chainDuration += Time.deltaTime;
                    // �w�r�[�U��
                    if (_chainDuration <= _chainTime)
                    {
                        if (_player.Action.Player.Attack.IsPressed())
                            _player.StateMachine.TransitionTo(PlayerState.HeavyAttack);
                    }
                    // �ҋ@
                    else
                        _player.StateMachine.TransitionTo(PlayerState.Idle);
                }
            }
            // === �r���J�� ===
            else if(_player.Animation.CompareAnimRatio(
                PlayerAnimation.HashLightAttack, _transRatio))
            {
                // �w�r�[�U��
                if (_player.Action.Player.Attack.IsPressed())
                    _player.StateMachine.TransitionTo(PlayerState.HeavyAttack);
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
