using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage.Players
{
    /// <summary>
    /// プレイヤーのステートを管理
    /// </summary>
    public class PlayerStateMachine
    {
        // 現在のステートを保存
        IState _currentState;

        // 各ステート
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
        /// ステートの初期化
        /// </summary>
        /// <param name="state">初期ステート</param>
        public void Initialize(IState state)
        {
            _currentState = state;
            _currentState?.Enter();
        }

        /// <summary>
        /// 現ステートのUpdate()を実行
        /// </summary>
        public void Update()
        {
            _currentState?.Update();
        }

        /// <summary>
        /// 現ステートのFixedUpdate()を実行
        /// </summary>
        public void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }

        /// <summary>
        /// ステートの更新
        /// </summary>
        /// <param name="nextState">次のステート</param>
        public void TransitionTo(IState nextState)
        {
            _currentState?.Exit();
            _currentState = nextState;
            _currentState?.Enter();
        }
    }
}
