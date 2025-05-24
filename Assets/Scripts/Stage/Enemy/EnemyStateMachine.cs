namespace Stage.Enemy
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
        public EnemyWanderState WanderState { get; private set; }

        public EnemyStateMachine(Enemy enemy)
        {
            IdleState = new EnemyIdleState(enemy);
            WanderState = new EnemyWanderState(enemy);
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
