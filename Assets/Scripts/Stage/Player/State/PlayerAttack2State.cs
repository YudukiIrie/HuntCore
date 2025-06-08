using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�U��2�i�ڏ��
    /// </summary>
    public class PlayerAttack2State : IState
    {
        Player _player;         // �v���C���[�N���X
        Quaternion _targetRot;  // ���_�����x�N�g��
        float _elapseTime;      // �R���{�ԗP�\�o�ߎ���
        float _rotSpeed;
        float _hitStartRatio;
        float _chainTime;

        public PlayerAttack2State(Player player)
        {
            _player = player;
            _rotSpeed = PlayerData.Data.Attack2RotSpeed;
            _hitStartRatio = WeaponData.Data.Attack2HitStartRatio;
            _chainTime = PlayerData.Data.ChainTime;
        }

        public void Enter()
        {
            _player.Animation.Attack2();
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
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashAttack2) >= _hitStartRatio)
            {
                if (_player.HitCheck.IsCollideBoxOBB(_player.HitCheck.GreatSwordOBB, _player.HitCheck.EnemyOBB))
                {
                    Debug.Log("�U��2�q�b�g");
                }
            }

            // === ��ԑJ�� ===
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashAttack2))
            {
                _elapseTime += Time.deltaTime;
                // �U��3
                if (_elapseTime <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(_player.StateMachine.Attack3State);
                }
                // �ҋ@
                else
                    _player.StateMachine.TransitionTo(_player.StateMachine.IdleState);
            }
        }

        public void FixedUpdate()
        {
            // �擾�����p�x�ɐ�����݂��I�u�W�F�N�g�ɔ��f
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRot, _rotSpeed);
        }

        public void Exit()
        {
            _elapseTime = 0.0f;
            _player.HitCheck.ResetHitInfo();
        }
    }
}
