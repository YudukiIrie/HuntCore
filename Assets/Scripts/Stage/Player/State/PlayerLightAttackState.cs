using Stage.HitCheck;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[���C�g�U�����
    /// </summary>
    public class PlayerLightAttackState : IState
    {
        Player _player;     // �v���C���[�N���X
        float _elapseTime;  // �R���{�ԗP�\�o�ߎ���

        // �f�[�^�L���b�V���p
        Vector2 _hitWindow;
        float _chainTime;
        float _afterImageEndRatio;


        public PlayerLightAttackState(Player player)
        {
            _player = player;

            _hitWindow = WeaponData.Data.LightAttackHitWindow;
            _chainTime = PlayerData.Data.ChainTime;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.LightAttack();
        }

        public void Update()
        {
            DetectHit();

            SpawnAferImage();

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
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashLightAttack))
            {
                _elapseTime += Time.deltaTime;
                // �w�r�[�U��
                if (_elapseTime <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(PlayerState.HeavyAttack);
                }
                // �ҋ@
                else
                    _player.StateMachine.TransitionTo(PlayerState.Idle);
            }
        }
    }
}
