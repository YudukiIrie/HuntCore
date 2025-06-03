namespace Stage.Enemies
{
    /// <summary>
    /// 敵のステートを管理
    /// </summary>
    public class EnemyStateMachine
    {
        // 現在のステートを保存
        IState _currentState;

        // 各ステート
        public EnemyIdleState IdleState { get; private set; }
        public EnemyRoarState RoarState { get; private set; }
        public EnemyAlertState AlertState { get; private set; }
        public EnemyChaseState ChaseState { get; private set; }

        public EnemyStateMachine(Enemy enemy)
        {
            IdleState = new EnemyIdleState(enemy);
            RoarState = new EnemyRoarState(enemy);
            AlertState = new EnemyAlertState(enemy);
            ChaseState = new EnemyChaseState(enemy);
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
