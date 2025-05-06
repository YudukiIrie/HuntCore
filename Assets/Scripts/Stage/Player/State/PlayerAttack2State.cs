using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Stage.Player
{
    public class PlayerAttack2State : IPlayerState
    {
        // �v���C���[�N���X
        Player _player;

        // �R���{�ԗP�\�o�ߎ���
        float _elapseTime;
        
        public PlayerAttack2State(Player player)
        {
            _player = player;
        }

        public void Enter()
        {
            _player.Animation.Attack2();
        }

        public void Update()
        {
            // === ��ԑJ�� ===
            // �ҋ@
            if (_player.Animation.IsAttack2StateFinished())
            {
                _elapseTime += Time.deltaTime;
                // �U��3
                if (_elapseTime <= PlayerData.Data.ChainTime)
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

        }

        public void Exit()
        {
            _elapseTime = 0.0f;
        }
    }
}
