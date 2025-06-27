namespace Stage.Players
{
    //public enum PlayerState
    //{
    //    Idle,
    //    Move
    //}

    //public abstract class StateMachine<TContext, TState> where TState : Enum
    //{
    //    protected readonly Dictionary<TState, IState> states;

    //    protected StateMachine(TContext context, int capacity = 8, IEqualityComparer<TState> comparer = null) 
    //    {
    //        states = new Dictionary<TState, IState>(capacity, comparer);
    //    }

    //    public abstract void TransitionTo(TState key);
    //}

    //public class HogePlayerStateMachine : StateMachine<Player, PlayerState>
    //{
    //    class PlayerStateEqualityComparer : IEqualityComparer<PlayerState>
    //    {
    //        public bool Equals(PlayerState x, PlayerState y)
    //        {
    //            return (int)x == (int)y;
    //        }

    //        public int GetHashCode(PlayerState obj)
    //        {
    //            return ((int)obj).GetHashCode();
    //        }
    //    }

    //    public HogePlayerStateMachine(Player player) : base(player, 24, new PlayerStateEqualityComparer())
    //    {
    //        states.Add(PlayerState.Idle, new PlayerIdleState(player));
    //    }

    //    public override void TransitionTo(PlayerState key)
    //    {
          
    //    }
    //}

    public enum PlayerState
    {
        Idle,
        Move,
        Guard,
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
