using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�U��3�i�ڏ��
    /// </summary>
    public class PlayerAttack3State : IState
    {
        Player _player;         // �v���C���[�N���X
        Quaternion _targetRot;  // ���_�����x�N�g��
        float _rotSpeed;
        float _hitStartRatio;

        public PlayerAttack3State(Player player)
        {
            _player = player;
            _rotSpeed = PlayerData.Data.Attack3RotSpeed;
            _hitStartRatio = WeaponData.Data.Attack3HitStartRatio;
        }

        public void Enter()
        {
            _player.Animation.Attack3();
            _player.HitCheck.ResetHitInfo();
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
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashAttack3) >= _hitStartRatio)
            {
                if (_player.HitCheck.IsCollideBoxOBB(_player.HitCheck.GreatSwordOBB, _player.HitCheck.EnemyOBB))
                {
                    Debug.Log("3��������");
                }
            }

            // === ��ԑJ�� ===
            // �ҋ@
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashAttack3))
                _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
        }

        public void FixedUpdate()
        {
            // �擾�����p�x�ɐ�����݂��I�u�W�F�N�g�ɔ��f
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, _rotSpeed);
        }

        public void Exit()
        {
    
        }
    }
}
