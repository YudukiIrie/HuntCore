using UnityEngine;

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
            _findDistance = EnemyDataList.Data.GetData(EnemyData.Type.BossEnemy).FindDistance;
        }

        public void Enter()
        {
            _enemy.Animation.ResetAll();
        }

        public void Update()
        {
            // === ��ԑJ�� ===
            // ���K
            if (_enemy.CheckDistanceToPlayer() <= _findDistance)
                _enemy.StateMachine.TransitionTo(_enemy.StateMachine.RoarState);
        }

        public void FixedUpdate()
        {

        }

        public void Exit()
        {

        }
    }
}
