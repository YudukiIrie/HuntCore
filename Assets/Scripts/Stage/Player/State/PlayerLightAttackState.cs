using Stage.HitDetection;
using UnityEngine;

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
        bool _isExitTimeSet;    // �ޏo���Ԑݒ�t���O

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
            HitChecker.ResetHitInfo(_player.Collider.Weapon, _player.Enemy.Collider.Colliders);
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
            var progress = _player.Animation.CheckRatio(PlayerAnimation.HashLightAttack);
            if (progress >= start && progress <= end)
            {
                HitCollider weapon = _player.Collider.Weapon;
                if (HitChecker.IsColliding(weapon, _player.Enemy.Collider.Colliders))
                {
                    _player.FreezeFrame();
                    _player.Enemy.IncreaseHitNum();
                    _player.BloodFXSpawner.Spawn(weapon.Other.Position);
                }
            }
        }

        /// <summary>
        /// �c���̐���
        /// </summary>
        void SpawnAferImage()
        {
            if (!_player.Animation.CompareRatio(
                PlayerAnimation.HashLightAttack, _afterImageEndRatio))
                _player.AfterImageSpawner.Spawn(_player.Weapon.transform);
        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // === �I����J�� ===
            if (_player.Animation.CheckEnd(PlayerAnimation.HashLightAttack))
            {
                // _elapsedTime��_exitTime�̊֌W��PlayerRollState���Q��
                if (!_isExitTimeSet)
                {
                    _isExitTimeSet = true;
                    _exitTime = _elapsedTime;
                }

                if (_elapsedTime >= _exitTime)
                {
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
            else if (_player.Animation.CompareRatio(
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
