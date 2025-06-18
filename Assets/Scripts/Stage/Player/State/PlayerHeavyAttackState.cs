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
        float _elapseTime;      // �R���{�ԗP�\�o�ߎ���
        float _rotSpeed;
        float _hitStartRatio;
        float _chainTime;
        float _afterImageEndRatio;

        public PlayerHeavyAttackState(Player player)
        {
            _player = player;
            _rotSpeed = PlayerData.Data.HeavyAttackRotSpeed;
            _hitStartRatio = WeaponData.Data.HeavyAttackHitStartRatio;
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
            // === ��]�v�Z ===
            // �n�ʂɕ��s�Ȏ��_�����̎擾
            Vector3 viewV = Camera.main.transform.forward.normalized;
            viewV = Vector3.ProjectOnPlane(viewV, _player.NormalVector);
            // ��]�l�̎擾
            _targetRot = Quaternion.LookRotation(viewV);

            // === �����]�� ===
            // ��]���x�̎擾
            float rotSpeed = _rotSpeed * Time.deltaTime;
            // ��]
            Quaternion rot = _player.transform.rotation;
            rot = Quaternion.RotateTowards(rot, _targetRot, rotSpeed);
            _player.transform.rotation = rot;

            // === �����蔻�� ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack) >= _hitStartRatio)
            {
                if (OBBHitChecker.IsCollideBoxOBB(_player.WeaponOBB, _player.Enemy.DamageableOBBs))
                    _player.IncreaseHitNum();
            }

            // === �c���̐��� ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashHeavyAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);

            // === ��ԑJ�� ===
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashHeavyAttack))
            {
                _elapseTime += Time.deltaTime;
                // �X�y�V�����U��
                if (_elapseTime <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(_player.StateMachine.SpecialAttackState);
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
            OBBHitChecker.ResetHitInfo(_player.WeaponOBB, _player.Enemy.DamageableOBBs);
        }
    }
}
