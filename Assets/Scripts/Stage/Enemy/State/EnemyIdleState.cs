namespace Stage.Enemies
{
    /// <summary>
    /// �G�ҋ@���
    /// </summary>
    public class EnemyIdleState : IState
    {
        Enemy _enemy;   // �G�N���X
        float _findDistance;

        public EnemyIdleState(Enemy enemy)
        {
            _enemy = enemy;
            _findDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).FindDist;
        }

        public void Enter()
        {
            _enemy.Animation.Idle();
        }

        public void Update()
        {
            // === ��ԑJ�� ===
            // ���K
            if (_enemy.GetDistanceToPlayer() <= _findDistance)
                _enemy.StateMachine.TransitionTo(EnemyState.Roar);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
