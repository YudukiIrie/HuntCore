namespace Stage.Enemies
{
    /// <summary>
    /// �G�x�����
    /// </summary>
    public class EnemyAlertState : IState
    {
        Enemy _enemy;   // �G�N���X

        // �f�[�^�L���b�V���p
        float _actionDist;
        float _limitAngle;
        float _attackDist;

        public EnemyAlertState(Enemy enemy)
        {
            _enemy = enemy;

            _actionDist = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).ActionDist;
            _limitAngle  = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).LimitAngle;
            _attackDist  = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).AttackDist;
        }

        public void Enter()
        {
            _enemy.Animation.Alert();
        }

        public void Update()
        {
            Transition();
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }

        /// <summary>
        /// ��ԑJ��
        /// </summary>
        void Transition()
        {
            // �ǐ�
            if (_enemy.GetDistanceToPlayer() > _actionDist)
                _enemy.StateMachine.TransitionTo(EnemyState.Chase);
            // �����]��
            else if (_enemy.GetAngleToPlayer() > _limitAngle)
                _enemy.StateMachine.TransitionTo(EnemyState.Turn);
            // �U��
            else if (_enemy.CheckAttackState())
            {
                // �ʏ�U��
                if (_enemy.GetDistanceToPlayer() <= _attackDist)
                    _enemy.StateMachine.TransitionTo(EnemyState.Attack);
                // �܍U��
                else
                    _enemy.StateMachine.TransitionTo(EnemyState.ClawAttack);
            }
        }
    }
}