using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�U��1�i�ڏ��
    /// </summary>
    public class PlayerAttack1State : IState
    {
        Player _player;     // �v���C���[�N���X
        float _elapseTime;  // �R���{�ԗP�\�o�ߎ���
        float _hitStartRatio;
        float _chainTime;


        public PlayerAttack1State(Player player)
        {
            _player = player;
            _hitStartRatio = WeaponData.Data.Attack1HitStartRatio;
            _chainTime = PlayerData.Data.ChainTime;
        }

        public void Enter()
        {
            _player.Animation.Attack1();
        }

        public void Update()
        {
            // === �����蔻�� ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashAttack1) >= _hitStartRatio)
            {
                if (_player.HitCheck.IsCollideBoxOBB(_player.HitCheck.GreatSwordOBB, _player.HitCheck.EnemyOBB))
                {
                    Debug.Log("1��������");
                }
            }

            // === ��ԑJ�� ===
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashAttack1))
            {
                _elapseTime += Time.deltaTime;
                // �U��2
                if (_elapseTime <= _chainTime)
                {
                    if (_player.Action.Player.Attack.IsPressed())
                        _player.StateMachine.TransitionTo(_player.StateMachine.Attack2State);
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
            _player.HitCheck.ResetHitInfo();
        }
    }
}
