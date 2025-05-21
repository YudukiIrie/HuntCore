using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Player
{
    /// <summary>
    /// �v���C���[�U��1�i�ڏ��
    /// </summary>
    public class PlayerAttack1State : IState
    {
        // �v���C���[�N���X
        Player _player;

        // �R���{�ԗP�\�o�ߎ���
        float _elapseTime;

        public PlayerAttack1State(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Attack1();
            _player.HitCheck.ResetHitEnemies();
        }

        public void Update()
        {
            // === �����蔻�� ===
            if (_player.Animation.CheckAnimRatio(PlayerAnimation.HashAttack1) >= WeaponData.Data.Attack1HitStartRatio)
            {
                if (_player.HitCheck.IsCollideBoxOBB(_player.HitCheck.GreatSwordOBB))
                {
                    _player.HitCheck.ChangeEnemyColor();
                    Debug.Log("1��������");
                }
            }

            // === ��ԑJ�� ===
            if (_player.Animation.IsAnimFinished(PlayerAnimation.HashAttack1))
            {
                _elapseTime += Time.deltaTime;
                // �U��2
                if (_elapseTime <= PlayerData.Data.ChainTime)
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
            _player.HitCheck.ResetEnemyColor();
        }
    }
}
