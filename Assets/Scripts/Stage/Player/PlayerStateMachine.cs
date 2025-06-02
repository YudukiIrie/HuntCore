using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// �v���C���[�̃X�e�[�g���Ǘ�
    /// </summary>
    public class PlayerStateMachine
    {
        // ���݂̃X�e�[�g��ۑ�
        IState _currentState;

        // �e�X�e�[�g
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerAttack1State Attack1State { get; private set; }
        public PlayerAttack2State Attack2State { get; private set; }
        public PlayerAttack3State Attack3State { get; private set; }
        public PlayerImpactedState ImpactedState { get; private set; }

        public PlayerStateMachine(Player player)
        {
            IdleState = new PlayerIdleState(player);
            MoveState = new PlayerMoveState(player);
            Attack1State = new PlayerAttack1State(player);
            Attack2State = new PlayerAttack2State(player);
            Attack3State = new PlayerAttack3State(player);
            ImpactedState = new PlayerImpactedState(player);
        }

        /// <summary>
        /// �X�e�[�g�̏�����
        /// </summary>
        /// <param name="state">�����X�e�[�g</param>
        public void Initialize(IState state)
        {
            _currentState = state;
            _currentState?.Enter();
        }

        /// <summary>
        /// ���X�e�[�g��Update()�����s
        /// </summary>
        public void Update()
        {
            _currentState?.Update();
        }

        /// <summary>
        /// ���X�e�[�g��FixedUpdate()�����s
        /// </summary>
        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }

        /// <summary>
        /// �X�e�[�g�̍X�V
        /// </summary>
        /// <param name="nextState">���̃X�e�[�g</param>
        public void TransitionTo(IState nextState)
        {
            _currentState?.Exit();
            _currentState = nextState;
            _currentState?.Enter();
        }
    }
}
