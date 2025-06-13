using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�X�y�V�����U�����
    /// </summary>
    public class PlayerSpecialAttackState : IState
    {
        Player _player;         // �v���C���[�N���X
        Quaternion _targetRot;  // ���_�����x�N�g��
        float _rotSpeed;
        float _hitStartRatio;
        float _afterImageEndRatio;

        public PlayerSpecialAttackState(Player player)
        {
            _player = player;
            _rotSpeed = PlayerData.Data.SpecialAttackRotSpeed;
            _hitStartRatio = WeaponData.Data.SpecialAttackHitStartRatio;
            _afterImageEndRatio = WeaponData.Data.AfterImageEndRatio;
        }

        public void Enter()
        {
            _player.Animation.SpecialAttack();
            _targetRot = _player.transform.rotation;
        }

        public void Update()
        {
            // === ��] ===
            // �n�ʂɕ��s�Ȏ��_�����̎擾
            Vector3 viewV = Camera.main.transform.forward.normalized;
            viewV = Vector3.ProjectOnPlane(viewV, _player.NormalVector);
            // ��]�l�̎擾
            _targetRot = Quaternion.LookRotation(viewV);

            // === �����蔻�� ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashSpecialAttack) >= _hitStartRatio)
            {
                if (_player.HitChecker.IsCollideBoxOBB(_player.HitChecker.GreatSwordOBB, _player.HitChecker.EnemyOBB))
                {
                    Debug.Log("�X�y�V�����U���q�b�g");
                }
            }

            // === �c���̐��� ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashSpecialAttack) <= _afterImageEndRatio)
                _player.Spawner.Spawn(_player.Weapon.transform);

            // === ��ԑJ�� ===
            // �ҋ@
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashSpecialAttack))
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
        }

        public void FixedUpdate()
        {
            // �擾�����p�x�ɐ�����݂��I�u�W�F�N�g�ɔ��f
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, _rotSpeed);
        }

        public void Exit()
        {
            _player.HitChecker.ResetHitInfo();
        }
    }
}
