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
        float _hitStartRatio;
        float _chainTime;
        float _afterImageEndRatio;


        public PlayerLightAttackState(Player player)
        {
            _player = player;
            _hitStartRatio = WeaponData.Data.LightAttackHitStartRatio;
            _chainTime = PlayerData.Data.ChainTime;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.LightAttack();
        }

        public void Update()
        {
            // === �����蔻�� ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashLightAttack) >= _hitStartRatio)
            {
                if (_player.HitChecker.IsCollideBoxOBB(_player.HitChecker.GreatSwordOBB, _player.HitChecker.EnemyOBB))
                {
                    _player.IncreaseHitNum();
                    Debug.Log("���C�g�U���q�b�g");
                }
            }

            // === �c���̐��� ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashLightAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);

            // === ��ԑJ�� ===
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashLightAttack))
            {
                _elapseTime += Time.deltaTime;
                // �w�r�[�U��
                if (_elapseTime <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(_player.StateMachine.HeavyAttackState);
                }
                // �ҋ@
                else
                    _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            }
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapseTime = 0.0f;
            _player.HitChecker.GreatSwordOBB.ResetHitInfo();
            _player.HitChecker.EnemyOBB.ResetHitInfo();
        }
    }
}
