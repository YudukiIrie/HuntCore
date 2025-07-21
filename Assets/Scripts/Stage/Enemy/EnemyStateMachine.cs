namespace Stage.Enemies
{
    public enum EnemyState
    {
        Idle,
        Turn,
        Roar,
        Down,
        Alert,
        Chase,
        Attack,
        GetHit,
    }

    /// <summary>
    /// 敵のステートを管理
    /// </summary>
    public class EnemyStateMachine : StateMachine<EnemyState>
    {
        public EnemyStateMachine(Enemy enemy) : base(10)
        {
            _states.Add(EnemyState.Idle, new EnemyIdleState(enemy));
            _states.Add(EnemyState.Turn, new EnemyTurnState(enemy));
            _states.Add(EnemyState.Roar, new EnemyRoarState(enemy));
            _states.Add(EnemyState.Down, new EnemyDownState(enemy));
            _states.Add(EnemyState.Alert, new EnemyAlertState(enemy));
            _states.Add(EnemyState.Chase, new EnemyChaseState(enemy));
            _states.Add(EnemyState.Attack, new EnemyAttackState(enemy));
            _states.Add(EnemyState.GetHit, new EnemyGetHitState(enemy));
        }

        public override void Initialize(EnemyState key)
        {
            _currentState = _states[key];
            _currentState?.Enter();
        }

        public override void Update()
        {
            _currentState?.Update();
        }

        public override void FixedUpdate()
        {
            _currentState?.FixedUpdate();
        }

        public override void TransitionTo(EnemyState key)
        {
            _currentState?.Exit();
            _currentState = _states[key];
            _currentState?.Enter();
        }
    }
}
