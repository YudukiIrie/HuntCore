namespace Stage.Enemies
{
    /// <summary>
    /// �G�̃X�e�[�g���Ǘ�
    /// </summary>
    public class EnemyStateMachine
    {
        // ���݂̃X�e�[�g��ۑ�
        IState _currentState;

        // �e�X�e�[�g
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
