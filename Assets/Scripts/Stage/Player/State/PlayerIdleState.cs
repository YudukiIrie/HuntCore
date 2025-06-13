using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�̒ʏ���
    /// </summary>
    public class PlayerIdleState : IState
    {
        Player _player;     // �v���C���[�N���X
        float _elapsedTime; // �o�ߎ���
        float _toOtherDuration;

        public PlayerIdleState(Player player)
        {
            _player = player;
            _toOtherDuration = PlayerData.Data.IdleToOtherDuration;
        }

        public void Enter()
        {
            _player.Animation.Idle();
        }

        public void Update()
        {
            _elapsedTime += Time.deltaTime;

            // === ��ԑJ�� ===
            // �ړ�
            if (_player.Action.Player.Move.ReadValue<Vector2>() != Vector2.zero)
                _player.StateMachine.TransitionTo(_player.StateMachine.MoveState);
            // ���C�g�U��
            // Animator�Ɠ��������𓯊������邽�ߑҋ@
            else if (_player.Action.Player.Attack.IsPressed())
            {
                if (_elapsedTime > _toOtherDuration)
                    _player.StateMachine.TransitionTo(_player.StateMachine.LightAttackState);
            }
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {
            _elapsedTime = 0.0f;
        }
    }
}
