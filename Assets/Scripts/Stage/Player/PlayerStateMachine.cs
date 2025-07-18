namespace Stage.Players
{
    public enum PlayerState
    {
        Idle,
        Move,
        Guard,
        Parry,
        Blocked,
        Impacted,
        LightAttack,
        HeavyAttack,
        SpecialAttack,
    }

    /// <summary>
    /// プレイヤーのステートを管理
    /// </summary>
    public class PlayerStateMachine : StateMachine<PlayerState>
    {
        public PlayerStateMachine(Player player) : base(10)
        {
            _states.Add(PlayerState.Idle, new PlayerIdleState(player));
            _states.Add(PlayerState.Move, new PlayerMoveState(player));
            _states.Add(PlayerState.Guard, new PlayerGuardState(player));
            _states.Add(PlayerState.Parry, new PlayerParryState(player));
            _states.Add(PlayerState.Blocked, new PlayerBlockedState(player));
            _states.Add(PlayerState.Impacted, new PlayerImpactedState(player));
            _states.Add(PlayerState.LightAttack, new PlayerLightAttackState(player));
            _states.Add(PlayerState.HeavyAttack, new PlayerHeavyAttackState(player));
            _states.Add(PlayerState.SpecialAttack, new PlayerSpecialAttackState(player));
        }

        public override void Initialize(PlayerState key)
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

        public override void TransitionTo(PlayerState key)
        {
            _currentState?.Exit();
            _currentState = _states[key];
            _currentState?.Enter();
        }
    }
}
